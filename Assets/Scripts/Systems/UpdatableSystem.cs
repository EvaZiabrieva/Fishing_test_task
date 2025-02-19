using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Taking responsibility for updating updatable objects
/// </summary>
public class UpdatableSystem : MonoBehaviour, ISystem
{
    private List<IUpdatable> _updatables;
    private List<IFixedUpdatable> _fixedUpdatables;
    public bool IsInitialized => _updatables != null;

    public void Initialize()
    {
        _updatables = new List<IUpdatable>();
        _fixedUpdatables = new List<IFixedUpdatable>();
    }

    public void Shutdown()
    {
        _updatables.Clear();
        _fixedUpdatables.Clear();
    }

    public void RegisterUpdatable(IUpdatable updatable)
    {
        if (_updatables.Contains(updatable))
        {
            Debug.LogWarning($"Updatable {updatable} is already registred");
            return;
        }

        _updatables.Add(updatable);
    }

    public void UnRegisterUpdatable(IUpdatable updatable)
    {
        if (!_updatables.Contains(updatable))
        {
            Debug.LogWarning($"Updatable {updatable} is not registred yet");
            return;
        }

        _updatables.Remove(updatable);
    }

    public void RegisterFixedUpdatable(IFixedUpdatable updatable)
    {
        if (_fixedUpdatables.Contains(updatable))
        {
            Debug.LogWarning($"Updatable {updatable} is already registred");
            return;
        }

        _fixedUpdatables.Add(updatable);
    }   
    
    public void UnRegisterFixedUpdatable(IFixedUpdatable updatable)
    {
        if (!_fixedUpdatables.Contains(updatable))
        {
            Debug.LogWarning($"Updatable {updatable} is not registred yet");
            return;
        }

        _fixedUpdatables.Remove(updatable);
    }

    private void Update()
    {
        if (!IsInitialized)
            return;

        foreach (IUpdatable updatable in _updatables.ToArray())
        {
            updatable.ExecuteUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (!IsInitialized)
            return;

        foreach (IFixedUpdatable updatable in _fixedUpdatables.ToArray())
        {
            updatable.ExecuteFixedUpdate();
        }
    }
}
