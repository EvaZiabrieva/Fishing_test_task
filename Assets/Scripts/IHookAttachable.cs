using UnityEngine;
/// <summary>
/// Description of whan attachable items need to implement  
/// </summary>
public interface IHookAttachable 
{
    GameObject Visuals { get; }
    public bool ReadyToAttach { get; }
    public void OnAttach();
}
