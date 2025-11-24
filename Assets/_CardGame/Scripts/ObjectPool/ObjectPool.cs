// ObjectPool.cs
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Queue<T> _pool = new Queue<T>();

    public ObjectPool(T prefab, Transform parent, int initialSize = 0)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        T instance;
        if (_pool.Count > 0)
        {
            instance = _pool.Dequeue();
        }
        else
        {
            instance = GameObject.Instantiate(_prefab, _parent);
        }

        instance.gameObject.SetActive(true);
        return instance;
    }

    public void ReturnToPool(T instance)
    {
        instance.gameObject.SetActive(false);
        _pool.Enqueue(instance);
    }

    public void Clear()
    {
        foreach (var obj in _pool)
        {
            if (obj != null)
                GameObject.Destroy(obj.gameObject);
        }
        _pool.Clear();
    }
}
