using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishVisualsContainer : MonoBehaviour
{
    [field:SerializeField]
    public ParticleSystem ParticleSystem { get; private set; }

    [field: SerializeField]
    public GameObject Visuals { get; private set; }
}
