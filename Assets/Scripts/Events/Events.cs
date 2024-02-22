using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Events<T> :MonoBehaviour where T : Events<T>
{
    protected void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(this.gameObject);
        }
        ActionInitiallize();
    }

    private static  T instance;
    public static T Instance {
        get 
        {
            if(instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
            }
            return instance;
        } 
    }
    public System.Action OnExecute;

    protected abstract void ActionInitiallize();
    public virtual void ExecuteEvent()
    {
        OnExecute?.Invoke();
    }

}


