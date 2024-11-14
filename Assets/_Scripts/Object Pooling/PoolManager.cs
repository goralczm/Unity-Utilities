using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Utilities.Utilities.Core;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private Pool[] _pools;

    [SerializeField] private Dictionary<string, ObjectPool<GameObject>> _cachedPools = new Dictionary<string, ObjectPool<GameObject>>();

    protected override void Awake()
    {
        base.Awake();

        foreach (Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            ObjectPool<GameObject> newPool = new ObjectPool<GameObject>(() =>
            {
                GameObject newPoolObject = Instantiate(pool.prefab);
                newPoolObject.AddComponent<PoolObject>().Init(pool.tag);
                return newPoolObject;
            },
                pooledObject =>
                {
                    pooledObject.SetActive(true);
                },
                pooledObject =>
                {

                },
                pooledObject =>
                {
                    Destroy(pooledObject.gameObject);
                },
                false,
                pool.size
            );

            _cachedPools.Add(pool.tag, newPool);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            SpawnFromPool(_pools[0].tag, Vector2.zero, Quaternion.identity);
    }

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot, Transform parent = null)
    {
        if (!_cachedPools.ContainsKey(tag))
            return null;

        GameObject pooledObject = _cachedPools[tag].Get();

        pooledObject.transform.SetPositionAndRotation(pos, rot);
        pooledObject.transform.SetParent(parent);

        return pooledObject;
    }

    public void ReleaseObject(string tag, GameObject obj)
    {
        _cachedPools[tag].Release(obj);
    }
}
