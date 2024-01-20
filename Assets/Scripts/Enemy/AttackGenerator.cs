using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackGenerator : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] attackObjectPrefab;

    private Dictionary<int, AttackData> allAttackStatArchive = new();
    private Dictionary<int, AttackStatus> containAttackDict = new();

    private GameObject attackTarget;

    private List<LaserTurret> laserList = new List<LaserTurret>();
    private List<TrapTurret> trapList = new List<TrapTurret>();
    private List<GuidedTurret> guidedList = new List<GuidedTurret>();
    private List<BulletTurret> bulletList = new List<BulletTurret>();
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

    public void Generate<T>(AttackStatus status, AttackCardInfo info) where T : AttackFunc<T>
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y+5f, transform.localPosition.z); 
        var obj = Instantiate(attackObjectPrefab[(int)status.attackType], RandomPose(),this.transform.rotation,StageManager.Instance.GetCurrentStagePos());
        var component = obj? obj.GetComponent<AttackFunc<T>>() : null;
        
        component.Initalize(status,info,attackTarget); // ���⼭ ����� ���޵Ǵ��� Ȯ������.
        component.DeadAction(() =>
        {
            GetTurretList<T>(status).Remove(component as T);
            obj.SetActive(false); // enable �� ���� ó��?
        });

        TimeEvent.Instance.StoreTimeEventObj(obj);

        //objectsComponent?.Add(component as AttackFunc<T>);
        StoreList(component);
    }

    public void IncreaseTargetStat<T>(AttackStatus status, AttackCardInfo info) where T : AttackFunc<T>
    {
        var list = GetTurretList<T>(status);
        for (int i = 0; i < list.Count; i++)
        {
            list[i].CalcStat(status, info);
        }
    }

    // ��ų �ߵ��ɶ� 1ȸ�� ����ϵ��� �̺�Ʈ �����°ɷ� �����?
    public void SetSkillActive<T>(AttackStatus status, int skillNum) where T : AttackFunc<T>
    {
        var list = GetTurretList<T>(status);
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetSkillBool(skillNum);
        }
    }

    private void StoreList<T>(AttackFunc<T> attackFunc) where T : AttackFunc<T>
    {
        GetTurretList<T>(attackFunc._AttackStatus).Add(attackFunc as T);
    }

    private List<T> GetTurretList<T>(AttackStatus status) where T : AttackFunc<T>
    {
        switch (status.attackType)
        {
            case AttackType.laser:
                return laserList as List<T>;
            case AttackType.guided:
                return guidedList as List<T>;
            case AttackType.bullet:
                return bulletList as List<T>;
            case AttackType.trap:
                return trapList as List<T>;
            default:
                return null;
        }
    }

}
