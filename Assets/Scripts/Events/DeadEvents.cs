using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeadEvents : Events<DeadEvents>
{
    public override void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        throw new System.NotImplementedException();
    }
    private void Start()
    {
        
    }

    protected override void ActionInitiallize()
    {
        if (OnExecute?.Method == null)
        {
            OnExecute += Dead_1;
            OnExecute += Dead_2;
        }
    }


    private void Dead_1()
    {
        DataManager.Instance._playerTransform.gameObject.SetActive(false);
        TimeEvent.Instance.ExecuteEvent();
    }
    private void Dead_2()
    {
        Debug.Log("Dead_2");
        StageManager.Instance.Reload();
        DataManager.Instance._playerTransform.gameObject.SetActive(true);

        //StageManager.Instance.SwichStage(); //�̺κ� �������� �����ϱ�
        // ���� �̺�Ʈ �ʹ� ������ �ߵ���.
        //StageManager.Instance.SwichStage();
    }
}
