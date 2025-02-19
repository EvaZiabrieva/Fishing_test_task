using UnityEngine;

/// <summary>
/// The application's starting point that handles system initialization/shutdown and process execution
/// </summary>
public class ApplicationStartUp : MonoBehaviour
{
    [SerializeField] private GameObject _systemsHolder;

    private void Awake()
    {
        SystemsContainer.Initialize(_systemsHolder.GetComponents<ISystem>());
    }

    private void Start()
    {
        SystemsContainer.GetSystem<SpawnSystem>().CreateFishingPole();
    }

    private void OnDestroy()
    {
        SystemsContainer.Shutdown();
    }
}
