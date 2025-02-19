using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Taking responsibility for track inputs
/// </summary>
public class InputSystem : MonoBehaviour, ISystem
{
    [SerializeField] private InputActionAsset _inputActionAsset;
    private Dictionary<string, InputAction> _inputActions = new();

    public bool IsInitialized => _inputActionAsset.enabled;

    public void Initialize()
    {
        InputActionMap map = _inputActionAsset.actionMaps[0];

        foreach (InputAction action in map.actions)
        {
            _inputActions.Add(action.name, action);
        }
        _inputActionAsset.Enable();
    }

    public InputAction GetInputAction(string name)
    {
        if (_inputActions.TryGetValue(name, out InputAction action))
            return action;

        throw new System.Exception($"Input action with name {name} not found");
    }

    public void Shutdown() { }
}
public static class Inputs
{
    public const string CASTING_NAME = "Casting";
}
