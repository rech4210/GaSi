
public class TrapAttack : AbstractAttack
{
    public void Start()
    {
        FindBuffWithAttackGenerator();
    }
    // ��üũ�� ���� �Ŵ������� ������ �����.����? ���� ���ʷ����Ϳ��� �ؾ��ҵ�.
    public override void OnChecked()
    {
        if ((int)attackInfo.attackCardEnum > skillCheckNum)
        {
            Skill();
        }
        else if (attackInfo.attackCardEnum == AttackCardEnum.generate)
        {
            attackGenerator?.Generate(attackStatus);
        }
        // ���� �ٲ���ҵ�? ����
        else if ((int)attackInfo.attackCardEnum < skillCheckNum)
        {
            attackGenerator.IncreaseTargetStat(attackStatus, attackInfo);
        }
        attackGenerator.AddorUpdateAttackDictionary(attackCode);

        this.gameObject.SetActive(false);
    }

    public override void SetCardInfo()
    {
        base.SetCardInfo();
    }
}
