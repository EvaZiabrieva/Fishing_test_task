using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FishingProgressConfig : BaseConfig, ISerializationCallbackReceiver
{
    [Serializable]
    public class RarityMultiplier
    {
        [SerializeField] public Rarity rarity;
        [SerializeField] public float multiplier;
    }

    public RangeFloat progressPointsRange;
    public float desiredPoleToFishAngle;
    public float maxAllowedAngleOffset;
    public float reelingMultiplierPower;
    public float defaultPointsAmount;
    public float fishWeightPointsMultiplier;

    public List<RarityMultiplier> rarityMultiplierList;
    public Dictionary<Rarity, float> rarityMultipliers;

    public void OnBeforeSerialize()
    {
        FillDictionary();
    }

    public void OnAfterDeserialize()
    {
        FillDictionary();
    }

    private void FillDictionary()
    {
        if (rarityMultiplierList == null)
        {
            return;
        }

        rarityMultipliers = new Dictionary<Rarity, float>(rarityMultiplierList.Count);

        foreach (RarityMultiplier element in rarityMultiplierList)
        {
            if (rarityMultipliers.ContainsKey(element.rarity))
            {
                rarityMultipliers[element.rarity] = element.multiplier;
                continue;
            }

            rarityMultipliers.Add(element.rarity, element.multiplier);
        }
    }
}

