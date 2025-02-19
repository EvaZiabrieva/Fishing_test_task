using System;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CollisionByLayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public event Action<float> OnEnterDetected;
    public event Action OnExitDetected;
    public bool IsActive { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsActive)
        {
            return;
        }

        if (layerMask.Contains(other.gameObject.layer))
        {
            float height = other.bounds.max.y;
            OnEnterDetected?.Invoke(height);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (layerMask.Contains(other.gameObject.layer))
        {
            OnExitDetected?.Invoke();
        }
    }
}
