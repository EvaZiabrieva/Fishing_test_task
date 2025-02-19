using UnityEngine;

public class FishView : BaseFishView
{
    public FishView(FishVisualsContainer container) : base(container) {}

    public override GameObject GetFishVisuals() =>
        _container.Visuals;   

}
