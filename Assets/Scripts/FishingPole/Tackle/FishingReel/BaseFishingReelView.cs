using UnityEngine;

public abstract class BaseFishingReelView 
{
    protected FishingReelVisualsContainer _visualsContainer;

    public BaseFishingReelView(FishingReelVisualsContainer container)
    {
        _visualsContainer = container;
    }

    public abstract void Reel(float speed);

    public abstract void UnReel(float speed);
}
