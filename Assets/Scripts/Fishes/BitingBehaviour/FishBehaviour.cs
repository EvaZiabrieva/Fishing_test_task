using UnityEngine;

public class FishBehaviour : BaseFishBehaviour
{
    private Transform _fishTransform;
    public override Vector3 CurrentPosition => _fishTransform.position;

    public FishBehaviour(FishBehaviourData data, Transform fishTransform) : base(data)
    {
        _fishTransform = fishTransform;
    }


    public override void Bite()
    {
        
    }

    public override void Pull(Vector3 direction)
    {
        _fishTransform.position += direction.normalized * _behaviourData.Strength * Time.deltaTime;
    }

    public override void Release()
    {
        
    }
}
