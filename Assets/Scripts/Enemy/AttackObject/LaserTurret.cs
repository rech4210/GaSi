using System.Collections;
using UnityEngine;

public class LaserTurret : AttackFunc<LaserTurret>
{

    private void FixedUpdate()
    {
        if (Player == null)
        {
            transform.rotation = Quaternion.identity;
            return;
        }

        transform.rotation = new Quaternion
            (transform.rotation.x, ChaseTarget(Player, this.gameObject).y
            , transform.rotation.z, ChaseTarget(Player, this.gameObject).w);
    }
    public override void CalcStat(AttackStatus status, AttackCardInfo info)
    {
        //이거 제대로 작동하나? 매우 딱딱한 구조다
        switch (info.attackCardEnum)
        {
            case AttackCardEnum.duration:
                _Duration *= status.duration;
                break;
            case AttackCardEnum.scale:
                _Scale *= status.scale;
                break;
            case AttackCardEnum.point:
                _Point *= status.point;
                break;
            case AttackCardEnum.speed:
                _Speed *= status.speed;
                break;
            default: break;
        }
    }

  
    void Start()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        while (true)
        {
            ExcuteAttack();
            yield return new WaitForSeconds(AttackStatus.duration);
        }
    }
    protected override void ExcuteAttack()
    {
        if(Player == null)
        {
            return;
        }
        var atkobj = Instantiate(attackObject,transform.position,Quaternion.identity);
        atkobj.transform.position = Player? Player.transform.position : Vector3.zero;
        atkobj.GetComponent<AtkObjStat<LaserObj>>().Initialize(AttackStatus, sk_1, sk_2, sk_3);
        atkobj.GetComponent<AtkObjStat<LaserObj>>().SetDamageAction(() =>
        {
            playerInteraction.GetDamaged(_Point * 0.1f);
        });
    }

    public override void TimeEvent(float time)
    {
        Debug.Log(time + this.gameObject.name);
        if (time > 15f)
        {
            Debug.Log("스킬 발동");
        };

    }
}
