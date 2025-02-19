using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : BaseBobber
{
    private ConfigurableJoint _joint;
    private SoftJointLimit currentLimit;
    private FishInteractionSystem _fishInteractionSystem;
    private CollisionByLayerDetector _waterDetector;
    private DefaultAudioPlayer _audioPlayer;

    public override event Action<float> OnWaterEntered
    {
        add => _waterDetector.OnEnterDetected += value; 
        remove => _waterDetector.OnEnterDetected -= value; 
    }

    public override event Action OnWaterExited
    {
        add => _waterDetector.OnExitDetected += value;
        remove => _waterDetector.OnExitDetected -= value;
    }

    public Bobber(BaseBobberView view, CollisionByLayerDetector waterDetector,
                  ConfigurableJoint joint, DefaultAudioPlayer audioPlayer) : base(view)
    {
        _waterDetector = waterDetector;
        _joint = joint;

        _fishInteractionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();
        _audioPlayer = audioPlayer;
    }

    public override void Initialize()
    {
        OnWaterEntered += OnWaterEnteredHandler;
        OnWaterExited += OnWaterExitedHandler;
        _view.Initialize();
        _audioPlayer.Initialize();
    }

    public override void Shutdown()
    {
        OnWaterEntered -= OnWaterEnteredHandler;
        OnWaterExited -= OnWaterExitedHandler;
        _view.Shutdown();
    }

    public override void Cast(Vector3 direction, float force)
    {
        _view.Rigidbody.velocity = Vector3.zero;
        _view.Rigidbody.AddForce(direction * force, ForceMode.Impulse);
        _waterDetector.IsActive = true;
    }

    public override void UpdateOffset(float reeledDistance)
    {
        SetLimit(reeledDistance);
    }

    private void SetLimit(float limit)
    {
        currentLimit = new SoftJointLimit();
        currentLimit.limit = limit;
        _joint.linearLimit = currentLimit;
    }
    
    private void OnWaterEnteredHandler(float height)
    {
        _waterDetector.IsActive = false;
        _view.OnWaterEnter(height);
        _audioPlayer.Play();
    }

    private void OnWaterExitedHandler()
    {
        _fishInteractionSystem.AbortFishingProcess();
        _view.OnWaterExit();
    }
}