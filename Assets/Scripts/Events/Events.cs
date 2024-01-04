using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Events<T> :MonoBehaviour, ISceneLoaded where T : Events<T>
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
    protected System.Action OnExecute;

    protected abstract void ActionInitiallize();
    public virtual void ExecuteEvent()
    {
        OnExecute?.Invoke();
    }

    public abstract void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1);
    // Manager������ �̸� �������� �ʴµ�, Event�� Manager �� ������ ������ �ƴϸ� �ϳ��� �����ϴ°� �´�.
}


