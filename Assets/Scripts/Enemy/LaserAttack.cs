
using UnityEngine;

public class LaserAttack : AbstractAttack
{
    
    public void Start()
    {
        FindBuffWithAttackGenerator();
    }
    // 온체크시 버프 매니저에게 영향을 줘야함.수정? 어택 제너레이터에서 해야할듯.
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
        // 조건 바꿔야할듯? 수정
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
