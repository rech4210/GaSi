using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

    private Dictionary<char, BuffData> allBuffStatArchive = new();
    private Dictionary<char, BuffStat> containBuffDict = new();

    Player player = null;
    // �÷��̾�� ���� �Ѱ��ֱ�.

    //BuffData buffData; //�ʱ�ȭ�� ����

    private void Start()
    {
        DataManager.Instance.ReturnDict(allBuffStatArchive);
    }

   

    //�߾� ��꿡�� ���� �߰� ���� �� ������� ������ �� �ѷ��ִ°� ������?
    // �ƴ϶�� ���� ������ ī�� �ܿ��� �����ϴ°� ������?
    // �켱 �ѷ��ִ� �����̶�� ������ �����Ŵ������� �����Ѵ�.
    // �׸��� �ߺ��Ǵ� �Ǵ��� point ���� 0���� �ƴ����� �Ǵ��ϴ°ɷ� ����.

    // �װ� �׷����ĵ� �÷��̾����� �� ������ ��� �����Ұǵ�?
    // ���ȸ��ص� ���� �پ��ϰ� �����µ�. ���� �ڵ�� ó���Ұ���? �ƴϸ� .

    // ���, ���� �Ŵ������� �����͸� �����ϵ��� ����,
    // search BuffContainer -> Generate card -> Click buff -> Calc buffCode in BM
    // broad cast to player, enemy
    public void AddorUpdateDictionary(char buffCode)
    {
        if (containBuffDict.ContainsKey(buffCode))
        {
            containBuffDict[buffCode] = BuffUp(containBuffDict[buffCode]);
            Debug.Log($"�̹� �����մϴ� ���� ��ũ ����");
        }
        else
        {
            containBuffDict.Add(buffCode, allBuffStatArchive[buffCode].stat); //������ �ν��Ͻ��� �����ع���, �� ������ ������ �����Ŵ������� �������� �����ؾ���.
            Debug.Log("���� ���� �߰� : " + allBuffStatArchive[buffCode].cardInfo.cardName + " " + "���� ���� ����:"+ containBuffDict.Count);
        }
        Debug.Log($"��� ���� : {allBuffStatArchive[buffCode].cardInfo.cardName}, " +
            $"���� ��� : {containBuffDict[buffCode].point}, " +
            $"��ũ : {containBuffDict[buffCode].rank}");
    }
    // �̰� �������ʷ����Ͱ� �����ϵ���
    

    // �÷��̾� ����� �����ϱ�

    private BuffStat BuffUp(BuffStat buffStat)
    {
        buffStat.rank++;
        buffStat.point += buffStat.upValue;
        return buffStat;
    }

    //public BuffStat BuffUse(BuffStat buffStat)
    //{
    //    buffStat.rank = 0;
    //    buffStat.point += buffStat.useValue;
    //    return buffStat;
    //}

    // ���� ������ �������� ��������� ��? �̸� ������ �����ΰ� bool�̳� setactive�� buffdata Ž���ؼ� ó�����ָ� �Ǵ°� �ƴ�?
    public void RemoveSomthing(char buffCode) 
    {
        if (containBuffDict.ContainsKey(buffCode))
        {
            containBuffDict.Remove(buffCode);
        }
    }

    public BuffStat ReturnBuff(char buffCode)
    {
        return containBuffDict[buffCode];
    }

    public Dictionary<char, BuffStat> ContainStatToGenerate()
    {
        return containBuffDict;
    }


}

//public Dictionary<char, BuffStat> StatToGenerate()
//{
//    return allBuffStatArchive;
//}

//public Dictionary<char,CardInfo> InfoToGenerate()
//{
//    return allCardInfoArchive;
//}

//public Dictionary<char, AttackStatus> AttackStatToGenerate()
//{
//    return allAttackStatArchive;
//}

//public Dictionary<char, AttackCardInfo> AttackInfoToGenerate()
//{
//    return allAttackCardInfoArchive;
//}


// This method use for Initialize of Buffdata
//public BuffData SetBuffData(char buffCode, BuffStat data) 
//{
//    // �̹� �����ϴ°�� �ٽ� ī�忡 �����Ǹ� init�� �ߵ��ɰ��� �ش� ���� ó��
//    if (!containBuffDictionary.ContainsKey(buffCode))
//    {
//        if (allBuffArchive.TryGetValue(buffCode, out BuffStat archiveData))
//        {
//            //archiveData.StatusEffect = data.StatusEffect;
//            //buffData = archiveData;
//            buffData = new BuffData(buffCode, archiveData);
//        }
//    }
//    else
//    {
//        Debug.Log("�̹� ���� �����̳ʿ� ��ϵ� �����Դϴ�");
//        //buffData = data;
//    }
//    return buffData;
//}