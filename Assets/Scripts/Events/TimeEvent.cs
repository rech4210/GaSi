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

    // ON DESTORY ��ü�� ���� ���ε� �Ǵ��� Start�� ó���� �ߵ���
    private void Start()
    {
        //timeWorkTask = Task.Run(() => { SendMessagePerSecond();});
        perSecondTimeEventLists = new List<ITimeEvent>();
        //SceneManager.sceneLoaded += SceneManager_sceneLoaded;

    }
    // �� �κ� ���� ��ü�ؾ��ϳ�..? �ƴϸ� onenable�� �ؾ��ϳ�
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
        Debug.Log($"���� Ÿ�� �̺�Ʈ ����� ���� : {perSecondTimeEventLists.Count}");
    }

    // �ʴ� �θ��� �δ��� ũ��������?
    // ����� ������ �����ϰų�, �ʴ��� ȣ���� �ƴ� �ٸ� �̺�Ʈ ȣ�� ������ ����Ͽ��� �ҵ��ϴ�.
    // ����� ������ ITimeEvent�� �ð����� �־��ְ� �̰� �񼭳ฮ�� ������ �����ϸ� ���?

    /*��ųʸ��� �ش� �Լ��� �ߵ��ǵ��� ������ �÷԰��� �־�ΰ� �ش� ������ �Ϸ�ɽ� �����Ű�� �½�ũ�� �����
     �� ��ä (Bullet Turret ���..)�½�ũ�� ������ �ڷᱸ���� ����� ���? Task�� �Ϸ�Ǹ� ���� bool ���� ����Ǵ� ����
     ex) Dic*/
    /*Dictionary<ITimeEvent, float> eventDict;
    ForEach -> task if(float > ItimeEventLocalFloat)
        => bool = true*/

    //Timer���� �ð� �� ����
    //TimeEvent������ ���� ��ü������ �ð� ���� ȣ���� �����.
    //TimeEvent�� ȣ��� �̺�Ʈ���� �з��صξ�� �� (ex) ���ü�, ȣ�� ��)
    //ITimer�� �����޾� �Լ��� ����� TimeEvent���� �з��� ��ü���� ��� ȣ����


    // �̺κ��� scene reload�� �ƴҶ� ȣ��Ǹ鼭 �ı��� ��ü�� ������
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
