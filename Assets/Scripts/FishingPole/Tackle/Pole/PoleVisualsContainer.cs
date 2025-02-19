using UnityEngine;

public class PoleVisualsContainer : MonoBehaviour
{
    [field:SerializeField]
    public Transform ReelPlacement { get; private set; }

    [field:SerializeField]
    public Transform FishingLinePlacement { get; private set; }

    [field: SerializeField]
    public Transform BobberPlacement { get; private set; }

    [field: SerializeField]
    public Transform HookPlacement { get; private set; }

    [field: SerializeField]
    public Transform PoleTip { get; private set; }
}
