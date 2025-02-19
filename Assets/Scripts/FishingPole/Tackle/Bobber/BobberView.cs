using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BobberView : BaseBobberView, IFixedUpdatable
{
    private WaterPhysicsData _waterPhysicsData;
    private float _bitingForce;

    private float _waterHeight;
    private float _waterResistanceMultiplier;

    private UpdatableSystem _updatableSystem;
    private FishInteractionSystem _fishInteractionSystem;

    public BobberView(BobberVisualsContainer bobberVisualsContainer, Rigidbody rigidbody) : base(bobberVisualsContainer, rigidbody)
    {
        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
        _fishInteractionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();

        PhysicsParametersConfig config = SystemsContainer.GetSystem<ConfigsSystem>().GetConfig<PhysicsParametersConfig>("PhysicsParametersConfig");
        _waterPhysicsData = config.waterPhysicsData;
        _bitingForce = config.baseBobberBitingForce;
    }

    public override void Initialize()
    {
        _fishInteractionSystem.OnFishBit += OnFishBitHandler;
        _fishInteractionSystem.OnFishBitTheBait += OnFishBitTheBaitHandler;
        _fishInteractionSystem.OnFishingFinished += OnFishingFinishedHandler;
    }

    public override void Shutdown()
    {
        _fishInteractionSystem.OnFishBit -= OnFishBitHandler;
        _fishInteractionSystem.OnFishBitTheBait -= OnFishBitTheBaitHandler;
        _fishInteractionSystem.OnFishingFinished -= OnFishingFinishedHandler;
    }

    // Bobber water simulation implementation
    public void ExecuteFixedUpdate()
    {
        // Wether we need to simulate water pushing the bobber out
        if (Rigidbody.transform.position.y < _waterHeight)
        {
            // Simulate water pushing the bobber out
            float displacementMultiplier = Mathf.Clamp01(-Rigidbody.transform.position.y / _waterPhysicsData.SubmergedDepth) * _waterPhysicsData.DisplacementAmount;
            Vector3 verticalForce = new Vector3(0, Mathf.Abs(Physics.gravity.y) * Mathf.Clamp01(_waterResistanceMultiplier) * displacementMultiplier, 0);

            // Calculate bobber on waves behaviour
            Rigidbody.AddForce(verticalForce + (Rigidbody.transform.forward * Time.deltaTime * _waterPhysicsData.WavingMultiplier));

            // Simulate water pushing out strength depend on bobber approximation to the water surface
            _waterResistanceMultiplier += Time.deltaTime;
            return;
        }

        _waterResistanceMultiplier = _waterPhysicsData.WaterResistanceAmount;
    }

    protected override void OnFishBitHandler(Fish fish)
    {
        _waterHeight -= 1;
        Rigidbody.transform.parent = fish.transform;
        ParticleSystem.Emit(1);
    }

    protected override void OnFishingFinishedHandler(bool result)
    {
        Rigidbody.transform.parent = null;
    }

    protected override void OnFishBitTheBaitHandler(float strength)
    {
        Rigidbody.AddForce(-Vector3.up * strength * _bitingForce, ForceMode.Impulse);
        ParticleSystem.Emit(1);
    }

    public override void OnWaterEnter(float height)
    {
        Rigidbody.transform.rotation = Quaternion.identity;
        Rigidbody.freezeRotation = true;

        _waterHeight = height;
        Rigidbody.drag = _waterPhysicsData.LinearDrag;
        Rigidbody.angularDrag = _waterPhysicsData.AngularDrag;
        _waterResistanceMultiplier = _waterPhysicsData.LinearDrag;

        _updatableSystem.RegisterFixedUpdatable(this);
        ParticleSystem.Emit(1);
    }

    public override void OnWaterExit()
    {
        Rigidbody.freezeRotation = false;

        _waterHeight = 0;
        Rigidbody.drag = 0;
        Rigidbody.angularDrag = 0;

        _updatableSystem.UnRegisterFixedUpdatable(this);
    }
}
