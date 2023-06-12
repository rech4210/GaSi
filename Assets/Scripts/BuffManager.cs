using System.Collections.Generic;
using UnityEngine;
public class BuffManager : MonoBehaviour
{
    //use char type to buff code
    //���� ���� enum �� ���� �ذ��Ϸ� ������, json ���Ͽ� ������ �ʿ���ٰ� �Ǵ�
    //�׷��Ƿ�  char Ÿ������ 256���� ī�� ���������� ������ �� �ֵ��� Ÿ���� �����
    private Dictionary<char, BuffStat> allBuffArchive = new();

    private Dictionary<char, BuffStat> containBuffDictionary = new();

    Ray ray;
    RaycastHit hit;

    BuffData buffData; //�ʱ�ȭ�� ����

    private void Awake()
    {
        allBuffArchive.Clear();
        containBuffDictionary.Clear();
        temp();
    }

    /*
     * 1. BuffEnumStorage , buffstat ���� ��� ���̽� ���Ϸ� ����
     * 2. ī����� ������ Ŭ������ �������� ����, �ߺ��Ǵ� �κ��� �������̽��� ����
     * 3. temp �Լ� �κп��� json �Ľ��ϴ� �κ��� ���������ҵ�.
     */

    public void temp()
    {
        // ���� �κ��� json Ÿ������ �Ľ��ؼ� �����;���.
        allBuffArchive.Add((char)1,new BuffStat("power",1, 5, 7, 10));
        allBuffArchive.Add((char)2, new BuffStat("HealthUp", 1, 5, 22, 100));

        foreach (var item in allBuffArchive.Keys)
        {
            Debug.Log("Ű ���: "+item);
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position,ray.direction * 20f,Color.red);
            if (Physics.Raycast(ray, out hit,50f))
            {
                try
                {
                    hit.collider.GetComponent<PowerUp>().OnChecked();

                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }

            }
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
            containBuffDictionary.Add(buffCode, allBuffArchive[buffCode]); //������ �ν��Ͻ��� �����ع���, �� ������ ������ �����Ŵ������� �������� �����ؾ���.
            Debug.Log("���� ���� �߰� : " + allBuffArchive[buffCode].buffEnumName + " " + "���� ���� ����:"+ containBuffDictionary.Count);
        }
        Debug.Log($"��� ���� : {containBuffDictionary[buffCode].buffEnumName}, " +
            $"���� ��� : {containBuffDictionary[buffCode].point}, " +
            $"��ũ : {containBuffDictionary[buffCode].rank}");
    }

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

    public Dictionary<char, BuffStat> DicToGenerate()
    {
        return allBuffArchive;
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