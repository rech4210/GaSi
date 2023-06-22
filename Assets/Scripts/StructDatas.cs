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


[Serializable]
public class Structure
{
    public BuffData[] buff;

    public Structure()
    {
        buff = new BuffData[100];
    }

}

[Serializable]
public class BuffData
{
    public char buffCode;
    public CardInfo cardInfo;
    public BuffStat stat;

    public void Print()
    {
        UnityEngine.Debug.Log($"code:{buffCode},{cardInfo.BuffEnumName},{stat.point}");
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
    public string BuffEnumName;
    public string BGImage;
    public string FRImage;
    public string information; // ������ ... ��ŭ... �Ѵ�. ��ȭ�� ��ġ�� ǥ��
    public string description; //���� ��������.. ex
    public CardInfo(string buffEnumName, string bGImage, string fRImage, string information, string description)
    {
        BuffEnumName = buffEnumName;
        BGImage = bGImage;
        FRImage = fRImage;
        this.information = information;
        this.description = description;
    }
}

[Serializable]
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

[Serializable]
#region ���� ����, ���� ������



public class AttackStructure
{
    public AttackData[] attackDatas;
}


public class AttackData
{
    char attackCode;
    AttackStatus attackStatus;
    AttackCardInfo attackInfo;

    public AttackData(char attackCode, AttackStatus status ,AttackCardInfo info)
    {
        this.attackCode = attackCode;
        attackStatus = status;
        attackInfo = info;
    }
}

public enum AttackType
{
    laser,
    guided,
    bullet,
    trap,
    Upgrade
}

[Serializable]
public struct AttackStatus
{
    public AttackType attackType;
    public int rank;
    public int point;
    public float duration;
    public float scale;

    public AttackStatus(AttackType attackType, int rank, int point, float duration, float scale)
    {
        this.attackType = attackType;
        this.rank = rank;
        this.point = point;
        this.duration = duration;
        this.scale = scale;
    }
}

[Serializable]
public class AttackCardInfo
{
    public string attackEnumNmae;
    public string BGImage;
    public string FRImage;
    public string information; // ������ ... ��ŭ... �Ѵ�. ��ȭ�� ��ġ�� ǥ��
    public string description; //���� ��������.. ex
    public AttackCardInfo(string attackEnumNmae, string bGImage, string fRImage, string information, string description)
    {
        this.attackEnumNmae = attackEnumNmae;
        BGImage = bGImage;
        FRImage = fRImage;
        this.information = information;
        this.description = description;
    }
}

#endregion

