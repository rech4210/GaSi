using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
public class BuffManager : MonoBehaviour
{
    //use char type to buff code
    //���� ���� enum �� ���� �ذ��Ϸ� ������, json ���Ͽ� ������ �ʿ���ٰ� �Ǵ�
    //�׷��Ƿ�  char Ÿ������ 256���� ī�� ���������� ������ �� �ֵ��� Ÿ���� �����
    private Dictionary<char, BuffStat> allBuffStatArchive = new();
    private Dictionary<char, CardInfo> allCardInfoArchive = new();

    private Dictionary<char, BuffStat> containBuffDictionary = new();

    string path = null;

    int buffCounts = 100;

    //BuffData buffData; //�ʱ�ȭ�� ����

    private void Awake()
    {
        //?_?
        allBuffStatArchive.Clear();
        allCardInfoArchive.Clear();

        containBuffDictionary.Clear();
        temp();


    }

    private void Start()
    {
        try
        {
            SaveJson();
        }

        catch (System.Exception e)
        {
            Debug.Log(e + "Path is not defined");
            throw e;
        }

        JsonParsing();

    }

    /*
     * 1. BuffEnumStorage , buffstat ���� ��� ���̽� ���Ϸ� ����
     * 2. ī����� ������ Ŭ������ �������� ����, �ߺ��Ǵ� �κ��� �������̽��� ����
     * 3. temp �Լ� �κп��� json �Ľ��ϴ� �κ��� ���������ҵ�.
     */


    public void SaveJson()
    {
        path = Path.Combine(Application.dataPath + "/Json/", "BuffData.json");
        //string jsonData = null;

        File.WriteAllText(path, "");
        for (int i = 1; i < buffCounts; i++)
        {
            BuffData buffData = new BuffData((char)i, new BuffStat(1, 1, 1, 1), new CardInfo("1", "1", "1", "1", "1"));
            string jsonData = JsonUtility.ToJson(buffData,true);
            File.AppendAllText(path, jsonData);
            File.AppendAllText(path, "\n");
        }

    }

    public void JsonParsing()
    {
        path = Path.Combine(Application.dataPath + "/Json/", "BuffData.json");

        string jsonData = File.ReadAllText(path);
        Debug.Log(jsonData);

        var _buffData = JsonConvert.DeserializeObject<BuffData>(jsonData);
        //BuffData _buffData =JsonUtility.FromJson<BuffData>(jsonData);
        _buffData.Print();
        for (int i = 0; i < buffCounts; i++)
        {
            allBuffStatArchive.Add(_buffData.buffCode, _buffData.stat);
            allCardInfoArchive.Add(_buffData.buffCode, _buffData.cardInfo);

            Debug.Log(allBuffStatArchive[_buffData.buffCode]);
            Debug.Log(allCardInfoArchive[_buffData.buffCode]);

        }
    }
    public void temp()
    {
        // ���� �κ��� json Ÿ������ �Ľ��ؼ� �����;���.
        allBuffStatArchive.Add((char)1,new BuffStat(1, 5, 7, 10));
        allBuffStatArchive.Add((char)2, new BuffStat(1, 5, 22, 100));

        allCardInfoArchive.Add((char)1, new CardInfo("name","bg","fr","informaton","description"));
        allCardInfoArchive.Add((char)2, new CardInfo("name", "bg", "fr", "informaton", "description"));

        foreach (var item in allBuffStatArchive.Keys)
        {
            Debug.Log("Ű ���: " + allCardInfoArchive[item].BuffEnumName);
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
    public void AddorUpdateDictionary(char buffCode)
    {
        if (containBuffDictionary.ContainsKey(buffCode))
        {
            //method which use for player through out buffstat with buffcode
            containBuffDictionary[buffCode] = BuffUp(containBuffDictionary[buffCode]);
            Debug.Log($"�̹� �����մϴ� ��ũ ����");
        }
        else
        {
            containBuffDictionary.Add(buffCode, allBuffStatArchive[buffCode]); //������ �ν��Ͻ��� �����ع���, �� ������ ������ �����Ŵ������� �������� �����ؾ���.
            Debug.Log("���� ���� �߰� : " + allCardInfoArchive[buffCode].BuffEnumName + " " + "���� ���� ����:"+ containBuffDictionary.Count);
        }
        Debug.Log($"��� ���� : {allCardInfoArchive[buffCode].BuffEnumName}, " +
            $"���� ��� : {containBuffDictionary[buffCode].point}, " +
            $"��ũ : {containBuffDictionary[buffCode].rank}");
    }
    // �÷��̾� ����� �����ϱ�

    public BuffStat BuffUp(BuffStat buffStat)
    {
        buffStat.rank++;
        buffStat.point += buffStat.upValue;
        return buffStat;
    }

    public BuffStat BuffUse(BuffStat buffStat)
    {
        buffStat.rank = 0;
        buffStat.point += buffStat.useValue;
        return buffStat;
    }

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