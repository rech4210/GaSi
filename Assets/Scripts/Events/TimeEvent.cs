using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeEvent : Events<TimeEvent>
{
    float savedTime;

    List<ITimeEvent> perSecondTimeEventLists;
    List<ITimeEvent> perHMiniteTimeEventLists;
    Action<ITimeEvent> timeAction;

    Task timeWorkTask;

    protected override void ActionInitiallize()
    {
        if (OnExecute?.Method == null)
        {
            OnExecute += Time_1;
            OnExecute += Time_2;
        }
    }

    // ON DESTORY 개체는 씬이 리로드 되더라도 Start가 처음만 발동함
    private void Start()
    {
        //timeWorkTask = Task.Run(() => { SendMessagePerSecond();});
        perSecondTimeEventLists = new List<ITimeEvent>();
        //SceneManager.sceneLoaded += SceneManager_sceneLoaded;

    }
    // 이 부분 씬을 교체해야하나..? 아니면 onenable로 해야하나
    //public override void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    //{
    //    perSecondTimeEventLists.Clear();
    //    Debug.Log("scene cleared");
    //}

    public void StoreTimeEventObj(GameObject obj)
    {
        var eventObj = obj.GetComponent<ITimeEvent>();
        perSecondTimeEventLists.Add(eventObj);
        CurrentListData();
    }
    public void RemoveTimeEventObj(GameObject obj)
    {
        var eventObj = obj.GetComponent<ITimeEvent>();
        perSecondTimeEventLists.Remove(eventObj);
        CurrentListData();
    }


    public void CurrentListData()
    {
        Debug.Log($"현재 타임 이벤트 요소의 개수 : {perSecondTimeEventLists.Count}");
    }

    // 초당 부르면 부담이 크지않을까?
    // 대상의 범위를 제한하거나, 초단위 호출이 아닌 다른 이벤트 호출 조건을 사용하여야 할듯하다.
    // 대상이 가지는 ITimeEvent에 시간값을 넣어주고 이걸 딕서녀리나 셋으로 관리하면 어떨기?

    /*딕셔너리에 해당 함수가 발동되도록 딜레이 플롯값을 넣어두고 해당 내용이 완료될시 실행시키는 태스크를 만들고
     한 개채 (Bullet Turret 등등..)태스크를 관리할 자료구조를 만들면 어떨까? Task가 완료되면 로컬 bool 값이 변경되는 형식
     ex) Dic*/
    /*Dictionary<ITimeEvent, float> eventDict;
    ForEach -> task if(float > ItimeEventLocalFloat)
        => bool = true*/

    //Timer에서 시간 값 추출
    //TimeEvent에서는 게임 전체에서의 시간 관련 호출을 담당함.
    //TimeEvent에 호출될 이벤트들을 분류해두어야 함 (ex) 동시성, 호출 빈도)
    //ITimer을 구현받아 함수를 만들고 TimeEvent에서 분류된 개체들을 모두 호출함


    // 이부분이 scene reload가 아닐때 호출되면서 파괴된 개체에 접근함
    public void SendMessagePerSecond(float time)
    {
        for (int i = 0; i < perSecondTimeEventLists.Count; i++)
        {
            //get now time in here
            perSecondTimeEventLists[i].TimeEvent(time);
        } 
    }

    private void Time_1()
    {
        Debug.Log("Time_1");
        Time.timeScale = 0;
    }
    private void Time_2()
    {
        Debug.Log("Time_2");
    }
    public void Save(float time)
    {
        savedTime = time;
    }
}
