using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    [SerializeField] protected GameObject player;
    Transform playerTransform;
    Action GameInitiallzie;

    public Transform _PlayerTransform { get { return playerTransform; } }

    private void Awake()
    {
        Time.timeScale = 1f;
        // �������� �ʱ�ȭ, �÷��̾� ���� �ʱ�ȭ, ��� ó�����ֱ�
        //GameInitiallzie += test1;

        var obj = Instantiate(player, new Vector3(0,.5f,0),Quaternion.identity);
        obj.SetActive(true);
        playerTransform = obj.transform;
    }

    private void Do<T>(IEventHandler<T> eventHandler) where T : Events<T>
    {
        eventHandler.Event();
    }

    //void test1()
    //{
    //    GameInitiallzie.Invoke();
    //}

    /*123
     * 1. ���� �ʱ�ȭ ���� => ���� �Ŵ����κ��� �ʱ�ȭ (���� ����, �÷��̾� ����, ���������� �ʱⰪ �� �ߵ��� ����)
     * 2. ���ӿ� �ʿ��� ������ ���� (������ �Ŵ����� ���� �ֽ�ȭ)
     * 3. ���� ���ణ �߻��ϴ� �̺�Ʈ ( �ð� �̺�Ʈ/ ���� �̺�Ʈ) �з� �� �׼� ����
     * 4. ���� Ŭ���� �� ���н� �б� (ClearEvent,DeadEvent �б�, TransitionEvent���..)
     * 5. ���� �������� ����.
    123*/
}
