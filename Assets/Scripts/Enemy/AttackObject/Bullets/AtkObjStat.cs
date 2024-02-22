using KMS.Player.PlayerInteraction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AtkObjStat<T> : MonoBehaviour  where T : AtkObjStat<T>
{
    protected float lifeTime;

    protected float speed;
    protected float point;
    protected float duration;
    protected float scale;

    protected Action damageAction;


    public float Point { get { return point;} }
    protected void GetAtkObjPoint(AttackStatus attackStatus)
    {
        this.point = attackStatus.point;
        this.speed = attackStatus.speed;
        
        //속도도 관리해줘야함
    }

    public virtual void Initialize(AttackStatus attackStatus) { GetAtkObjPoint(attackStatus); }
    public virtual void Initialize(AttackStatus attackStatus, bool skill_1) { GetAtkObjPoint(attackStatus); }
    public virtual void Initialize(AttackStatus attackStatus, bool skill_1, bool skill_2) { GetAtkObjPoint(attackStatus); }
    public virtual void Initialize(AttackStatus attackStatus, bool skill_1, bool skill_2, bool skill_3) { GetAtkObjPoint(attackStatus); }
    public virtual void Initialize(AttackStatus attackStatus, bool skill_1, bool skill_2, bool skill_3, bool skill_4) { GetAtkObjPoint(attackStatus); }

    public void SetDamageAction(Action action) { damageAction = action;}

    public virtual void OnHitTarget()
    {
        //Destroy(gameObject);
    }


}
