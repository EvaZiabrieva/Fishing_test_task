using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Taking responsibility for register and keep all systems references
/// </summary>
public static class SystemsContainer 
{
    private static Dictionary<Type, ISystem> _systems = new Dictionary<Type, ISystem>();

    public static void Initialize(ISystem[] systems)
    {
        foreach (ISystem system in systems)
        {
            system.Initialize();
            _systems.Add(system.GetType(), system);
        }
    }

    public static void RegisterSystem(ISystem system)
    {
        if (_systems.ContainsValue(system))
        {
            Debug.LogWarning($"System {system} is already registered");
            return;
        }

        system.Initialize();
        _systems.Add(system.GetType(), system);
    }

    public static void UnRegisterSystem(ISystem system)
    {
        if (!_systems.ContainsValue(system))
        {
            Debug.LogWarning($"System {system} is not registered yet");
            return;
        }

        system.Shutdown();
        _systems.Remove(system.GetType());
    }

    public static T GetSystem<T>() where T : ISystem
    {
        if(!_systems.TryGetValue(typeof(T), out ISystem system))
        {
            throw new Exception($"System {system} is not registered yet");
        }

        return (T)system;
    }

    public static void Shutdown()
    {
        foreach (ISystem system in _systems.Values)
        {
            system.Shutdown();
        }
    }
}
