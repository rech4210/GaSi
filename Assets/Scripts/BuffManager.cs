using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class BuffManager : MonoBehaviour
{
    private Dictionary<BuffStorage, BuffData> 
        dictBuff = new Dictionary<BuffStorage, BuffData>();

    private Dictionary<StatusEffect, BuffStat> 
        buffValuePairs = new Dictionary<StatusEffect, BuffStat>();

    // -> ���⿡�� ��ü ������ ���� �ʱ�ȭ�� �����ְ� �ش� ������ ī�忡 �����ϸ�
    //    ��ü�� ��ȯ�ϸ鼭 �´� �����Ͱ��� ��������.
    Ray ray;
    RaycastHit hit;

    BuffData buffData;

    private void Awake()
    {
        dictBuff.Clear();

        
    }

    /* 1. ������ ������.
     * 2. AddDictionary�� ���� �ش� ������ �����ϴ��� �Ǵ�.
     * 3. ���� �����Ѵٸ� SetBuffData�� ���� �����ϰ� ���� �ֽ�ȭ �����ش� (BuffManager�� ����).
     *      3-1. �ƴ϶�� ���� ����Ʈ ��ü�� ��ȸ�ϸ鼭 Type�� ���Ͽ� ���� �־��ش�.
     * 4. �ֽ�ȭ �� ���� ������ �� ������ ���� BuffData�� �����Ѵ�.
     */
    public BuffData SetBuffData(StatusEffect statusEffect)
    {
        // �̹� ���� �����͵�� ���Ͽ� ó���Ұ�.
        
        if (buffValuePairs.TryGetValue(statusEffect,out BuffStat value))
        {
            buffData = new BuffData(statusEffect, value);

        }
        return buffData;
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
                catch (System.NullReferenceException e)
                {
                    Debug.Log("��� �ƴ�") ;
                }
            }
        }
    }

    public void AddorUpdateDictionary(BuffStorage buffType, BuffData buffValues)
    {
        if (dictBuff.ContainsKey(buffType))
        {
            Debug.Log("�̹� �����մϴ� ��ũ ����");
            buffValues.StatusEffect.BuffUp();
            // ���� ��ųʸ� �ֽ�ȭ
        }
        else
        {
            dictBuff.Add(buffType, buffValues);
            buffValues.StatusEffect.BuffUse();
            
            Debug.Log("���� ���� �߰� : " + dictBuff.Keys + " " + "���� ����:"+ dictBuff.Count);
        }
    }

    public void RemoveSomthing(BuffStorage buffType)
    {
        if (dictBuff.ContainsKey(buffType))
        {
            dictBuff.Remove(buffType);
        }
    }

    public StatusEffect ReturnBuff(BuffStorage buffType)
    {
        return dictBuff[buffType].StatusEffect;
    }
    public StatusEffect CheckBuff(BuffStorage buffType)
    {
        return dictBuff[buffType].StatusEffect;
    }
    
}

