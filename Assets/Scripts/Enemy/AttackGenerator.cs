using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class AttackGenerator : MonoBehaviour
{
    public GameObject attackObject;

    private List<AbstractAttack> attackObjList;

    void AttackGenerate()
    {
        var obj = Instantiate(attackObject,Vector3.zero,Quaternion.identity);
        var _abstract = obj.GetComponent<AbstractAttack>();
        attackObjList.Add(_abstract);

        // �ƹ����� ���⼭ �ʱ� ������ ��������� �ҵ�. Buffmanager -> CardGen -> AttackGen -> ����
        //if (_abstract != null ) { _abstract.SetAttackStatus(); }
        //else 
    }

    private void IncreaseTargetStat()
    {
        //foreach (var attack in attackObjList) { attack.CalcAttackStatus()}
    }

}
