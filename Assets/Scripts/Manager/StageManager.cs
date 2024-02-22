using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Manager<StageManager>
{
    // stage도 don't 해야하지 ㅇ않을까
    [SerializeField] private int currentStageNum = 2;
    [SerializeField] private GameObject currentStage = null; //static으로 설정하면 해결됨.
    // don't destroy 개체 안에 들어갔을때 생기는 문제.
    [SerializeField] GameObject[] stagesArray = new GameObject[10];

    private void Awake()
    {
        currentStageNum = 0;
        //currentStageNum을 받아오기
        currentStage = Instantiate(stagesArray[currentStageNum]);
        currentStage.SetActive(true);
        //currentStage = obj;
    }


    public void SwitchStage()
    {
        currentStage?.SetActive(false);
        currentStageNum++;
        currentStage = Instantiate(stagesArray[currentStageNum]);
        // 이부분 스테이지 로드로 바꿀까? => 이유, 씬이 변경될때 하던 동작들이 제대로 동작안함
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
