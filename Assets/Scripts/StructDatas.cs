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
#region ���� ����, ���� ������
using System;
using System.Collections.Generic;


[Serializable]
public class BuffStructure : IDataLoader<char, BuffData>
{
    public BuffData[] buffDatas;

    public BuffStructure()
    {
        buffDatas = new BuffData[100];
    }

    public Dictionary<char, BuffData> MakeDict()
    {
        Dictionary<char, BuffData> dict = new();

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
    speed,
    endurance,
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
public class AttackStructure : IDataLoader<char, AttackData>
{
    public AttackData[] attackDatas;

    // �迭 ���� �����Ұ�
    public AttackStructure()
    {
        attackDatas = new AttackData[10];
    }


    public Dictionary<char, AttackData> MakeDict()
    {
        Dictionary<char, AttackData> dict = new();
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
    public AttackData(char attackCode, AttackStatus status ,AttackCardInfo info)
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

