using TMPro;
using UnityEngine;

public class PowerUp : StatusEffect/*, IBuff*/
{
    // Card Generate -> search BuffContainer -> add buffcode in card
    // -> click ->  
    char buffCode;

    CardInfo cardInfo;
    CardInfo _CardInfo
    { /*ȣ�� ����ǥ��*/ get { return cardInfo; }
      /*ī�� ��ȭ ���÷���*/ set { cardInfo = value; Init(); } }

    //BuffStat stat; // �� ī�尡 data�� ������ �־ ����� ������ �׳�. -> ���� �Ŵ������� �ϰ������� ���� ���¸� �����ϵ��� �ؾ���.
    //BuffData data;

    private void Start()
    {
        FindBuffManager(buffManager);
    }
    public override void OnChecked()
    {
        Debug.Log("��üũ �ߵ�");
        buffManager.AddorUpdateDictionary(buffCode);
    }

    public void GetRandomCodeWithInfo(char buffCode, CardInfo cardInfo )
    { this.buffCode = buffCode; _CardInfo = cardInfo; }

    //card generate
    public override void Init()
    {
        //�ڽĵ��� �������� ������ �����ϱ� �� �κ��� start���� �Ľ��ص���.
        if (this.transform.GetChild(0).GetChild(1)
            .TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI tmp))
        {
            tmp.text = cardInfo.BuffEnumName;
        }
        else
        {
            Debug.LogError("Not Setted Object You're null!!");
        }
        

        //this.transform.GetChild(0).GetChild(1).GetComponent<TextMeshPro>().text = cardInfo.BuffEnumName;
        //this.transform.GetChild(2).GetComponent<TextMeshPro>().text = cardInfo.information;
        //this.transform.GetChild(3).GetComponent<TextMeshPro>().text = cardInfo.description;

        //this.GetComponent<Image>().sprite = JsonUtility.FromJson()cardInfo.BGImage;
        //this.GetComponent<Image>().sprite = cardInfo.FRImage;
        //data = buffManager.SetBuffData(buffCode, stat);
        //Debug.Log(stat);
    }
}
