using UnityEngine;


public class PlayerStat : MonoBehaviour
{
    PlayerStatStruct playerStat;


    // this section must be proteted so it will be use action type
    public void UpdatePlayerStat(PlayerStatStruct stat) // here data is buff or attack debuff
    {
        playerStat = stat;
        playerStat.PrintPlayerStat();
        DataManager.Instance.UpdatePlayerData(playerStat,this);
    }
    public void GetDamaged(float dmg)
    {
        playerStat.health -= dmg;
        Debug.Log(playerStat.health);
        if (playerStat.health <= 0f)
        {
            isLive = false;

            DeadEvents.Instance.ExecuteEvent();
        }
    }
    bool isLive = true;

    private void Awake()
    {
        
    }
    void OnEnable() // �÷��̾� ������ �����ϱ����� ó����. Awake �Ǵ� manager���� ȣ���ϵ��� ����?
    {
        DataManager.Instance.PlayerStatDele = UpdatePlayerStat; // this calc need
        DataManager.Instance.PlayerStatDele?.Invoke(new PlayerStatStruct(10, 3, 15, 0, 0, 0, 0,0,0)); 
        // �̺κ� �������� �������� �ʱ�ȭ
    }
    private void OnDisable()
    {
        DataManager.Instance.PlayerStatDele -= UpdatePlayerStat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Background")) { }
        else if(isLive)
        {
            //����ó��?
            var attackObject = other.gameObject.GetComponent<AtkObjStat>();
            GetDamaged(attackObject.Point);
            attackObject.OnHitTarget();
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
