using KMS.Player.PlayerInteraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObj : AtkObjStat<TrapObj>, IUseSkill
{
    Vector3 RandomPose()
    {
        return new Vector3(
        UnityEngine.Random.Range(17f, -17f),
        0.1f,
        UnityEngine.Random.Range(1.2f, -20f));
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = RandomPose();
        StartCoroutine(Attack());
    }

    public override void Initialize(AttackStatus attackStatus, bool skill_1, bool skill_2)
    {
        GetAtkObjPoint(attackStatus);
        if (skill_1)
        {
            Skill();
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<MeshCollider>().isTrigger = true;
        gameObject.GetComponent<MeshRenderer>().material.color= Color.red;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            damageAction.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void Skill()
    {
        throw new System.NotImplementedException();
    }
}
