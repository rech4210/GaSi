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
    public int rank;
    public int point;
    public float duration;
    public float scale;

    public AttackStatus(int rank, int point, float duration, float scale)
    {
        this.rank = rank;
        this.point = point;
        this.duration = duration;
        this.scale = scale;
    }
} 
#endregion

