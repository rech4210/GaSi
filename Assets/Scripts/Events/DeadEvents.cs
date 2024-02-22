using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeadEvents : Events<DeadEvents>
{
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

        //StageManager.Instance.SwichStage(); //이부분 스테이지 변경하기
        // 죽음 이벤트 너무 여러번 발동됨.
        //StageManager.Instance.SwichStage();
    }
}
