using System.IO;

public class PowerUp : StatusEffect/*, IBuff*/
{
    //CardInfo _CardInfo
    //{ /*ȣ�� ����ǥ��*/ get { return cardInfo; }
    //  /*ī�� ��ȭ ���÷���*/ set { cardInfo = value; Init(); } }


    private void Start()
    {
        FindBuffManager(buffManager);
    }
    public override void OnChecked()
    {
        //Debug.Log("��üũ �ߵ�");
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
