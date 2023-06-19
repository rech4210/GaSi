using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractAttack : MonoBehaviour
{
    private AttackStatus attackStatus;

    public AttackStatus _attackStatus { get {return attackStatus ;} set { SetAttackStatus(value); }}
    //public int rank
    //{ get { return attackStatus.rank; } set { attackStatus.rank += value; }}
    //public int point
    //{ get { return attackStatus.point; } set { attackStatus.point = value; } }
    //public float duration
    //{ get { return attackStatus.duration; } set { attackStatus.duration= value; } }
    //public float scale
    //{ get { return attackStatus.scale; } set { attackStatus.scale = value; } }

    public abstract AttackType GetAttackType();

    public virtual void SetAttackStatus(AttackStatus attackStatus)
    {
        _attackStatus = attackStatus;
    }

    // ����Ÿ������ ������������ ���� �Լ��� ���� ���� ����. json�� ���� Ÿ�Ա��� ����Ѵٸ� ����������.
    public virtual void CalcAttackStatus(float calcNum, string statType)
    {
        this.attackStatus.rank++;
        switch (statType)
        {
            case "duration":
                this.attackStatus.duration *= calcNum; break;
            case "point":
                this.attackStatus.point += (int)calcNum; break;
            case "scale":
                this.attackStatus.scale *= calcNum; break;
            default:
                break;
        }
    }
}
