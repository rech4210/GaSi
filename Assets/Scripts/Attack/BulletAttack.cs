using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : AttackGenerater
{
    private void Start()
    {
        attackStatus = new AttackStatus(1,5,5f,1f);
        Debug.Log($"���� ����� �±״� {GetAttackType()} + {duration}");

    }

    AttackType attackType = AttackType.bullet;
    public override AttackType GetAttackType()
    {
        return attackType;
    }

    public override void AttackDamage()
    {
        throw new NotImplementedException();
    }

    public override void AttackDuration()
    {
        throw new NotImplementedException();
    }

    public override void AttackRange()
    {
        throw new NotImplementedException();
    }
}
