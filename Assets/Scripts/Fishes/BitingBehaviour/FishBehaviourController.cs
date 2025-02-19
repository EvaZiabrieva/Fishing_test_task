using UnityEngine;

public class FishBehaviourController : BaseFishBehaviourController
{
    private UpdatableSystem _updatableSystem;
    private FishInteractionSystem _fishInteractionSystem;
    private FishBehaviourData _data;
    private FishBehaviourStateMachine _stateMachine;
    private Transform _target;

    public FishBehaviourController(Fish fish, Transform target) : base(fish) 
    {
        _target = target;
        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
        _fishInteractionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();
    }

    private void InitializeStateMachine()
    {
        FollowTargetState followState = new FollowTargetState(_target);
        _stateMachine = new FishBehaviourStateMachine(_fish.Behaviour, followState);
        BiteTheBaitState biteTheBaitState = new BiteTheBaitState(_data.BitesDelayRange, _data.BitesCountRange.random, _data.Strength, _fish);
        _stateMachine.EnqueueOneTimeState(biteTheBaitState);
        MoveToRandomDirectionState randomDirectionState = new MoveToRandomDirectionState(_data.ChangeDirectionDelayRange, _data);
        _stateMachine.EnqueueRepetableState(randomDirectionState);
    }

    public override void Initialize()
    {
        _data = _fish.Data.BehaviourData;
        InitializeStateMachine();
        _stateMachine.Start();
        _updatableSystem.RegisterUpdatable(this);
    }

    public override void Shutdown()
    {
        _stateMachine?.Stop();
        _updatableSystem.UnRegisterUpdatable(this);
    }

    public override void ExecuteUpdate()
    {
       _stateMachine.Update();
    }
}
