using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHook
{
    protected BaseHookView _view;
    protected AttachDetector _attachDetector;
    protected HookData _hookData;
    protected List<BaseBait> _baits;
    protected int currentBaitCount => _baits.Count;
    public BaseHookView View => _view;
    public AttachDetector AttachDetector => _attachDetector;
    public HookData Data => _hookData;
    public List<BaseBait> Baits => _baits;

    protected BaseHook(BaseHookView view, AttachDetector attachDetector, HookData data)
    {
        _view = view;
        _attachDetector = attachDetector;
        _hookData = data;
        _baits = new List<BaseBait>(_hookData.BaitCapacity);
    }

    public abstract void Initialize();
    public abstract void Shutdown();
    public abstract void CheckForAttach();
    public abstract void SetDefault();
    public abstract void OnWaterDetectedHandler();
}

[System.Serializable]
public class HookData
{
    public int BaitCapacity { get; private set; }
    public float FailChance { get; private set; }

    public HookData(int capacity, float failChance)
    {
        BaitCapacity = capacity;
        FailChance = failChance;
    }

    public HookData(HookConfig config)
    {
        BaitCapacity = config.baitCapacity;
        FailChance = config.failChance;
    }
}


