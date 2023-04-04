using UnityEngine;
public class PowerUp : StatusEffect, IBuff
{
    BuffEnumStorage type = BuffEnumStorage.Power;
    BuffData data; // �� ī�尡 data�� ������ �־ ����� ������ �׳�. -> ���� �Ŵ������� �ϰ������� ���� ���¸� �����ϵ��� �ؾ���.
    private void Start()
    {
        if (data.StatusEffect == null)
        {
            FindBuffManager(buffManager);
            Init();
        }
    }
    public override void OnChecked()
    {
        buffManager.AddorUpdateDictionary(type, data);
    }
     // -> ������ ���� ��ü���� �����ϴ°� �ƴ�, �����Ŵ����� contains Ű�� ������
    public override BuffData BuffUp()
    {
        data.stat.rank++;
        data.stat.point += data.stat.upValue;
        return data;
    }

    public override BuffData BuffUse()
    {
        data.stat.point += data.stat.useValue;
        return data;
    }

    public override void Init()
    {
        data.StatusEffect = this;
        data = buffManager.SetBuffData(type,data);
        Debug.Log(this.data.StatusEffect+"  " + data.stat);
    }

}
