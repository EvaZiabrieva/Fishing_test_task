using UnityEngine;

public abstract class BasePoleView 
{
    protected PoleVisualsContainer _container;

    public BasePoleView(PoleVisualsContainer container)
    {
        _container = container;
    }

    //Possible to implement pole bending depend on fish pulling strength
    public abstract void SetBending(float bending);
}
