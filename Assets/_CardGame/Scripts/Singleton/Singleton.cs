using UnityEngine;

/// <summary>
/// Generic Singleton base class for MonoBehaviours.
/// Ensures only one instance exists in the scene.
/// Automatically creates a GameObject if none exists.
/// </summary>
/// <typeparam name="T">Type of the singleton class.</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool _applicationIsQuitting;

    /// <summary>
    /// Gets the instance of the singleton. 
    /// If no instance exists, tries to find one or create a new GameObject.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting) return null;

            if (_instance == null)
            {
                // Try to find existing instance
                _instance = FindObjectOfType<T>();

                // If not found, create new one
                if (_instance == null)
                {
                    var obj = new GameObject(typeof(T).Name + " (Singleton)");
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// Ensures only one instance exists. 
    /// Destroys duplicates automatically.
    /// </summary>
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Marks the singleton as quitting to prevent creation during shutdown.
    /// </summary>
    protected virtual void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
    }
}
