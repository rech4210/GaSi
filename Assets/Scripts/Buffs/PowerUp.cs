using UnityEngine;
public class PowerUp : StatusEffect/*, IBuff*/
{
    // Card Generate -> search BuffContainer -> add buffcode in card
    // -> click ->  
    char buffCode;
    //BuffStat stat; // �� ī�尡 data�� ������ �־ ����� ������ �׳�. -> ���� �Ŵ������� �ϰ������� ���� ���¸� �����ϵ��� �ؾ���.
    //BuffData data;
    private void Start()
    {
        //if (data.StatusEffect == null)
        //{
        //}
            FindBuffManager(buffManager);
    }
    public override void OnChecked()
    {
        buffManager.AddorUpdateDictionary(buffCode);
    }
     // -> ������ ���� ��ü���� �����ϴ°� �ƴ�, �����Ŵ����� contains Ű�� ������

    public void GetRandomBuffCode(char buffCode)
    { this.buffCode = buffCode;}

    //card generate
    public override void Init()
    {
        //data = buffManager.SetBuffData(buffCode, stat);
        //Debug.Log(stat);
    }
}
