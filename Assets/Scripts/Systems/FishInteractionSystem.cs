using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Taking responsibility for updating fishing state
/// </summary>
public class FishInteractionSystem : MonoBehaviour, ISystem
{
    [SerializeField] private List<Fish> _allFishes;
    public bool IsInitialized => _spawnSystem != null;

    public event System.Action<float> OnFishBitTheBait;
    public event System.Action<Fish> OnFishBit;
    public event System.Action<bool> OnFishingFinished;

    private SpawnSystem _spawnSystem;
    private FishingProgressSystem _progressSystem;
    private Fish _currentFish;

    private Coroutine _followDelayCoroutine;

    public void Initialize()
    {
        _spawnSystem = SystemsContainer.GetSystem<SpawnSystem>();
    }

    public void Shutdown() { }

    public void SetupFish(List<BaseBait> baits, Transform parent)
    {
        float baitAttractiveness = 0;
        // TODO: create configs system, choose random fish
        if (baits == null || baits.Count == 0)
        {
            return;
        }

        foreach (BaseBait bait in baits)
        {
            baitAttractiveness += bait.AttractivenessStrenght;
        }

        if (_currentFish == null)
        {
            int randomIndex = Random.Range(0, _allFishes.Count);
            Fish prefab = _allFishes[randomIndex];
            _currentFish = _spawnSystem.CreateFish(prefab, parent);
        }

        float delay = _currentFish.Data.BehaviourData.BitDelayRange.random / baitAttractiveness;

        _followDelayCoroutine = StartCoroutine(WaitUntillFollowCo(delay));
    }

    private IEnumerator WaitUntillFollowCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        _currentFish.Controller.Initialize();
    }

    private void OnFishingFinishedHandler(bool result)
    {
        _currentFish.Controller.Shutdown();
        _progressSystem.OnFishingFinished -= OnFishingFinishedHandler;
        _currentFish = null;
        OnFishingFinished?.Invoke(result);
    }

    public void InvokeOnFishBiteTheBait(float strength)
    {
        OnFishBitTheBait?.Invoke(strength);
    }

    public void InvokeOnFishBit(Fish fish)
    {
        OnFishBit?.Invoke(fish);
        //TODO: resolve conflict with FishingProgressSystem Init
        _progressSystem = SystemsContainer.GetSystem<FishingProgressSystem>();
        _progressSystem.OnFishingFinished += OnFishingFinishedHandler;
    }

    public void AbortFishingProcess()
    {
        if (_followDelayCoroutine != null)
        {
            StopCoroutine(_followDelayCoroutine);
            _followDelayCoroutine = null;
        }

        if (_currentFish != null)
        {
            _currentFish.Controller.Shutdown();
        }
    }
}
