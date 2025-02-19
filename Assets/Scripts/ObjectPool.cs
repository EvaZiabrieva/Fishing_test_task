using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can be used to create objects pool
/// </summary>
public class ObjectPool : IDisposable
{
    private GameObject _prefab;
    private Queue<GameObject> _pool;

    public ObjectPool(GameObject prefab)
    {
        _prefab = prefab;
        _pool = new Queue<GameObject>();
    }

    public ObjectPool(GameObject prefab, int prespawnCount)
    {
        _prefab = prefab;
        _pool = new Queue<GameObject>(prespawnCount);

        for (int i = 0; i < prespawnCount; i++)
        {
            GameObject pooled = CreateInstance();
            ReturnToPool(pooled);
        }
    }

    public GameObject GetFromPool()
    {
        if (_pool.TryDequeue(out GameObject pooled))
        {
            pooled.SetActive(true);
            return pooled;
        }

        return CreateInstance();
    }

    public void ReturnToPool(GameObject toAdd)
    {
        toAdd.SetActive(false);
        _pool.Enqueue(toAdd);
    }

    private GameObject CreateInstance()
    {
        GameObject instance = GameObject.Instantiate(_prefab);
        return instance;
    }

    public void Dispose()
    {
        foreach (GameObject item in _pool.ToArray())
        {
            GameObject.Destroy(item);
        }
    }
}

public static class PoolsContainer
{
    private static Dictionary<GameObject, ObjectPool> _pools = new();

    public static GameObject GetFromPool(GameObject prefab)
    {
        if (_pools.TryGetValue(prefab, out ObjectPool pool))
        {
            return pool.GetFromPool();
        }

        return CreateNewPool(prefab).GetFromPool();
    }

    public static void ReturnToPool(GameObject prefab, GameObject pooled)
    {
        if (_pools.TryGetValue(pooled, out ObjectPool pool))
        {
            pool.ReturnToPool(pooled);
        }
        else
        {
            CreateNewPool(prefab).ReturnToPool(pooled);
        }
    }

    private static ObjectPool CreateNewPool(GameObject prefab)
    {
        ObjectPool pool = new ObjectPool(prefab);
        _pools.Add(prefab, pool);
        return pool;
    }
}
