using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;          // e.g. "Card","Enemy", "Bullet", "Coin"
    public GameObject prefab;   // prefab to pool
    public int size = 10;       // initial pool size
}

public class ObjectPooler : Singleton<ObjectPooler>
{
    // public static ObjectPooler Instance;


    [Header("Pool Configs")]
    public List<Pool> pools = new List<Pool>();

    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    private void Start()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);

                // Optional but keeps tag consistent
                obj.tag = pool.tag;

                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.tag, objectPool);
        }
    }

    /// <summary>
    /// Spawn an object from pool by tag
    /// </summary>
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"[ObjectPooler] Pool with tag {tag} doesn't exist.");
            return null;
        }

        Queue<GameObject> poolQueue = _poolDictionary[tag];
        GameObject objectToSpawn;

        if (poolQueue.Count == 0)
        {
            // Optional: auto-expand pool if empty
            Pool config = pools.Find(p => p.tag == tag);
            if (config == null)
            {
                Debug.LogError($"[ObjectPooler] No Pool config found for tag {tag}");
                return null;
            }

            objectToSpawn = Instantiate(config.prefab);
            objectToSpawn.tag = tag;
        }
        else
        {
            objectToSpawn = poolQueue.Dequeue();
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.SetPositionAndRotation(position, rotation);

        return objectToSpawn;
    }

    /// <summary>
    /// Return object back to its pool using its tag
    /// </summary>
    public void ReturnToPool(GameObject obj)
    {
        string tag = obj.tag;

        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"[ObjectPooler] No pool found for tag {tag}. Destroying object.");
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        _poolDictionary[tag].Enqueue(obj);
    }
}
