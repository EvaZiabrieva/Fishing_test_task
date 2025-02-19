using System;
using UnityEngine;

[Serializable]
public class FishConfig : BaseConfig
{
    public float weight;
    public int points;
    public int level;
    public Rarity rarity;
    public FishBehaviourData behaviourData;
}
