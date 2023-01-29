
#region ���� ����, ���� ������


public struct BuffData
{
    public StatusEffect StatusEffect;
    public BuffStat stat;

    public BuffData(StatusEffect statusEffect, BuffStat stat)
    {
        this.StatusEffect = statusEffect;
        this.stat = stat;
    }
}
public enum BuffStorage
{
    Health,
    Speed,
    Wisdom,
    Agility,
    Endurance,
    Power,
    Remove,
}

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
public enum AttackType
{
    laser,
    guided,
    bullet,
    trap
}

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