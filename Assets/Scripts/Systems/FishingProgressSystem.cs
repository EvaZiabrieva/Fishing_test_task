using System;
using UnityEngine;
using UnityEngine.UI;

public class FishingProgressSystem : MonoBehaviour, ISystem, IUpdatable
{
    private struct FishingProgressParameters
    {
        public RangeFloat PointsRange { get; private set; }
        public float DesiredAngle { get; private set; }
        public float MaxAllowedAngleOffset { get; private set; }
        public float ReelingMultiplierPower { get; private set; }
        public float DefaultPointsAmount { get; private set; }
        public float FishWeightPointsMultiplier { get; private set; }

        public FishingProgressParameters(FishingProgressConfig config)
        {
            PointsRange = config.progressPointsRange;
            DesiredAngle = config.desiredPoleToFishAngle;
            MaxAllowedAngleOffset = config.maxAllowedAngleOffset;
            ReelingMultiplierPower = config.reelingMultiplierPower;
            DefaultPointsAmount = config.defaultPointsAmount;
            FishWeightPointsMultiplier = config.fishWeightPointsMultiplier;
        }
    }

    // Left here for faster implementation
    [SerializeField] private Transform _fishBucket;

    [SerializeField] private Canvas _progressUI;
    [SerializeField] private Image _progressBar;

    private FishingPole _fishingPole;
    private Transform _bobber;
    private Fish _fish;
    private FishInteractionSystem _interactionSystem;
    private UpdatableSystem _updatableSystem;

    private FishingProgressParameters _parameters;

    private float _currentPoints;
    private float _previousLength;
    private float _results;

    public bool IsInitialized => _interactionSystem != null;
    public event Action<bool> OnFishingFinished;
    public event Action<float, float> OnGetResults;

    public void RegisterFishingPole(FishingPole fishingPole)
    {
        _fishingPole = fishingPole;
        _bobber = _fishingPole.Bobber.View.Rigidbody.transform;
    }

    public void Initialize()
    {
        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
        _interactionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();

        FishingProgressConfig config = SystemsContainer.GetSystem<ConfigsSystem>().GetConfig<FishingProgressConfig>("FishingProgressConfig");
        _parameters = new FishingProgressParameters(config);

        _interactionSystem.OnFishBit += OnFishBitHandler;
        OnFishingFinished += OnFishingFinishedHandler;
    }

    private void OnFishingFinishedHandler(bool isSuccessful)
    {
        _updatableSystem.UnRegisterUpdatable(this);
        _progressUI.gameObject.SetActive(false);

        if (isSuccessful)
        {
            CalculateResults();
            OnGetResults?.Invoke(_results, (_fish.Data.Weight / 1000) /*Calculate weight in kilogramms*/ );
        }
    }

    private void OnFishBitHandler(Fish fish)
    {
        _fish = fish;
        _progressUI.gameObject.SetActive(true);
        _previousLength = _fishingPole.FishingReel.GetLength();
        _updatableSystem.RegisterUpdatable(this);
        _currentPoints = _parameters.PointsRange.max / 2;
    }

    public void Shutdown()
    {
        _interactionSystem.OnFishBit -= OnFishBitHandler;
    }

    public void ExecuteUpdate()
    {
        Vector3 poleTipForward = _fishingPole.PoleTip.forward;
        Vector2 fishingPoleDirection = new Vector2(poleTipForward.x, poleTipForward.z);
        Vector2 desiredDirection = GetDirection(_fishingPole.PoleTip.position, _fish.transform.position);

        float currLenght = _fishingPole.FishingReel.GetLength();
        float reelingDelta = (_previousLength - currLenght);

        float pointsMultiplier = (reelingDelta * _parameters.ReelingMultiplierPower * _fish.Data.Weight);

        float angle = Vector2.Angle(fishingPoleDirection, desiredDirection);
        float absAngleOffset = Mathf.Abs(angle);

        float earnedPoints = Mathf.InverseLerp(_parameters.MaxAllowedAngleOffset * 2, 0, absAngleOffset) * pointsMultiplier;
        _currentPoints += (earnedPoints + _parameters.DefaultPointsAmount) * Time.deltaTime;
        _progressBar.fillAmount = _currentPoints / _parameters.PointsRange.max;

        if (_currentPoints >= _parameters.PointsRange.max)
        {
            OnFishingFinished?.Invoke(true);
        }

        if (_currentPoints <= _parameters.PointsRange.min)
        {
            OnFishingFinished?.Invoke(false);
        }

        _previousLength = currLenght;
    }

    private Vector2 GetDirection(Vector3 from, Vector3 to)
    {
        Vector3 direction = (to - from).normalized;
        return new Vector2(direction.x, direction.z);
    }

    public void OnContinue()
    {
        _fish.Reattach(_fishBucket);
    }

    private void CalculateResults()
    {
        _results = _fish.Data.Points + ((_fish.Data.Weight / _parameters.FishWeightPointsMultiplier) * _fish.Data.Level * _fish.Data.Rarity.GetMultiplier());
    }
}
