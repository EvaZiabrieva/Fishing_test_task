using System;

[Serializable]
public class PoleConfig : BaseConfig
{
    public float strength;
    public CastingData castingData;
}

[Serializable]
public class CastingData
{
    public float trackedDistanceTreshold;
    public float castingTrackerTimeStep;
}
