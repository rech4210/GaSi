using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] attackObjectPrefab;

    private Dictionary<char, AttackData> allAttackStatArchive = new();
    private Dictionary<char, AttackStatus> containAttackDict = new();

    private GameObject attackTarget;
    private List<GameObject> attackObjects = new List<GameObject>();
    private List<AttackFunc> objectsComponent = new List<AttackFunc>();
    //private List<AbstractAttack> attackObjList;

    public void AddorUpdateAttackDictionary(char attackCode)
    {
        //���� ������ ��ȭ�� �����ִµ� ��� ��ųʸ� ������� �����Ұ���?
        if (containAttackDict.ContainsKey(attackCode))
        {
            //method which use for player through out buffstat with buffcode
            containAttackDict[attackCode] = allAttackStatArchive[attackCode].attackStatus;
            Debug.Log($"��ũ ����");
        }
        else
        {
            Debug.Log(allAttackStatArchive[attackCode].attackStatus);
            containAttackDict.Add(attackCode, allAttackStatArchive[attackCode].attackStatus); //������ �ν��Ͻ��� �����ع���, �� ������ ������ �����Ŵ������� �������� �����ؾ���.
            Debug.Log("���� ���� �߰� : " + allAttackStatArchive[attackCode].attackInfo.attackName + " " + "���� ���� ����:" + containAttackDict.Count);
        }
        Debug.Log($"��� ���� : {allAttackStatArchive[attackCode].attackInfo.attackName}, " +
            $"���� ��� : {containAttackDict[attackCode].point}, " +
            $"��ũ : {containAttackDict[attackCode].rank}");
    }
    public Dictionary<char, AttackStatus> ContainAttackStatToGenerate()
    {
        return containAttackDict;
    }


    private void Start()
    {
        allAttackStatArchive = DataManager.Instance.ReturnDict(allAttackStatArchive);
        FindPlayer();
    }

    protected void FindPlayer()
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


    Vector3 RandomPose()
    {
        return new Vector3(
        UnityEngine.Random.Range(17f, -17f),
        UnityEngine.Random.Range(0, 0),
        UnityEngine.Random.Range(1.2f, -20f));
    }

    public void Generate(AttackStatus status)
    {
        Debug.Log(((int)status.attackType).ToString());
        var obj = Instantiate(attackObjectPrefab[(int)status.attackType], RandomPose(),this.transform.rotation);
        attackObjects.Add(obj);

        var component = obj.GetComponent<AttackFunc>();

        Debug.Log(component.ToString());
        Debug.Log(attackTarget.ToString());
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
