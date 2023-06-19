using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public override void GetRandomCodeWithInfo(char buffCode, CardInfo cardInfo )
    { this.buffCode = buffCode; _CardInfo = cardInfo; }

    //card generate
    public override void Init()
    {
        //�ڽĵ��� �������� ������ �����ϱ� �� �κ��� start���� �Ľ��ص���.
        if (this.transform.GetChild(0).GetChild(1)
            .TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI buffname))
        {
            buffname.text = cardInfo.BuffEnumName;
        }
        else Debug.LogError("Not Setted Object You're null!!");

        if (this.transform.GetChild(0).GetChild(2)
            .TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI information))
        {
            information.text = cardInfo.information;
        }
        else Debug.LogError("Not Setted Object You're null!!");

        if (this.transform.GetChild(0).GetChild(3)
            .TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI description))
        {
            description.text = cardInfo.description;
        }
        else Debug.LogError("Not Setted Object You're null!!");


        if (this.transform.GetChild(0).GetChild(0).TryGetComponent<Image>(out Image frontImage))
        {

            frontImage.sprite = Resources.Load<Sprite>(Path.Combine("CardResource/", cardInfo.FRImage));
            if (frontImage.sprite == null)
            {
                Debug.Log($"There is no resource__{cardInfo.FRImage} at: " + Path.Combine(Application.dataPath + "/CardResource/", ""));
            }
        }
        else
        {
            Debug.Log("wrong Path in child frontImage");
        }

        if (this.transform.GetChild(0).TryGetComponent<Image>(out Image backImage))
        {

            backImage.sprite = Resources.Load<Sprite>(Path.Combine("CardResource/", cardInfo.BGImage));
            if (backImage.sprite == null)
            {
                Debug.Log($"There is no resource__{cardInfo.BGImage} at: " + Path.Combine(Application.dataPath + "/CardResource/", ""));
            }
        }
        else
        {
            Debug.Log("wrong Path in child backImage");
        }
        //this.transform.GetChild(0).GetChild(1).GetComponent<TextMeshPro>().text = cardInfo.BuffEnumName;
        //this.transform.GetChild(2).GetComponent<TextMeshPro>().text = cardInfo.information;
        //this.transform.GetChild(3).GetComponent<TextMeshPro>().text = cardInfo.description;

        //this.GetComponent<Image>().sprite = cardInfo.FRImage;
        //data = buffManager.SetBuffData(buffCode, stat);
        //Debug.Log(stat);
    }
}
