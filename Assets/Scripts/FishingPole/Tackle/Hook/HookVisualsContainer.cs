using UnityEngine;

public class HookVisualsContainer : MonoBehaviour
{
    [field: SerializeField]
    public Transform SnapPlacement { get; private set; }
    [field: SerializeField]
    public Rigidbody HookJointPoint { get; private set; }
    [field: SerializeField]
    public Outline Outline { get; private set; }
    [field: SerializeField]
    public Color CanAttachColor { get; private set; }
    [field: SerializeField]
    public Color CanNotAttachColor { get; private set; }
}
