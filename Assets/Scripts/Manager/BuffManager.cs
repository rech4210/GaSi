using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

    private Dictionary<int, BuffData> allBuffStatArchive = new();
    private Dictionary<int, BuffStat> containBuffDict = new();

    Player player = null;

    private void Start()
    {
        allBuffStatArchive = DataManager.Instance.ReturnDict(allBuffStatArchive);
    }

    public void AddorUpdateDictionary(int buffCode)
    {
        if (containBuffDict.ContainsKey(buffCode))
        {
            containBuffDict[buffCode] = BuffCase(buffCode);
            //CalcBuff
            Debug.Log($"�̹� �����մϴ� ���� ��ũ ����");
        }
        else
        {
            //�ʱ� ���� �������־� BuffCase�� ���� �÷��̾� ������ ����Ұ���? �߰��ؾ���.
            containBuffDict.Add(buffCode, allBuffStatArchive[buffCode].stat); //������ �ν��Ͻ��� �����ع���, �� ������ ������ �����Ŵ������� �������� �����ؾ���.
            Debug.Log("���� ���� �߰� : " + allBuffStatArchive[buffCode].cardInfo.cardName + " " + "���� ���� ����:"+ containBuffDict.Count);
        }
        Debug.Log($"��� ���� : {allBuffStatArchive[buffCode].cardInfo.cardName}, " +
            $"���� ��� : {containBuffDict[buffCode].point}, " +
            $"��ũ : {containBuffDict[buffCode].rank}");

        //this content must be need switch case
    }
    
    private BuffStat BuffCase(int buffCode)
    {
        BuffStat stat = containBuffDict[buffCode];
        PlayerStatStruct playerStatStruct = DataManager.Instance._playerStat;

        playerStatStruct.SetArrayFromValue(8); // ���⸦ ������ ��ü�ϱ�
        // empty�� ��� üũ���ֱ�
        if (allBuffStatArchive[buffCode].cardInfo.buffType != BuffStatEnum.empty)
        {
            playerStatStruct.array[(int)allBuffStatArchive[buffCode].cardInfo.buffType] += CalcBuff(ref stat, buffCode);
            playerStatStruct.SetValueFromArray();
        }
        else
        {
            //Do empty work
        }
        DataManager.Instance.PlayerStatDele(playerStatStruct);
        return stat;
    }
    private int CalcBuff(ref BuffStat stat, int buffCode)
    {
        //Use�� Up�� ȥ������ �ʾҴ���..?
        stat.rank++;
        stat.point += allBuffStatArchive[buffCode].stat.upValue;
        return stat.point;
    }

    public void RemoveSomthing(int buffCode) 
    {
        if (containBuffDict.ContainsKey(buffCode))
        {
            containBuffDict.Remove(buffCode);
        }
    }

    public BuffStat ReturnBuff(int buffCode)
    {
        return containBuffDict[buffCode];
    }

    // �� �κ� ������Ƽ�� ����
    public Dictionary<int, BuffStat> ContainStatToGenerate()
    {
        return containBuffDict;
    }
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