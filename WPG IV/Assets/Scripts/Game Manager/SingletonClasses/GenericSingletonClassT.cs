//THIS IS SINGLETON CLASS

using UnityEngine;

public class GenericSingletonClass<T> : MonoBehaviour where T : Component
{
    private static T instance = null;

    private static readonly object padlock = new object();

    public static T Instance 
    {
        get 
        {
            lock (padlock)
            {
                if (instance == null) 
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null) 
                    {
                        GameObject obj = new GameObject ();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                        Debug.LogWarning("created another instance of "+instance.GetType()+" inside of "+obj.name+" using getter");
                    }
                }
            }
            return instance;
        }
    }
 
    public virtual void Awake()
    {
        lock (padlock)
        {
            if (instance == null) 
            {
                instance = this as T;
                //DontDestroyOnLoad (this.gameObject);
                Debug.Log("new instance of "+instance.GetType()+" inside of "+gameObject.name);
            } 
            else 
            {
                Destroy (gameObject);
                Debug.LogWarning("another instance of "+instance.GetType()+" inside of "+gameObject.name);
            }
        }
    }
}
