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
        //Timer�� �ð� ����
        clearPopUp.SetActive(true);
        // Ŭ���� UI ���
        StartCoroutine(WaitClearInput());
    }
    private void Clear_2()
    {
        // �� �κ��� sceneLoaded �Լ��� ��ĥ ������ �ֽ��ϴ�.
        clearPopUp.SetActive(false);
        Time.timeScale = 1f;
        // �̰� ������ Ÿ�̸� �ʱ�ȭ�ؾ��մϴ�
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
