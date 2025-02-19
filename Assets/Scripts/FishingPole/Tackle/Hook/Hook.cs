using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : BaseHook
{
    private ConfigurableJoint _joint;
    private SoftJointLimit currentLimit;
    private Rigidbody _rigidbody;
    private GrabableSystem _grabableSystem;
    private FishInteractionSystem _fishInteractionSystem;

    public Hook(BaseHookView view, AttachDetector attachDetector,
                HookData data, ConfigurableJoint joint, Rigidbody rigidbody) : base (view, attachDetector, data)
    {
        _joint = joint;
        _rigidbody = rigidbody;
        _attachDetector = attachDetector;

        _grabableSystem = SystemsContainer.GetSystem<GrabableSystem>();
        _fishInteractionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();
    }
    public override void Initialize()
    {
        if (_grabableSystem.ContainsHookAttachable)
            CheckForAttach();

        _attachDetector.OnAttach += _view.Attach;
        _attachDetector.OnAttach += OnAttachHandler;

        _grabableSystem.OnAttachableGrab += CheckForAttach;
        _grabableSystem.OnAttachableDrop += SetDefault;

        _fishInteractionSystem.OnFishBit += OnBaitEaten;
    }

    public override void Shutdown()
    {
        _attachDetector.OnAttach -= _view.Attach;
        _attachDetector.OnAttach -= OnAttachHandler;

        _grabableSystem.OnAttachableGrab -= CheckForAttach;
        _grabableSystem.OnAttachableDrop -= SetDefault;

        _fishInteractionSystem.OnFishBit -= OnBaitEaten;
    }

    public override void CheckForAttach()
    {
        if (currentBaitCount < Data.BaitCapacity)
        {
            _attachDetector.isActive = true;
            _view.SetReadyToAttachVisual();
        }
        else
        {
            _view.SetNotReadyToAttachVisual();
        }
    }

    public override void SetDefault()
    {
        _attachDetector.isActive = false;
        _view.SetDefaultVisual();
    }

    private void OnBaitEaten(Fish fish)
    {
        _baits[0].OnReattach();
        _baits.RemoveAt(0);

        _view.Attach(fish);
    }

    private void OnAttachHandler(IHookAttachable attachable)
    {
        _attachDetector.isActive = false;

        //TODO: prevent unnecessary casting
        if (attachable is BaseBait bait)
        {
            _baits.Add(bait);
        }
    }

    public override void OnWaterDetectedHandler()
    {
        _fishInteractionSystem.SetupFish(_baits, _rigidbody.gameObject.transform);
    }
}
