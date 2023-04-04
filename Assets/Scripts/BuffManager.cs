using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;



public class BuffManager : MonoBehaviour
{
    private Dictionary<BuffEnumStorage, BuffData> allBuffArchive = new();

    private Dictionary<BuffEnumStorage, BuffData> containBuffDictionary = new();

    Ray ray;
    RaycastHit hit;

    BuffData buffData; //�ʱ�ȭ�� ����

    private void Awake()
    {
        containBuffDictionary.Clear();
        temp();
    }
    public BuffData SetBuffData(BuffEnumStorage type, BuffData data) 
    {
        // �̹� �����ϴ°�� �ٽ� ī�忡 �����Ǹ� init�� �ߵ��ɰ��� �ش� ���� ó��
        if (!containBuffDictionary.ContainsKey(type))
        {
            if (allBuffArchive.TryGetValue(type, out BuffData archiveData))
            {
                archiveData.StatusEffect = data.StatusEffect;
                buffData = archiveData;
            }
        }
        else
        {
            Debug.Log("�̹� ��ī�̺꿡 ��ϵ� �����Դϴ�");
            buffData = data;
        }
        return buffData;
    }
    public void temp()
    {
        allBuffArchive.Add(BuffEnumStorage.Power, new BuffData(null, new BuffStat(1, 5, 7, 10)));
        allBuffArchive.Add(BuffEnumStorage.Health, new BuffData(null, new BuffStat(1, 5, 22, 100)));

        foreach (var item in allBuffArchive.Keys)
        {
            Debug.Log("Ű ���: "+item);
        }
        //���ϰ� �� ���.
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
                    hit.collider.GetComponent<StatusEffect>().OnChecked();

                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }

            }
        }
    }

    public void AddorUpdateDictionary(BuffEnumStorage buffType, BuffData data)
    {
        if (containBuffDictionary.ContainsKey(buffType))
        {
            containBuffDictionary[buffType] = data.StatusEffect.BuffUp();
            Debug.Log($"�̹� �����մϴ� ��ũ ����");
        }
        else
        {
            data.StatusEffect.BuffUse();
            containBuffDictionary.Add(buffType,data); //������ �ν��Ͻ��� �����ع���, �� ������ ������ �����Ŵ������� �������� �����ؾ���.
            Debug.Log("���� ���� �߰� : " + allBuffArchive[buffType].StatusEffect + " " + "���� ����:"+ allBuffArchive.Count);
        }
        Debug.Log($"��� ���� : {buffType}, " +
            $"���� ��� : {containBuffDictionary[buffType].stat.point}, " +
            $"��ũ : {containBuffDictionary[buffType].stat.rank}");
    }


    // ���� ������ �������� ��������� ��? �̸� ������ �����ΰ� bool�̳� setactive�� buffdata Ž���ؼ� ó�����ָ� �Ǵ°� �ƴ�?
    public void RemoveSomthing(BuffEnumStorage buffType) 
    {
        if (containBuffDictionary.ContainsKey(buffType))
        {
            containBuffDictionary.Remove(buffType);
        }
    }

    public BuffData ReturnBuff(BuffEnumStorage buffType)
    {
        return containBuffDictionary[buffType];
    }
    public StatusEffect CheckBuff(BuffEnumStorage buffType) /// �̰� �����ص� �ɵ�
    {
        return containBuffDictionary[buffType].StatusEffect;
    }
}

