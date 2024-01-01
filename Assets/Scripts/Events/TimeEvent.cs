using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class TimeEvent : Events<TimeEvent>
{
    float time;

    List<ITimeEvent> perSecondTimeEventLists;
    List<ITimeEvent> perHMiniteTimeEventLists;
    Action<ITimeEvent> timeAction;

    Task timeWorkTask;

    protected override void Execute()
    {
        if (OnExecute?.Method == null)
        {
            OnExecute += Time_1;
            OnExecute += Time_2;
        }
    }

    private void Start()
    {
        //timeWorkTask = Task.Run(() => { SendMessagePerSecond();});
        perSecondTimeEventLists = new List<ITimeEvent>();
    }

    public void StoreTimeEventObj(ITimeEvent @event)
    {
        perSecondTimeEventLists.Add(@event);
        foreach (var item in perSecondTimeEventLists)
        {
            Debug.Log(item);
        }
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

    public void SendMessagePerSecond()
    {
        for (int i = 0; i < perSecondTimeEventLists.Count; i++)
        {
            //get now time in here
            perSecondTimeEventLists[i].TimeEvent(Time.time);
        } 
    }

    private void Time_1()
    {
        Debug.Log("Time_1");
        //Time.timeScale = 0;
    }
    private void Time_2()
    {
        Debug.Log("Time_2");
    }
    public void SaveTime(float time)
    {
        this.time = time;
    }
}
