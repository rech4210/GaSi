using UnityEngine;

public class PowerUp : StatusEffect, IBuff
{
    BuffStorage type = BuffStorage.Power;
    BuffStat status;
    BuffData data;
    private void Start()
    {
        FindBuffManager(buffManager);
        Debug.Log("�����Ǿ����ϴ�");
        Init();
    }

    public override void OnChecked()
    {
        buffManager.AddorUpdateDictionary(type, data);
    }

    public override void BuffUp()
    {
        RankUp();
        base.point = 5;
        Debug.Log($"������ ����{point}");
    }

    public override void BuffUse()
    {
        base.point = 15;
        status.point = point;
        Debug.Log($"���� ���� ��� : {this.GetType()}");
    }
    public override void RankUp()
    {
        rank++;
        status.rank  = rank;
        Debug.Log($"��ũ ��� :{rank}");
    }

    public override void Init()
    {
        data = new BuffData(this,status);
        //����Ʈ�� ��ũ �ʱ�ȭ
    }

}
