using UnityEngine;

/// <summary>
/// Taking responsibility for calculate force on cast
/// </summary>
public class CastingTracker : IUpdatable
{
    private Transform _trackerTransform;
    private Vector3 _trackedPosition;
    private float _fixedTimeStep;
    private float _timer;
    private UpdatableSystem _updatableSystem;
    public float TrackedDistance => Vector3.Distance(_trackerTransform.position, _trackedPosition);
    public Vector3 TrackedDirection => (_trackedPosition - _trackerTransform.position).normalized;
    public CastingTracker(Transform tracker)
    {
        _trackerTransform = tracker;

        CastingData data = SystemsContainer.GetSystem<ConfigsSystem>().GetConfig<PoleConfig>("PoleConfig").castingData;
        _fixedTimeStep = data.castingTrackerTimeStep;
        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
    }

    public void ExecuteUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= _fixedTimeStep)
        {
            _timer = 0;
            _trackedPosition = _trackerTransform.position;
        }
    }

    public void OnBeforeCast()
    {
        _trackedPosition = _trackerTransform.position;
        _updatableSystem.RegisterUpdatable(this);
    }

    public void OnAfterCast()
    {
        _trackedPosition = _trackerTransform.position;
        _updatableSystem.UnRegisterUpdatable(this);
    }
}
