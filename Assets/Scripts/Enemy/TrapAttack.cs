
using Unity.VisualScripting;

public class TrapAttack : AbstractAttack
{
    public void Start()
    {
        FindBuffWithAttackGenerator();
    }
    // ��üũ�� ���� �Ŵ������� ������ �����.����? ���� ���ʷ����Ϳ��� �ؾ��ҵ�.
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
        else if ((int)_AttackCardInfo.attackCardEnum < skillCheckNum)
        {
            attackGenerator.IncreaseTargetStat(_AttackStatus, _AttackCardInfo);
        }
        buffManager.AddorUpdateAttackDictionary(attackCode,_AttackStatus);

        this.gameObject.SetActive(false);
    }

    public override void SetCardInfo()
    {
        base.SetCardInfo();
    }
}
