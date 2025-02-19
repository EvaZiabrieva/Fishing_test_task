using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBait : MonoBehaviour, IHookAttachable, IGrabable
{
    [SerializeField] protected GameObject _visuals;
    [SerializeField] protected float _attractivenessStrenght;
    protected bool _grabable = false;
    public GameObject Visuals => _visuals;
    public float AttractivenessStrenght => _attractivenessStrenght;

    public bool ReadyToAttach => _grabable;

    public abstract void OnAttach();
    public abstract void OnReattach();

    public void OnGrab() => _grabable = true;
    public void OnDrop() => _grabable = false;    
}
