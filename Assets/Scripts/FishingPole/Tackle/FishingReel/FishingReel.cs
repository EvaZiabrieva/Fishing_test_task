using Unity.VRTemplate;
using UnityEngine;

public class FishingReel : BaseFishingReel
{
    private XRKnob _knob;
    private RangeFloat _knobRange;
    private float _currRoundLenght;
    private float _currntTension = 1;
    public FishingReel(BaseFishingReelView view, FishingReelData data, XRKnob knob) : base(view, data)
    {
        _knob = knob;
        _currRoundLenght = _data.RoundLength;
    }

    public override void ApplyTension(float tension)
    {
        _currntTension = tension;
        _knobRange.max *= _currntTension;
        _knob.maxAngle = _knobRange.max;
        _currRoundLenght /= _currntTension;
    }

    public override void RevertTension()
    {
        _knobRange.max /= _currntTension;
        _knob.maxAngle = _knobRange.max;
        _currRoundLenght *= _currntTension;
        _currntTension = 1;
    }

    public override float GetLength() => Mathf.Lerp(_knob.minAngle, _knob.maxAngle, _knob.value) / 360 * _currRoundLenght;

    public override float SetAngle(float angle) => _knob.value = angle;

    public override void SetRange(float minAngle, float maxAngle)
    {
        _knobRange.min = minAngle;
        _knobRange.max = maxAngle;

        _knob.minAngle = _knobRange.min;
        _knob.maxAngle = _knobRange.max;
    }
}
