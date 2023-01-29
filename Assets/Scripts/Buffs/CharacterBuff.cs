using UnityEngine;

public class CharacterBuff : StatusEffect,IBuff
{
    BuffStorage type = BuffStorage.Health;
    BuffData data;
    BuffStat stat;
    private void Start()
    {
        FindBuffManager(buffManager);
        Init();
    }

    public override void OnChecked()
    {
        buffManager.AddorUpdateDictionary(type, data);
    }
    public override void Init()
    {
        data = buffManager.SetBuffData(this);
        stat = new BuffStat(data.stat.rank, data.stat.point, data.stat.useValue, data.stat.upValue);
    }
    public override void BuffUse()
    {
        stat.point += stat.useValue;
    }

    public override void BuffUp()
    {
        RankUp(); // ��ũ�� �κе� �Ŵ������� ó���Ұ�.
        stat.point += stat.upValue;
        Debug.Log(stat.point);
    }

    public override void RankUp()
    {
        rank++; // ��ũ���� ���� ����� ���� ���� ����.
        Debug.Log($"��ũ ��� :{rank}");
    }
}
