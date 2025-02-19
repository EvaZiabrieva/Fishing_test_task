using UnityEngine;

public abstract class RandomTimeConstrainedState : BaseState
{
    public override bool Finished
    {
        get => _timer >= _executionTime;
        protected set
        {
            if (value)
                _timer = _executionTime;
        }
    }

    protected RangeFloat _executionTimeRange;
    protected float _executionTime;
    protected float _timer;

    protected RandomTimeConstrainedState(RangeFloat executionTimeRange)
    {
        _executionTimeRange = executionTimeRange;
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        UpdateInternal();
    }

    public override void StartInternal()
    {
        _timer = 0f;
        _executionTime = _executionTimeRange.random;
    }

    public abstract void UpdateInternal();
}