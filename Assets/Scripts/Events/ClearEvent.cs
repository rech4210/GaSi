using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum ClearFlag : int
{
    notClear = 0, clear = 1
}
public class ClearEvent : Events<ClearEvent>, IEventHandler<ClearEvent>
{
    [SerializeField] private GameObject clearPopUp;
    bool isClear = false;
    public ClearFlag clearFlag = ClearFlag.notClear;

    protected override void ActionInitiallize()
    {
        clearPopUp.SetActive(false);
        clearFlag = ClearFlag.notClear;
        if (OnExecute?.Method == null)
        {
            OnExecute += Clear_1;
            OnExecute += Clear_2;
        }
    }

    private void Clear_1()
    {
        clearFlag = ClearFlag.clear;
        Time.timeScale = 0f;
        //Timer에 시간 정지
        clearPopUp.SetActive(true);
        // 클리어 UI 출력
        StartCoroutine(WaitClearInput());
    }
    private void Clear_2()
    {
        // 이 부분이 sceneLoaded 함수랑 겹칠 염려가 있습니다.
        clearPopUp.SetActive(false);
        Time.timeScale = 1f;
        // 이걸 쓰려면 타이머 초기화해야합니다
        //clearFlag = ClearFlag.notClear;
        //StageManager.Instance.SwitchStage();
    }


    IEnumerator WaitClearInput()
    {
        yield return new WaitUntil(() => Input.anyKeyDown);
    }

    public void Event()
    {
        throw new System.NotImplementedException();
    }
}
