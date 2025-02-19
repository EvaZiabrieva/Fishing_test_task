using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : BaseFishingLine
{
    public FishingLine(float lenght, float currentLenght, float strength, BaseFishingLineView fishingLineView)
    {
        _maxLength = lenght;
        _currentLength = currentLenght;
        _strength = strength;   
        _view = fishingLineView;

        _view.SetLenght(_currentLength);
    }
}
