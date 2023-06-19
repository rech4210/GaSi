using UnityEngine;
public abstract class StatusEffect : MonoBehaviour/*, IStatusEffect*/
{
    protected BuffManager buffManager;

    //private int _point;
    //public int point { get { return _point; } protected set { _point = value; } }

    //private int _rank = 1;
    //public int rank { get { return _rank; } protected set { _rank = value; } }

    //private int _upBuffValue;
    //public int upBuffValue { get { return _rank; } protected set { _rank = value; } }

    //private int _useBuffValue;
    //public int useBuffValue { get { return _rank; } protected set { _rank = value; } }

    public abstract void Init();
    public abstract void OnChecked();
    protected void FindBuffManager(BuffManager buff)
    {
        try
        {
            if (GameObject.FindWithTag("BuffManager")
            .TryGetComponent<BuffManager>(out buff))
            {
                this.buffManager = buff;
            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.LogError($"���� ���:{this.name }$���� ����: {e.Message}");
            throw e;
        }
    }
    public abstract void GetRandomCodeWithInfo(char buffCode, CardInfo cardInfo);

    // �Ʒ��� �������̽��ε� �̰� �̷��� ��� �� �ʿ� �ֳ�?
    //public virtual BuffStat BuffUse()
    //{
    //    return new BuffStat();
    //}

    //public virtual BuffStat BuffUp()
    //{
    //    return new BuffStat();
    //}

}
