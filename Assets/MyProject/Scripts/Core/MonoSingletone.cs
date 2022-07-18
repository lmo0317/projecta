using UnityEngine​;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance = null;
    public static T Instance
    {
        get
        {
            instance = FindObjectOfType(typeof(T)) as T;
            
            if (instance == null)
            {
                instance = new GameObject("@" + typeof(T).ToString(),
                                           typeof(T)).AddComponent<T>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
}