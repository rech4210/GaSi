using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackGenerator : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] attackObjectPrefab;

    private Dictionary<int, AttackData> allAttackStatArchive = new();
    private Dictionary<int, AttackStatus> containAttackDict = new();

    private GameObject attackTarget;
    private List<GameObject> attackObjects = new List<GameObject>();
    private List<AttackFunc> objectsComponent = new List<AttackFunc>();
    //private List<AbstractAttack> attackObjList;

    public void AddorUpdateAttackDictionary(int attackCode)
    {
        //���� ������ ��ȭ�� �����ִµ� ��� ��ųʸ� ������� �����Ұ���?
        if (containAttackDict.ContainsKey(attackCode))
        {
            //method which use for player through out buffstat with buffcode
            containAttackDict[attackCode] = allAttackStatArchive[attackCode].attackStatus;
            //Debug.Log($"��ũ ����");
        }
        else
        {
            //Debug.Log(allAttackStatArchive[attackCode].attackStatus);
            containAttackDict.Add(attackCode, allAttackStatArchive[attackCode].attackStatus); //������ �ν��Ͻ��� �����ع���, �� ������ ������ �����Ŵ������� �������� �����ؾ���.
            //Debug.Log("���� ���� �߰� : " + allAttackStatArchive[attackCode].attackInfo.attackName + " " + "���� ���� ����:" + containAttackDict.Count);
        }
        //Debug.Log($"��� ���� : {allAttackStatArchive[attackCode].attackInfo.attackName}, " +
        //    $"���� ��� : {containAttackDict[attackCode].point}, " +
        //    $"��ũ : {containAttackDict[attackCode].rank}");
    }
    public Dictionary<int, AttackStatus> ContainAttackStatToGenerate()
    {
        return containAttackDict;
    }


    private void Start()
    {
        allAttackStatArchive = DataManager.Instance.ReturnDict(allAttackStatArchive);
        FindPlayer();
    }

    protected void FindPlayer() //�̺κ��� �����ؾ��� (�÷��̾� ã�⸦ �ǽð�����)
    {
       // �÷��̾� ã�°� ����ƽ���� ó���ص� �ɵ� .����
        try
        {
            attackTarget = DataManager.Instance._playerTransform.gameObject;
            //if (GameObject.FindWithTag("Player").TryGetComponent<Player>(out Player player))
            //{
            //    // ���ӿ�����Ʈ ������Ʈ ��ü�� ���������� ���� ��������.
            //    attackTarget = player.gameObject;
            //    Debug.Log(attackTarget + "���� ���");
            //}
        }
        catch (NullReferenceException e)
        {
            Debug.Log("IsThere no player");
            throw e;
        }
    }


    Vector3 RandomPose() => new Vector3(1, 1, 1);
    //{
    //    return new Vector3(
    //    UnityEngine.Random.Range(23f, -23f),
    //    UnityEngine.Random.Range(0, 0),
    //    UnityEngine.Random.Range(4f, -23f));
    //}

    public void Generate(AttackStatus status)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y+5f, transform.localPosition.z); 

        //Debug.Log((status.attackType).ToString());
        var obj = Instantiate(attackObjectPrefab[(int)status.attackType], RandomPose(),this.transform.rotation,StageManager.Instance.GetCurrentStagePos());
        attackObjects.Add(obj);

        var component = obj.GetComponent<AttackFunc>();
        //var Itimeobj = component.GetComponent<ITimeEvent>();
        //component.GetComponent<ITimeEvent>(() => { TimeEvent.Instance.StoreTimeEventObj(component); });
        TimeEvent.Instance.StoreTimeEventObj(obj);
        // �� �κп��� generate�� �ڵ����� �������� ��ϵǵ��� ����

        //Debug.Log(component.ToString());
        //Debug.Log(attackTarget.ToString());

        //���������� �ʴ�
        component._Player = attackTarget;
        component._AttackStatus = status;
        //attackFunc�� ī�� ���ݴɷ�ġ�� ���� ����� �ο�����.
        //�Ŀ�, AttackFunc�� ��ӹ޴� ���鿡�� ����
        //���� ��ü���� �����͸� ������ �Ļ��� ź���鿡�� ���ݼ�ġ�� �Է�����.

        objectsComponent?.Add(component);

        //var _abstract = obj.GetComponent<AbstractAttack>();

        // �ƹ����� ���⼭ �ʱ� ������ ��������� �ҵ�. Buffmanager -> CardGen -> AttackGen -> ����
        //if (_abstract != null ) { _abstract.SetAttackStatus(); }
        //else 
    }

    public void IncreaseTargetStat(AttackStatus status, AttackCardInfo info)
    {
        //�������� ��� �ش� �κп��� ������ �߻��� ������ �ִ�. ����
        foreach (var obj in objectsComponent) 
        {
            if (obj._AttackType == status.attackType) 
            {
                obj.GetComponent<AttackFunc>()?.CalcStat(status,info);
            }
            // ���ݿ� ���� ���� (�������)
            // 
            //obj.GetComponent<>();
        }
        //foreach (var attack in attackObjList) { attack.CalcAttackStatus()}
    }

    public void UseSkill(AttackStatus status, string skill)
    {
        foreach (var obj in objectsComponent)
        {
            if (obj._AttackType == status.attackType)
            {
                obj.GetComponent<AttackFunc>()?.Invoke(skill,0);
            }
            // ���ݿ� ���� ���� (�������)
            // 
            //obj.GetComponent<>();
        }
    }

}
