using UnityEngine;

public class MoveToRandomDirectionState : RandomTimeConstrainedState
{
    private FishBehaviourData _data;
    private Vector3 _direction;
    private float _oneFrameMoveDelta => _data.Strength;

    public MoveToRandomDirectionState(RangeFloat executionTimeRange, FishBehaviourData data) : base(executionTimeRange) 
    {
        _data = data;
    }

    public override void StartInternal()
    {
        base.StartInternal();
        _direction = GetRandomDirection(_data.XDirectionRange, _data.ZDirectionRange);
    }

    public override void UpdateInternal()
    {
        // if obstacle detected stop state immidiate 
        if(CheckForwardObstacles(_stateMachine.Behaviour.CurrentPosition, _direction, _oneFrameMoveDelta))
        {
            Stop();
            return;
        }

        _stateMachine.Behaviour.Pull(_direction);
    }

    private Vector3 GetRandomDirection(RangeFloat xRange, RangeFloat zRange) => new Vector3(xRange.random, 0, zRange.random);

    private bool CheckForwardObstacles(Vector3 origin, Vector3 direction, float distance)
    {
        Ray ray = new Ray(origin, direction);
        return Physics.Raycast(ray, distance, _data.ObstaclesLayerMask);
    }
}
