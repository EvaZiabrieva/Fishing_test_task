using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookView : BaseHookView
{
    public HookView(HookVisualsContainer hookVisualsContainer) : base(hookVisualsContainer) { }

    public override void Attach(IHookAttachable attachable)
    {
        attachable.OnAttach();
        _hookAttachable = attachable;
        _attachableVisuals = attachable.Visuals;
        _attachableVisuals.transform.position = _hookVisualsContainer.SnapPlacement.position;
        _attachableVisuals.transform.SetParent(_hookVisualsContainer.SnapPlacement);
    }

    public override void NotReadyToAttach()
    {
        
    }

    public override void ReadyToAttach()
    {
        throw new System.NotImplementedException();
    }

    public override void RemoveAttachment() { }
}
