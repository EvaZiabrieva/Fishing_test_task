using UnityEngine;
public class BiteTheBaitState : BaseState
{
    private FishInteractionSystem _fishInteractionSystem;
    private RangeFloat _bitingDelay;
    private int _bitesCount;
    private int _remainedBitesCount;
    private float _strength;
    private Fish _fish;

    private float _timer;

    public override bool Finished 
    { 
        get => _remainedBitesCount <= 0; 
        protected set => _remainedBitesCount = 0; 
    }

    public BiteTheBaitState(RangeFloat bitesDelayRange, int bitesCount, float strength, Fish fish)
    {
        _bitingDelay = bitesDelayRange;
        _bitesCount = bitesCount;
        _strength = strength;
        _fish = fish;
        _fishInteractionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();
    }

    public override void StartInternal()
    {
        base.StartInternal();
        _remainedBitesCount = _bitesCount;
        _timer = _bitingDelay.random;
    }

    public override void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer > 0)
            return;

        _fishInteractionSystem.InvokeOnFishBiteTheBait(_strength);
        _timer = _bitingDelay.random;
        _remainedBitesCount--;
    }

    public override void StopInternal()
    {
        base.StopInternal();
        _fishInteractionSystem.InvokeOnFishBit(_fish);
    }
}
