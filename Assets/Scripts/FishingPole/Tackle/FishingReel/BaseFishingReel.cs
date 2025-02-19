
public abstract class BaseFishingReel
{
    protected BaseFishingReelView _view;
    protected FishingReelData _data;
    public FishingReelData Data => _data;

    protected BaseFishingReel(BaseFishingReelView view, FishingReelData data)
    {
        _view = view;
        _data = data;
    }

    public abstract void SetRange(float minAngle, float maxAngle);
    public abstract float GetLength();
    public abstract float SetAngle(float angle);
    public abstract void ApplyTension(float tension);
    public abstract void RevertTension();
}

public class FishingReelData
{
    public float RoundLength { get; private set; }

    public FishingReelData(float roundLength)
    {
        RoundLength = roundLength;
    }

    public FishingReelData(FishingReelConfig config)
    {
        RoundLength = config.roundLength;
    }
}
