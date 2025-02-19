using System;

public abstract class BaseFishingLine
{
    protected float _maxLength;
    protected float _currentLength;
    protected float _strength;
    protected BaseFishingLineView _view;
    public BaseFishingLineView View => _view;
}

[Serializable]
public class FishingLineData
{
    public float MaxLength { get; private set; }
    public float Strength { get; private set; }

    public FishingLineData(float maxLength, float strength)
    {
        MaxLength = maxLength;
        Strength = strength;
    }

    public FishingLineData(FishingLineConfig config)
    {
        MaxLength = config.maxLength;
        Strength = config.strength;
    }
}
