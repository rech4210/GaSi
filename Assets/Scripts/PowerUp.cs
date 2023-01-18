using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Buff
{

    private void Start()
    {
        FindBuffManager(buffManager);
        Init();
    }

    public override void OnChecked()
    {
        buffManager.GetBuff(this);
    }
    
    public override void BuffUp()
    {
        base.point = 5;
        Debug.Log($"������ ����{point}");
    }

    public override void BuffUse()
    {
        base.point = 15;
        Debug.Log($"���� ���� ��� : {this.GetType()}");
    }

    public override void Init()
    {
       
    }

    public override void RankUp(int rank)
    {
        base.rank = rank;
    }
    public override void BuffDown()
    {
        Debug.Log($"���� ���� : {this.GetType()}");
    }
}
