using System;
using UnityEngine;

[Serializable]
public enum Rarity
{
    Common, 
    Rare,
}

public static class RarityExtensions
{
    public static float GetMultiplier (this Rarity rarity)
    {
        return SystemsContainer.GetSystem<ConfigsSystem>().GetConfig<FishingProgressConfig>("FishingProgressConfig").rarityMultipliers[rarity];
    }
}

[Serializable]
public class FishData
{ 
    [field: SerializeField] public string Id { get; private set; }

    /// <summary>
    /// Weight in gramms
    /// </summary>
    [field: SerializeField] public float Weight {  get; private set; }
    [field: SerializeField] public int Points { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public Rarity Rarity { get; private set; }
    [field: SerializeField] public FishBehaviourData BehaviourData { get; private set; }
    public float Tension => Weight * BehaviourData.Strength;

    public FishData(string id, float weight, int points, int level, 
                    Rarity rarity, FishBehaviourData behavioutData)
    {
        Id = id;
        Weight = weight;
        Points = points;
        Level = level;
        Rarity = rarity;
        BehaviourData = behavioutData;
    }

    public FishData(FishConfig config)
    {
        Id = config.id;
        Weight = config.weight;
        Points = config.points;
        Level = config.level;
        Rarity = config.rarity;
        BehaviourData = config.behaviourData;
    }
}

[Serializable]
public class FishBehaviourData
{
    [field: SerializeField] public float Strength { get; private set; }
    [field: SerializeField] public RangeFloat BitDelayRange { get; private set; }
    [field: SerializeField] public RangeInt BitesCountRange { get; private set; }
    [field: SerializeField] public RangeFloat BitesDelayRange { get; private set; }
    [field: SerializeField] public RangeFloat ChangeDirectionDelayRange { get; private set; }
    [field: SerializeField] public RangeFloat XDirectionRange { get; private set; }
    [field: SerializeField] public RangeFloat ZDirectionRange { get; private set; }
    [field: SerializeField] public LayerMask ObstaclesLayerMask { get; private set; }

    public FishBehaviourData(float strength, RangeFloat bitDelayRange, RangeInt bitesCountRange, 
                             RangeFloat bitesDelayRange, RangeFloat changeDirectionDelayRange, 
                             RangeFloat xDirectionRange, RangeFloat zDirectionRange, LayerMask mask)
    {
        Strength = strength;
        BitDelayRange = bitDelayRange;
        BitesCountRange = bitesCountRange;
        BitesDelayRange = bitesDelayRange;
        ChangeDirectionDelayRange = changeDirectionDelayRange;
        XDirectionRange = xDirectionRange;
        ZDirectionRange = zDirectionRange;
        ObstaclesLayerMask = mask;
    }
}
