using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : StatusEffect/*, IBuff*/
{
    // Card Generate -> search BuffContainer -> add buffcode in card
    // -> click ->  
    //CardInfo _CardInfo
    //{ /*ȣ�� ����ǥ��*/ get { return cardInfo; }
    //  /*ī�� ��ȭ ���÷���*/ set { cardInfo = value; Init(); } }


    private void Start()
    {
        FindBuffManager(buffManager);
    }
    public override void OnChecked()
    {
        Debug.Log("��üũ �ߵ�");
        Debug.Log(_BuffCode);
        //generate �޾ƾ� �� 9/29

        //_BuffCode = '0';
        buffManager.AddorUpdateDictionary(_BuffCode);
        var stat = DataManager.Instance._playerStat;
        stat.health ++;
        DataManager.Instance.PlayerStatDele.Invoke(stat);
        // ���⿡ �����ϴ� ���?
    }


    //public override void GetRandomCodeWithInfo(char buffCode, CardInfo cardInfo, BuffStat buffStat )
    //{ base.GetRandomCodeWithInfo(); }

    public override void SetCardInfo()
    {
        base.SetCardInfo();
    }
}
