using UnityEngine;

[DisallowMultipleComponent]
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    protected static bool IsApplicationQuitting { get; private set; } = false;
    private static object lockObj = new object();

    private const string NAME_SUFFIX = " (Singleton)";

    [SerializeField] bool dontDestroyOnLoad = false;

    #region Unity
    virtual protected void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<T>();
            if (!gameObject.name.EndsWith(NAME_SUFFIX))
                gameObject.name += NAME_SUFFIX;
        }
        else if (instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }

    virtual protected void OnApplicationQuit() => IsApplicationQuitting = true;
    virtual protected void OnDestroy() { if (instance == this) instance = null; }

    #endregion

    public static T Instance
    {
        get
        {
            if (IsApplicationQuitting) { return null; }

            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        GameObject go = new GameObject();
                        instance = go.AddComponent<T>();
                        go.name = typeof(T).ToString() + NAME_SUFFIX;
                    }
                }
                return instance;
            }
        }
    }
}
