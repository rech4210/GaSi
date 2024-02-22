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
        // 스테이지 초기화, 플레이어 상태 초기화, 등등 처리해주기
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
     * 1. 게임 초기화 설정 => 게임 매니저로부터 초기화 (게임 상태, 플레이어 상태, 스테이지등 초기값 및 중도값 제어)
     * 2. 게임에 필요한 데이터 설정 (데이터 매니저의 정보 최신화)
     * 3. 게임 진행간 발생하는 이벤트 ( 시간 이벤트/ 조건 이벤트) 분류 및 액션 제어
     * 4. 게임 클리어 및 실패시 분기 (ClearEvent,DeadEvent 분기, TransitionEvent등등..)
     * 5. 다음 스테이지 관리.
    123*/
}
