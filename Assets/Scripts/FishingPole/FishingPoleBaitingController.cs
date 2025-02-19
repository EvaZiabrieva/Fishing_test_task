/// <summary>
/// Taking responsibility for react on fish interaction
/// </summary>
public class FishingPoleBaitingController : BaseFishingPoleBaitingController
{
    private FishInteractionSystem _fishInteractionSystem;

    public FishingPoleBaitingController(FishingPole pole) : base(pole) 
    {
        _fishInteractionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();
    }

    public override void Initialize()
    {
        _fishInteractionSystem.OnFishBit += OnFishBitHandler;
        _fishInteractionSystem.OnFishingFinished += OnFishingFinishedHandler;
    }

    private void OnFishingFinishedHandler(bool result)
    {
        _fishingPole.FishingReel.RevertTension();
    }

    private void OnFishBitHandler(Fish fish)
    {
        _baitedFish = fish;
        _fishingPole.FishingReel.ApplyTension(_baitedFish.Data.Tension);
    }

    public override void Shutdown()
    {
        _fishInteractionSystem.OnFishBit -= OnFishBitHandler;
        _fishInteractionSystem.OnFishingFinished -= OnFishingFinishedHandler;
    }
}
