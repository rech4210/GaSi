using System.Collections.Generic;
using UnityEngine;

public class AttackGenerator : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] attackObjectPrefab;

    private Dictionary<int, AttackData> allAttackStatArchive = new();
    private Dictionary<int, AttackStatus> containAttackDict = new();

    private GameObject attackTarget;
    private List<IGetPlayer> getPlayerCommandList = new();

    private List<LaserTurret> laserList = new List<LaserTurret>();
    private List<TrapTurret> trapList = new List<TrapTurret>();
    private List<GuidedTurret> guidedList = new List<GuidedTurret>();
    private List<BulletTurret> bulletList = new List<BulletTurret>();

    private void Start()
    {
        allAttackStatArchive = DataManager.Instance.ReturnDict(allAttackStatArchive);
        TracePlayerTransform();
    }

    private void Update()
    {
        if(attackTarget == null)
        {
            TracePlayerTransform();
        }
    }
    public void AddorUpdateAttackDictionary(int attackCode)
    {
        //과연 생성과 강화는 따로있는데 어떻게 딕셔너리 구분지어서 설정할건지?
        if (containAttackDict.ContainsKey(attackCode))
        {
            //method which use for player through out buffstat with buffcode
            containAttackDict[attackCode] = allAttackStatArchive[attackCode].attackStatus;
            //Debug.Log($"랭크 증가");
        }
        else
        {
            //Debug.Log(allAttackStatArchive[attackCode].attackStatus);
            containAttackDict.Add(attackCode, allAttackStatArchive[attackCode].attackStatus); //각각에 인스턴스로 존재해버림, 이 데이터 값들을 버프매니저에서 통합으로 관리해야함.
            //Debug.Log("없는 공격 추가 : " + allAttackStatArchive[attackCode].attackInfo.attackName + " " + "현재 버프 갯수:" + containAttackDict.Count);
        }
        //Debug.Log($"대상 공격 : {allAttackStatArchive[attackCode].attackInfo.attackName}, " +
        //    $"스탯 상승 : {containAttackDict[attackCode].point}, " +
        //    $"랭크 : {containAttackDict[attackCode].rank}");
    }
    public Dictionary<int, AttackStatus> ContainAttackStatToGenerate()
    {
        return containAttackDict;
    }

    private void TracePlayerTransform()
    {
        attackTarget = DataManager.Instance._playerTransform.gameObject;
        getPlayerCommandList.ForEach((obj) =>
        {
             obj.GetPlayer(attackTarget);
        });

    }


    Vector3 RandomPose() => new Vector3(1, 1, 1);
    //{
    //    return new Vector3(
    //    UnityEngine.Random.Range(23f, -23f),
    //    UnityEngine.Random.Range(0, 0),
    //    UnityEngine.Random.Range(4f, -23f));
    //}
        //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y+5f, transform.localPosition.z); 

    public void Generate<T>(AttackStatus status, AttackCardInfo info) where T : AttackFunc<T>
    {
        var obj = Instantiate(attackObjectPrefab[(int)status.attackType], RandomPose(),this.transform.rotation,StageManager.Instance.GetCurrentStagePos());
        var commandTarget = obj.GetComponent<IGetPlayer>();
        getPlayerCommandList.Add(commandTarget);
        Debug.Log(getPlayerCommandList.Count);
        
        var component = obj? obj.GetComponent<AttackFunc<T>>() : null;
        
        component.Initalize(status,info,attackTarget); // 여기서 제대로 전달되는지 확인하자.
        component.DeadAction(() =>
        {
            GetTurretList<T>(status).Remove(component as T);
            getPlayerCommandList.Remove(commandTarget);
            TimeEvent.Instance.RemoveTimeEventObj(obj);
            //obj.SetActive(false); // enable 시 제거 처리?
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

    // 스킬 발동될때 1회만 사용하도록 이벤트 던지는걸로 만들까?
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
        GetTurretList<T>(attackFunc.AttackStatus).Add(attackFunc as T);
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
