using System;
using UnityEngine;

/// <summary>
/// Can be extended with additional view logic for other fish visual states
/// </summary>
[Serializable]
public abstract class BaseFishView 
{
    protected FishVisualsContainer _container;

    protected BaseFishView(FishVisualsContainer container)
    {
        _container = container;
    }
    public abstract GameObject GetFishVisuals();
}
