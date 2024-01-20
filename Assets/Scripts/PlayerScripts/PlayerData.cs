using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    PlayerStatStruct playerStat;

    // 버프 -> 데이터매니저 -> 플레이어 -> 데이터 매니저

    // this section must be proteted so it will be use action type
    public void UpdatePlayerStat(PlayerStatStruct stat) // here data is buff or attack debuff
    {
        playerStat = stat;
        //playerStat.PrintPlayerStat();
        DataManager.Instance.UpdatePlayerData(playerStat,this);
    }
    bool isLive = true;
    public void GetDamaged(float dmg)
    {
        playerStat.health -= dmg;
        Debug.Log(playerStat.health);
        if (playerStat.health <= 0f && isLive)
        {
            isLive = false;
            DeadEvents.Instance.ExecuteEvent();
        }
    }

    private void Awake()
    {
        DataManager.Instance.PlayerStatDele = null;
        DataManager.Instance.PlayerStatDele = UpdatePlayerStat; // this calc need
        DataManager.Instance.PlayerStatDele?.Invoke(new PlayerStatStruct(10, 3, 15, 0, 0, 0, 0, 0, 0));
    }
    void OnEnable() // 플레이어 추적을 실행하기위해 처리함. Awake 또는 manager에서 호출하도록 제어?
    {
        isLive = true;

        // 이부분 스테이지 생성마다 초기화
    }
    private void OnDisable()
    {
        DataManager.Instance.PlayerStatDele = null;
    }

    private void OnTriggerEnter<T>(Collider other) where T : AtkObjStat<T>
    {
        if (other.gameObject.CompareTag("Background")) { }
        else if(isLive)
        {
            //예외처리?
            var attackObject = other.gameObject.GetComponent<AtkObjStat<T>>();
            //이 부분 수정하셈 두개가 겹치고있음.
            GetDamaged(attackObject.Point *15);
            attackObject.OnHitTarget(); // 이 부분 액션으로 넘겨주면 좋을듯 (레이저가 타겟 개체를 확인해야할때)
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Background")) { }

    //    else 
    //    {
    //        Destroy(collision.gameObject);
    //        GetDamaged(atk.point);
    //    }
    //}
}
