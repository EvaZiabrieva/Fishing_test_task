using Unity.VRTemplate;
using UnityEngine;

/// <summary>
/// Incapsulated fishing pole creation and initialization logic
/// </summary>
public class FishingPoleFactory
{
    private Pole _pole;
    private PoleView _poleView;

    private FishingReel _fishingReel;
    private FishingReelView _fishingReelView;

    private FishingLine _fishingLine;
    private FishingLineView _fishingLineView;

    private Hook _hook;
    private HookView _hookView;

    private Bobber _bobber;
    private BobberView _bobberView;

    private FishingPole _fishingPole;
    private FishingPoleController _fishingPoleController;

    private GameObject _poleObject;
    private GameObject _hookObject;
    private Rigidbody _bobberRigidbody;
    private PoleVisualsContainer _poleVisualsContainer;

    private ConfigsSystem _configSystem;

    public void CreateFishingPole(GameObject pole, GameObject fishingReel, GameObject fishingLine,
                                  GameObject hook, GameObject bobber, Transform spawnPoint)
    {
        _configSystem = SystemsContainer.GetSystem<ConfigsSystem>();

        CreatePole(pole, spawnPoint.position);
        CreateReel(fishingReel, _poleVisualsContainer.ReelPlacement.position);
        CreateBobber(bobber, _poleVisualsContainer.BobberPlacement.position);
        CreateHook(hook, _poleVisualsContainer.HookPlacement.position);
        CreateFishingLine(fishingLine, _poleVisualsContainer.FishingLinePlacement.position);

        _fishingPole = _poleObject.GetComponent<FishingPole>();

        CastingTracker tracker = CreateCastingTracker();
        _fishingPoleController = new FishingPoleController(_fishingPole, tracker);
        FishingPoleBaitingController baitingController = new FishingPoleBaitingController(_fishingPole);

        _fishingPole.Initialize(_hook, _bobber, _fishingLine, _fishingReel, _pole, _fishingPoleController, baitingController);
    }

    public void RemoveFishingPole()
    {
        _fishingPoleController.Shutdown();
    }

    private void CreatePole(GameObject pole, Vector3 spawnPoint)
    {
        _poleObject = GameObject.Instantiate(pole, spawnPoint, Quaternion.identity);
        _poleVisualsContainer = _poleObject.GetComponent<PoleVisualsContainer>();
        PoleData data = new PoleData(_configSystem.GetConfig<PoleConfig>("PoleConfig"));
        _poleView = new PoleView(_poleVisualsContainer);
        _pole = new Pole(data, _poleView);
    }
    private void CreateReel(GameObject fishingReel, Vector3 spawnPoint)
    {
        GameObject reelObject = GameObject.Instantiate(fishingReel, spawnPoint, Quaternion.identity, _poleObject.transform);
        FishingReelVisualsContainer reelVisualsContainer = reelObject.GetComponent<FishingReelVisualsContainer>();
        XRKnob knob = reelObject.GetComponentInChildren<XRKnob>();
        _fishingReelView = new FishingReelView(reelVisualsContainer);
        FishingReelData data = new FishingReelData(_configSystem.GetConfig<FishingReelConfig>("FishingReelConfig"));
        _fishingReel = new FishingReel(_fishingReelView, data, knob);
    }
    private void CreateHook(GameObject hook, Vector3 spawnPoint)
    {
        _hookObject = GameObject.Instantiate(hook, spawnPoint, Quaternion.identity);
        HookVisualsContainer hookVisualsContainer = _hookObject.GetComponent<HookVisualsContainer>();

        ConfigurableJoint configurableJoint = _hookObject.GetComponent<ConfigurableJoint>();
        configurableJoint.connectedBody = _bobberRigidbody;
        Rigidbody hoohRigidbody = _hookObject.GetComponent<Rigidbody>();
        AttachDetector attachDetector = _hookObject.GetComponent<AttachDetector>();

        _hookView = new HookView(hookVisualsContainer);
        HookData data = new HookData(_configSystem.GetConfig<HookConfig>("HookConfig"));
        _hook = new Hook(_hookView, attachDetector, data, configurableJoint, hoohRigidbody);
    }
    private void CreateBobber(GameObject bobber, Vector3 spawnPoint)
    {
        GameObject bobberObject = GameObject.Instantiate(bobber, spawnPoint, Quaternion.identity);
        BobberVisualsContainer bobberVisualsContainer = bobberObject.GetComponent<BobberVisualsContainer>();

        Rigidbody connectedRigidbody = GameObject.Instantiate(bobberVisualsContainer.BobberJointPoint, spawnPoint, Quaternion.identity, _poleObject.transform);
        ConfigurableJoint configurableJoint = bobberObject.GetComponent<ConfigurableJoint>();
        configurableJoint.connectedBody = connectedRigidbody;
        _bobberRigidbody = bobberObject.GetComponent<Rigidbody>();
        CollisionByLayerDetector waterDetector = bobberObject.GetComponent<CollisionByLayerDetector>();
        DefaultAudioPlayer audioPlayer = bobberObject.GetComponent<DefaultAudioPlayer>();

        _bobberView = new BobberView(bobberVisualsContainer, _bobberRigidbody);
        _bobber = new Bobber(_bobberView, waterDetector, configurableJoint, audioPlayer);
    }
    private void CreateFishingLine(GameObject fishingLine, Vector3 spawnPoint)
    {
        GameObject lineObject = GameObject.Instantiate(fishingLine, spawnPoint, Quaternion.identity, _poleObject.transform);

        PointsData pointsData = new PointsData()
        {
            StartPoint = _poleVisualsContainer.FishingLinePlacement,
            EndPoint = _hookObject.transform
        };
        LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();

        _fishingLineView = new FishingLineView(pointsData, lineRenderer, 10);
        _fishingLine = new FishingLine(10, 0, 10, _fishingLineView);
    }

    private CastingTracker CreateCastingTracker()
    {
        GameObject trackerObject = new GameObject("Tracker");
        trackerObject.transform.position = _poleVisualsContainer.PoleTip.position;
        trackerObject.transform.parent = _poleObject.transform;
        return new CastingTracker(trackerObject.transform);
    }
}
