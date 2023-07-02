
using Unity.VisualScripting;

public class TrapAttack : AbstractAttack
{

    public void Start()
    {
        FindAttackGenerator(attackGenerator);
    }
    public override void OnChecked()
    {
        if ((int)_AttackCardInfo.attackCardEnum > skillCheckNum)
        {
            Skill();
        }
        else if (_AttackCardInfo.attackCardEnum == AttackCardEnum.generate)
        {
            attackGenerator?.Generate(_AttackStatus);
        }
        // ���� �ٲ���ҵ�? ����
        else if((int)_AttackCardInfo.attackCardEnum < skillCheckNum)
        {
            attackGenerator.IncreaseTargetStat(_AttackStatus, _AttackCardInfo);
        }
    }

    public override void SetCardInfo()
    {
        base.SetCardInfo();
    }
}
