/*�ʿ��� ������ ����
 * ī��
 * string type 
 * ���, ������, �����̸�, ����, ����, ��ȭ��ġ �ؽ�Ʈ
 * 
 * ����
 * primitive
 * ��ũ, ����Ʈ ��, �ʱ� ��, ��� ��, �����ڵ�
 * 
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

#region �÷��̾� ������
// �̺κ� �׳� Ŭ������ �ٲ������??
public struct PlayerStatStruct
{
    public float[] array;

    public float health;
    public float will;
    public float luck;
    public float agility;
    public float wisdom;
    public float faith;

    public float speed;
    public float defence;
    public float indurance;


    public void PrintPlayerStat()
    {
        Debug.Log($"PlayerStat: health = {health} will = {will} luck = {luck} agility = {agility} wisdom = {wisdom} faith = {faith} ");
    }
    public PlayerStatStruct(float health, float will, float luck, float agility, float wisdom, float faith, float speed, float defence, float indurance)
    {
        array = new float[50];
        this.health = health;
        this.will = will;
        this.luck = luck;
        this.agility = agility;
        this.wisdom = wisdom;
        this.faith = faith;

        this.speed = speed;
        this.defence = defence;
        this.indurance = indurance;

        this.damage = 0;
        this.avoidness = 0;
        this.blockness = 0;
    }

    public void SetArrayFromValue(int count)
    {
        if(array == null)
        {
            array = new float[count];
        }
        array[0] = health;
        array[1] = will; 
        array[2] = luck; 
        array[3] = agility;
        array[4] = wisdom;
        array[5] = faith; 
        array[6] = speed; 
        array[7] = defence;
        array[8] = indurance;
    }
    public void SetValueFromArray()
    {
        health = array[0];
        will = array[1];
        luck = array[2];
        agility = array[3];
        wisdom = array[4];
        faith = array[5];
        speed = array[6];
        defence = array[7];
        indurance = array[8];
    }

    // when it activated
    public float damage;
    public float avoidness;
    public float blockness;
}

#endregion

#region ���� ����, ���� ������
[Serializable]
public class BuffStructure : IDataLoader<int, BuffData>
{
    public BuffData[] buffDatas;

    public BuffStructure()
    {
        buffDatas = new BuffData[100];
    }

    public Dictionary<int, BuffData> MakeDict()
    {
        Dictionary<int, BuffData> dict = new();

        foreach (var item in buffDatas)
        {
            if(item != null)
            {
                dict.Add(item.buffCode, item);
            }
        }
        return dict;

    }
    public void Print()
    {
    }
}




[Serializable]
public enum BuffStatEnum
{
    health,
    will,
    luck,
    agility,
    wisdom,
    faith,
    speed,
    defence,
    indurance,
    empty
}


[Serializable]
public class BuffData 
{
    public char buffCode;
    public CardInfo cardInfo;
    public BuffStat stat;

    public void Print()
    {
        UnityEngine.Debug.Log($"code:{buffCode},{cardInfo.cardName},{stat.point}");
    }
    public BuffData(char buffCode, BuffStat stat, CardInfo cardInfo)
    {
        this.buffCode = buffCode;
        this.stat = stat;
        this.cardInfo = cardInfo;
    }
}
[Serializable]
public class CardInfo
{
    public BuffStatEnum buffType;
    public string cardName;
    public string bGImage;
    public string fRImage;
    public string information; // ������ ... ��ŭ... �Ѵ�. ��ȭ�� ��ġ�� ǥ��
    public string description; //���� ��������.. ex
    public CardInfo(BuffStatEnum bufftype,string cardName, string bGImage, string fRImage, string information, string description)
    {
        this.buffType = bufftype;
        this.cardName = cardName;
        this.bGImage = bGImage;
        this.fRImage = fRImage;
        this.information = information;
        this.description = description;
    }
}

[Serializable]
// ���⿡ ���� Ÿ�� �־������ ������?
public struct BuffStat
{
    public int rank;
    public int point;
    public int useValue;
    public int upValue;

    public BuffStat(int rank, int point, int useValue, int upValue)
    {
        this.rank = rank;
        this.point = point;
        this.useValue = useValue;
        this.upValue = upValue;
    }
}
#endregion

#region ���� ����, ���� ������



[Serializable]
public class AttackStructure : IDataLoader<int, AttackData>
{
    public AttackData[] attackDatas;

    // �迭 ���� �����Ұ�
    public AttackStructure()
    {
        attackDatas = new AttackData[10];
    }


    public Dictionary<int, AttackData> MakeDict()
    {
        Dictionary<int, AttackData> dict = new();
        foreach (var item in attackDatas)
        {
            if (item != null)
            {
                dict.Add(item.attackCode, item);
            }
        }
        return dict;
    }
}

[Serializable]
public class AttackData
{
    public char attackCode;
    public AttackStatus attackStatus;
    public  AttackCardInfo attackInfo;

    public void Print()
    {
        UnityEngine.Debug.Log($"code:{attackCode},{attackInfo.attackName},{attackStatus.point}");
    }
    public AttackData(char attackCode, AttackStatus status, AttackCardInfo info)
    {
        this.attackCode = attackCode;
        attackStatus = status;
        attackInfo = info;

    }
}

[Serializable]
public enum AttackCardEnum
{
    generate,
    duration,
    scale,
    point,
    speed, // �ֱٿ� �߰��Ȱ�, �����Ұ� Json ����
    skill_1 = 100,
    skill_2 = 101,
    skill_3 = 102
}

[Serializable]
public enum AttackType
{
    laser,
    guided,
    bullet,
    trap
}

[Serializable]
public struct AttackStatus
{
    public AttackType attackType;
    public int rank;
    public int point;
    public float duration;
    public float scale;
    public float speed;

    public AttackStatus(AttackType attackType, int rank, int point, float duration, float scale, float speed)
    {
        this.attackType = attackType;
        this.rank = rank;
        this.point = point;
        this.duration = duration;
        this.scale = scale;
        this.speed = speed;
    }
}

[Serializable]
public class AttackCardInfo
{
    public AttackCardEnum attackCardEnum;
    public string attackName;
    public string bGImage;
    public string fRImage;
    public string information; // ������ ... ��ŭ... �Ѵ�. ��ȭ�� ��ġ�� ǥ��
    public string description; //���� ��������.. ex
    public AttackCardInfo(AttackCardEnum attackCardEnum, string attackBuffName, string bGImage, string fRImage, string information, string description)
    {
        this.attackCardEnum = attackCardEnum;
        this.attackName = attackBuffName;
        this.bGImage = bGImage;
        this.fRImage = fRImage;
        this.information = information;
        this.description = description;
    }
}

#endregion

