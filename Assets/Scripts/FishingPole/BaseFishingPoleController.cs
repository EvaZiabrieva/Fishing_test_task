public abstract class BaseFishingPoleController : IUpdatable
{
    protected readonly FishingPole _fishingPole;
    protected readonly CastingTracker _castingTracker;
    protected readonly float _trackedDistanceTreshold;

    protected BaseFishingPoleController(FishingPole pole, CastingTracker castingTracker)
    {
        _fishingPole = pole;
        _castingTracker = castingTracker;

        CastingData data = SystemsContainer.GetSystem<ConfigsSystem>().GetConfig<PoleConfig>("PoleConfig").castingData;
        _trackedDistanceTreshold = data.trackedDistanceTreshold;
    }

    public abstract void ExecuteUpdate();
}
