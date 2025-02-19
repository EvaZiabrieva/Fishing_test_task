
using UnityEngine;

public abstract class BaseBobberView
{
    protected BobberVisualsContainer _bobberVisualsContainer;
    public Rigidbody Rigidbody { get; protected set; }
    public ParticleSystem ParticleSystem { get; protected set; }
    public BaseBobberView(BobberVisualsContainer bobberVisualsContainer, Rigidbody rigidbody)
    {
        _bobberVisualsContainer = bobberVisualsContainer;
        Rigidbody = rigidbody;
        ParticleSystem = _bobberVisualsContainer.ParticleSystem;
    }
    public abstract void Initialize();
    public abstract void Shutdown();
    protected abstract void OnFishBitTheBaitHandler(float strength);
    protected abstract void OnFishBitHandler(Fish fish);
    protected abstract void OnFishingFinishedHandler(bool result);
    public abstract void OnWaterEnter(float height);
    public abstract void OnWaterExit();
}
