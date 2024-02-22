using Cysharp.Threading.Tasks;
using KMS.Player.playerData;
using KMS.Player.PlayerInteraction;
using System;
using UnityEngine;

public abstract class AttackFunc<T> : MonoBehaviour, ITimeEvent, IGetPlayer where T : AttackFunc<T>
{
    AttackCardInfo attackInfo;
    AttackStatus attackStatus;
    protected PlayerInteraction playerInteraction;

    [SerializeField] private GameObject player;
    public GameObject Player { get { return player; }/* protected set { player = value; }*/}

    public void Initalize(AttackStatus status, AttackCardInfo info, GameObject attackTarget)
    {
        player = attackTarget;
        AttackStatus = status;
        AttackCardInfo = info;
        playerInteraction = player.GetComponent<PlayerInteraction>();
        CalcStat(status,info);
    }

    protected bool sk_1 = false;
    protected bool sk_2 = false;
    protected bool sk_3 = false;

    [SerializeField] protected GameObject attackObject;
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //var rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        //Gizmos.matrix = rotationMatrix;
        Gizmos.DrawRay(this.transform.position, Player.transform.position - transform.position);
    }

    public AttackCardInfo AttackCardInfo { get => attackInfo; set => attackInfo = value; }
    public AttackStatus AttackStatus { get => attackStatus; set => attackStatus = value; }

    public int _Point { protected get { return attackStatus.point; } set { attackStatus.point = value; } }
    public float _Duration { protected get { return attackStatus.duration; } set { attackStatus.duration = value; } }
    public float _Scale { protected get { return attackStatus.scale; } set { attackStatus.scale = value; } }
    public float _Speed { protected get { return attackStatus.speed; } set { attackStatus.speed = value; } }
    public int _Rank { protected get { return attackStatus.rank; } set { attackStatus.rank = value; } }




    // 이거 분리해야함.
    public abstract void CalcStat(AttackStatus status, AttackCardInfo info);

    public virtual Quaternion ChaseTarget(GameObject player, GameObject target)
    {
        return Quaternion.LookRotation(player.transform.position - target.transform.position);
    }

    protected abstract void ExcuteAttack();

    public abstract void TimeEvent(float time);


    public void SetSkillBool(int i)
    {
        switch (i)
        {
            case 0: sk_1 = true; break;
            case 1: sk_2 = true; break;
            case 2: sk_3 = true; break;
            /*            case 2: sk_3 = true; break;
                        case 2: sk_3 = true; break;*/
            default:
                break;
        }
    }

    public void DeadAction(Action action) { spawnerDeadAction = action; }

    protected Action spawnerDeadAction;

    private void OnDisable()
    {
        //Destroy(gameObject);
    }

    public void GetPlayer(GameObject player)
    {
        this.player = player;
        playerInteraction = player.GetComponent<PlayerInteraction>();
    }
}
