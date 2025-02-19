using System;
using UnityEngine;

[Serializable]
public class PhysicsParametersConfig : BaseConfig
{
    public WaterPhysicsData waterPhysicsData;
    public float baseBobberBitingForce;
}

[Serializable]
public class WaterPhysicsData
{
    [field:SerializeField]
    public float SubmergedDepth { get; private set; }

    [field: SerializeField]
    public float DisplacementAmount { get; private set; }

    [field: SerializeField]
    public float LinearDrag { get; private set; }

    [field: SerializeField]
    public float AngularDrag { get; private set; }

    [field: SerializeField]
    public float WavingMultiplier { get; private set; }

    [field: SerializeField]
    public float WaterResistanceAmount { get; private set; }
}
