using UnityEngine;

/// <summary>
/// Taking responsibility for control spawn factories
/// </summary>
public class SpawnSystem : MonoBehaviour, ISystem
{
    [Header("Fishing Pole")]
    [SerializeField] private Transform _poleSpawnPoint;
    [SerializeField] private GameObject _pole;
    [SerializeField] private GameObject _fishingReel;
    [SerializeField] private GameObject _fishingLine;
    [SerializeField] private GameObject _hook;
    [SerializeField] private GameObject _bobber;

    private FishingPoleFactory _fishingPoleFactory;
    private FishFactory _fishFactory;

    public bool IsInitialized => _fishingPoleFactory != null;

    public void Initialize()
    {
        _fishingPoleFactory = new FishingPoleFactory();
        _fishFactory = new FishFactory();
    }

    public void CreateFishingPole()
    {
        _fishingPoleFactory.CreateFishingPole(_pole, _fishingReel, _fishingLine, _hook, _bobber, _poleSpawnPoint);
    }

    public Fish CreateFish(Fish prefab, Transform parent)
    {
        return _fishFactory.CreateFish(prefab, parent);
    }

    public void Shutdown()
    {
        _fishingPoleFactory.RemoveFishingPole();
    }
}
