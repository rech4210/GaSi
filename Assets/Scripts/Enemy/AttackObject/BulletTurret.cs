using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BulletTurret : AttackFunc<BulletTurret>
{
    //1. gen에서 플레이어 정보있음
    //2. 새로 오브젝트 생성될때 gen 정보를 넘겨주기
    //3. 넘겨받은 정보로 action을 instantiate 시킬때 전달시켜주기

    
    private void FixedUpdate()
    {
        if (Player == null)
        {
            transform.rotation = Quaternion.identity;
            return;
        }

        transform.rotation = Quaternion.identity;
        transform.rotation = quaternion.identity;
        transform.rotation = new Quaternion
            (transform.rotation.x,ChaseTarget(Player,this.gameObject).y
            ,transform.rotation.z, ChaseTarget(Player, this.gameObject).w);
    }
    public override void CalcStat(AttackStatus status, AttackCardInfo info)
    {
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
        //Instantiate(attackObject,transform);
        if (Player == null)
        {
            return;
        }
        var atkobj = Instantiate(attackObject, transform.position + transform.forward ,transform.rotation);
        atkobj.GetComponent<AtkObjStat<BulletObj>>().Initialize(AttackStatus, sk_1, sk_2, sk_3);
        atkobj.GetComponent<AtkObjStat<BulletObj>>().SetDamageAction(() =>
        {
            playerInteraction.GetDamaged(_Point);
        });
        //atkobj.GetComponent<AtkObjStat<BulletObj>>().SetDamageAction();
    }

    public override void TimeEvent(float time)
    {
        Debug.Log(time + this.gameObject.name);
    }
}