
using UnityEngine;

public class LaserAttack : AbstractAttack
{
    
    public void Start()
    {
        FindBuffWithAttackGenerator();
    }
    // ��üũ�� ���� �Ŵ������� ������ �����.����? ���� ���ʷ����Ϳ��� �ؾ��ҵ�.
    public override void OnChecked()
    {
        if ((int)attackCardInfo.attackCardEnum > skillCheckNum)
        {
            Skill<LaserTurret>();
        }
        else if (attackCardInfo.attackCardEnum == AttackCardEnum.generate)
        {
            attackGenerator?.Generate<LaserTurret>(attackStatus, attackCardInfo);
        }
        // ���� �ٲ���ҵ�? ����
        else if ((int)attackCardInfo.attackCardEnum < skillCheckNum)
        {
            attackGenerator.IncreaseTargetStat<LaserTurret>(attackStatus, attackCardInfo);
        }
        attackGenerator.AddorUpdateAttackDictionary(attackCode);

        this.gameObject.SetActive(false);
    }

    public override void SetCardInfo()
    {
        base.SetCardInfo();
    }
}
