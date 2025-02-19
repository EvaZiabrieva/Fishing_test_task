using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WormBait : BaseBait
{
    [SerializeField] private XRGrabInteractable interactable;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    public override void OnAttach()
    {
        interactable.enabled = false;
        _rigidbody.isKinematic = true;
        Destroy(_collider);
    }

    public override void OnReattach()
    {
        Destroy(gameObject);
    }
}
