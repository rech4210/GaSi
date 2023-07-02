using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class AttackGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] attackObjectPrefab;

    private GameObject attackTarget;
    private List<GameObject> attackObjects = new List<GameObject>();
    private List<AttackFunc> objectsComponent = new List<AttackFunc>();
    //private List<AbstractAttack> attackObjList;

    private void Start()
    {
        FindPlayer();
    }

    protected void FindPlayer()
    {
       // �÷��̾� ã�°� ����ƽ���� ó���ص� �ɵ� .����
        try
        {
            if (GameObject.FindWithTag("Player").TryGetComponent<Player>(out Player player))
            {
                // ���ӿ�����Ʈ ������Ʈ ��ü�� ���������� ���� ��������.
                attackTarget = player.gameObject;
                Debug.Log(attackTarget + "���� ���");
            }
        }
        catch (NullReferenceException e)
        {
            Debug.Log("IsThere no player");
            throw e;
        }
    }

    public void Generate(AttackStatus status)
    {
        Debug.Log(((int)status.attackType).ToString());
        var obj = Instantiate(attackObjectPrefab[(int)status.attackType], this.transform.position,this.transform.rotation);
        attackObjects.Add(obj);

        var component = obj.GetComponent<AttackFunc>();

        Debug.Log(component.ToString());
        Debug.Log(attackTarget.ToString());
        component._Player = attackTarget;
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
            if (obj.attackType == status.attackType) 
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
            if (obj.attackType == status.attackType)
            {
                obj.GetComponent<AttackFunc>()?.Invoke(skill,0);
            }
            // ���ݿ� ���� ���� (�������)
            // 
            //obj.GetComponent<>();
        }
    }

}
