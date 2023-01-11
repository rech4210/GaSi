using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuff : Buff
{
    public BuffManager buffmanager;


    public override void OnChecked()
    {
        buffmanager.GetBuff(this);
    }
    
    public override void BuffUp()
    {
        base.point = 5;
        Debug.Log($"������ ����{point}");
    }

    public override void BuffUse()
    {
        base.point = 15;
        Debug.Log($"���� ���� ���{point}");
        buffmanager.GetBuff(this);
    }
}
