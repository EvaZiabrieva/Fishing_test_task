using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//Most grabbable objects need  to start their logic loop only when they are grabed, so this system kinda "activate" objects on grab
//and disable on drop  
/// <summary>
/// Taking responsibility for track the grab and drop of interactive objects
/// </summary>
public class GrabableSystem : MonoBehaviour, ISystem
{
    [SerializeField] private List<XRRayInteractor> _interactors = new List<XRRayInteractor>();
    private bool _containsHookAttachable;

    public event Action OnAttachableGrab;
    public event Action OnAttachableDrop;
    public bool IsInitialized => _interactors != null;
    public bool ContainsHookAttachable => _containsHookAttachable;
    public void Initialize()
    {
        foreach (var interactor in _interactors)
        {
            interactor.onSelectEntered.AddListener(OnGrab);
            interactor.onSelectExited.AddListener(OnDrop);
        }
    }

    private void OnGrab(XRBaseInteractable arg0)
    {
        if(arg0.TryGetComponent(out IGrabable grabable))
        {
            grabable.OnGrab();
        }
        if(arg0.TryGetComponent(out IHookAttachable hookAttachable))
        {
            OnAttachableGrab?.Invoke();
            _containsHookAttachable = true;
        }
    }
    private void OnDrop(XRBaseInteractable arg0)
    {
        if (arg0.TryGetComponent(out IGrabable grabable))
        {
            grabable.OnDrop();
        }
        if (arg0.TryGetComponent(out IHookAttachable hookAttachable))
        {
            OnAttachableDrop?.Invoke();
            _containsHookAttachable = false;
        }
    }
    public void Shutdown()
    {
        foreach (var interactor in _interactors)
        {
            interactor.onSelectEntered.RemoveListener(OnGrab);
            interactor.onSelectExited.RemoveListener(OnDrop);
        }
    }
}
