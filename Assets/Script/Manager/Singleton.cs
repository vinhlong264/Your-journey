using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance = null;
    protected static bool applicationIsQuitting = false;
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }



            if (_instance == null)
            {
                if (FindObjectOfType<T>() != null)
                {
                    _instance = FindObjectOfType<T>();
                }
                else
                {
                    new GameObject().AddComponent<T>().name = "Singleton_" + typeof(T).ToString();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        MakeSingleton(false);
    }

    protected void MakeSingleton(bool isDontDestroy)
    {
        if (!isDontDestroy)
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this.GetComponent<T>();
            }
        }
        else
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this.GetComponent<T>();
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }

    protected virtual void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }

}
