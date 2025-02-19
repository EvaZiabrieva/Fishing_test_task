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

    public override void RemoveAttachment() { }

    public override void SetReadyToAttachVisual()
    {
        _hookVisualsContainer.Outline.enabled = true;
        _hookVisualsContainer.Outline.OutlineColor = _hookVisualsContainer.CanAttachColor;
    }


    public override void SetNotReadyToAttachVisual() 
    {
        _hookVisualsContainer.Outline.enabled = true;
        _hookVisualsContainer.Outline.OutlineColor = _hookVisualsContainer.CanNotAttachColor;
    }

    public override void SetDefaultVisual() =>
        _hookVisualsContainer.Outline.enabled = false;
}
