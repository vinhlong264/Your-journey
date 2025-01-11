using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
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
            if (_instance != null)
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
            if (_instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this.GetComponent<T>();
                DontDestroyOnLoad(this);
            }
        }
    }

}
