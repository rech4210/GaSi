using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Manager<StageManager>
{
    [SerializeField] private int currentStageNum = 0;
    [SerializeField] private GameObject currentStage = null; //static���� �����ϸ� �ذ��.
    // don't destroy ��ü �ȿ� ������ ����� ����.
    [SerializeField] GameObject[] stagesArray = new GameObject[10];

    private void Awake()
    {
        //currentStageNum�� �޾ƿ���
        currentStage = Instantiate(stagesArray[currentStageNum]);
        currentStage.SetActive(true);
        //currentStage = obj;
    }


    public void SwichStage()
    {
        currentStage?.SetActive(false);
        currentStageNum++;
        currentStage = Instantiate(stagesArray[currentStageNum]);
        currentStage.SetActive(true);
    }
    public void Reload()
    {
        SceneManager.LoadScene(currentStageNum);
    }

    public Transform GetCurrentStagePos()
    {
        return currentStage.transform;
    }
}
