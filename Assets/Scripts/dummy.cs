using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy : Buff
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
        //Debug.Log($"������ ����{point}");
    }

    public override void BuffUse()
    {
        Debug.Log("�����Դϴ�");
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
