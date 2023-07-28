using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    //use char type to buff code
    //���� ���� enum �� ���� �ذ��Ϸ� ������, json ���Ͽ� ������ �ʿ���ٰ� �Ǵ�
    //�׷��Ƿ�  char Ÿ������ 256���� ī�� ���������� ������ �� �ֵ��� Ÿ���� �����
    private Dictionary<char, BuffStat> allBuffStatArchive = new();
    private Dictionary<char, CardInfo> allCardInfoArchive = new();

    private Dictionary<char, BuffStat> containBuffDictionary = new();


    private Dictionary<char, AttackStatus> allAttackStatArchive = new();
    private Dictionary<char, AttackCardInfo> allAttackCardInfoArchive = new();

    private Dictionary<char, AttackStatus> containAttackStatDictionary = new();



    string path = null;

    int buffCounts = 100;

    //BuffData buffData; //�ʱ�ȭ�� ����

    private void Awake()
    {
        //?_?
        allBuffStatArchive.Clear();
        allCardInfoArchive.Clear();
        allAttackStatArchive.Clear();
        allAttackCardInfoArchive.Clear();
        containBuffDictionary.Clear();
        containAttackStatDictionary.Clear();

        //SaveBuffJson();
        //SaveAttackJson();

        //Ʈ���� ĳġ ������ ���� ����ѵ�?
        //try
        //{
        //    SaveBuffJson();
        //}

        //catch (System.Exception e)
        //{
        //    Debug.Log(e + "Path is not defined");
        //    throw e;
        //}

        JsonParsing();
        JsonAttackParsing();
    }

    private void Start()
    {
    }
    private void Update()
    {

    }

    /*
     * 1. BuffEnumStorage , buffstat ���� ��� ���̽� ���Ϸ� ����
     * 2. ī����� ������ Ŭ������ �������� ����, �ߺ��Ǵ� �κ��� �������̽��� ����
     * 3. temp �Լ� �κп��� json �Ľ��ϴ� �κ��� ���������ҵ�.
     */

    [JsonConverter(typeof(StringEnumConverter))]
    public BuffStatEnum statEnum { get; set; }

    #region ���̽� �Ľ�
    public void SaveBuffJson()
    {
        path = Path.Combine(Application.dataPath + "/Json/", "BuffData.json");
        //string jsonData = null;

        File.WriteAllText(path, "");

        Structure buffdata = new Structure();

        for (int i = 0; i < buffCounts; i++)
        {
            BuffData buff = new BuffData((char)i, new BuffStat(1, 1, 1, 1), new CardInfo(statEnum = BuffStatEnum.empty, "1", "1", "1", "1", "1"));

            buffdata.buff[i] = buff;
        }
        string jsonData = JsonUtility.ToJson(buffdata, true);
        File.WriteAllText(path, jsonData);
    }
    //��� ó��?
    [JsonConverter(typeof(StringEnumConverter))]
    public AttackType attackType { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public AttackCardEnum attackEnum { get; set; }
    public void SaveAttackJson()
    {
        path = Path.Combine(Application.dataPath + "/Json/", "AttackData.json") ?? null;
        //string jsonData = null;
        if (path == null) return;

        File.WriteAllText(path, "");

        AttackStructure structure = new AttackStructure();

        for (int i = 0; i < 10; i++)
        {
            AttackData attackData = new AttackData((char)i,new AttackStatus(attackType, 1,1,1,1,1),new AttackCardInfo(attackEnum, "1", "1", "1", "1", "1"));

            structure.attackDatas[i] = attackData;

        }
        string jsonData = JsonUtility.ToJson(structure, true);
        File.WriteAllText(path, jsonData);
    }


    public void JsonParsing()
    {
 
        path = Path.Combine(Application.dataPath + "/Json/", "BuffData.json");

        string jsonData = File.ReadAllText(path);
        Debug.Log(jsonData);

        Structure _buffData = JsonUtility.FromJson<Structure>(jsonData);

        for (int i = 0; i < buffCounts; i++)
        {
            allBuffStatArchive.Add(_buffData.buff[i].buffCode, _buffData.buff[i].stat);
            allCardInfoArchive.Add(_buffData.buff[i].buffCode, _buffData.buff[i].cardInfo);

            //Debug.Log(allBuffStatArchive[_buffData.buff[i].buffCode].point);
            //Debug.Log(allCardInfoArchive[_buffData.buff[i].buffCode].cardName);

        }
    }

    public void JsonAttackParsing()
    {

        path = Path.Combine(Application.dataPath + "/Json/", "AttackData.json");

        string jsonData = File.ReadAllText(path);
        Debug.Log(jsonData);

        AttackStructure _attackStruct = JsonUtility.FromJson<AttackStructure>(jsonData);


        for (int i = 0; i < 10; i++)
        {
            allAttackStatArchive.Add(_attackStruct.attackDatas[i].attackCode, _attackStruct.attackDatas[i].attackStatus);
            allAttackCardInfoArchive.Add(_attackStruct.attackDatas[i].attackCode, _attackStruct.attackDatas[i].attackInfo);

            //Debug.Log(allAttackStatArchive[_attackStruct.attackDatas[i].attackCode].point);
            //Debug.Log(allAttackCardInfoArchive[_attackStruct.attackDatas[i].attackCode].attackName);

        }
    }

    #endregion

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
        if (containBuffDictionary.ContainsKey(buffCode))
        {
            //method which use for player through out buffstat with buffcode
            containBuffDictionary[buffCode] = BuffUp(containBuffDictionary[buffCode]);
            Debug.Log($"�̹� �����մϴ� ���� ��ũ ����");
        }
        else
        {
            containBuffDictionary.Add(buffCode, allBuffStatArchive[buffCode]); //������ �ν��Ͻ��� �����ع���, �� ������ ������ �����Ŵ������� �������� �����ؾ���.
            Debug.Log("���� ���� �߰� : " + allCardInfoArchive[buffCode].cardName + " " + "���� ���� ����:"+ containBuffDictionary.Count);
        }
        Debug.Log($"��� ���� : {allCardInfoArchive[buffCode].cardName}, " +
            $"���� ��� : {containBuffDictionary[buffCode].point}, " +
            $"��ũ : {containBuffDictionary[buffCode].rank}");
    }
    public void AddorUpdateAttackDictionary(char attackCode,AttackStatus attackStatus)
    {
        //���� ������ ��ȭ�� �����ִµ� ��� ��ųʸ� ������� �����Ұ���?
        if (containAttackStatDictionary.ContainsKey(attackCode))
        {
            //method which use for player through out buffstat with buffcode
            containAttackStatDictionary[attackCode] = attackStatus;
            Debug.Log($"��ũ ����");
        }
        else 
        {
            containAttackStatDictionary.Add(attackCode, allAttackStatArchive[attackCode]); //������ �ν��Ͻ��� �����ع���, �� ������ ������ �����Ŵ������� �������� �����ؾ���.
            Debug.Log("���� ���� �߰� : " + allAttackCardInfoArchive[attackCode].attackName + " " + "���� ���� ����:" + containAttackStatDictionary.Count);
        }
        Debug.Log($"��� ���� : {allAttackCardInfoArchive[attackCode].attackName}, " +
            $"���� ��� : {containAttackStatDictionary[attackCode].point}, " +
            $"��ũ : {containAttackStatDictionary[attackCode].rank}");
    }

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
        if (containBuffDictionary.ContainsKey(buffCode))
        {
            containBuffDictionary.Remove(buffCode);
        }
    }

    public BuffStat ReturnBuff(char buffCode)
    {
        return containBuffDictionary[buffCode];
    }



    public Dictionary<char, BuffStat> StatToGenerate()
    {
        return allBuffStatArchive;
    }

    public Dictionary<char,CardInfo> InfoToGenerate()
    {
        return allCardInfoArchive;
    }

    public Dictionary<char, AttackStatus> AttackStatToGenerate()
    {
        return allAttackStatArchive;
    }

    public Dictionary<char, AttackCardInfo> AttackInfoToGenerate()
    {
        return allAttackCardInfoArchive;
    }
    public Dictionary<char, BuffStat> ContainStatToGenerate()
    {
        return containBuffDictionary;
    }

    public Dictionary<char, AttackStatus> ContainAttackStatToGenerate()
    {
        return containAttackStatDictionary;
    }

}


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