using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberVisualsContainer : MonoBehaviour
{
    [field: SerializeField]
    public Rigidbody BobberJointPoint { get; private set; }

    [field: SerializeField]
    public ParticleSystem ParticleSystem { get; private set; }
}
