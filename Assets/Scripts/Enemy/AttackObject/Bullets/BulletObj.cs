using KMS.Player.PlayerInteraction;
using System.Collections;
using UnityEngine;

public class BulletObj : AtkObjStat<BulletObj>,IUseSkill
{

    public override void Initialize(AttackStatus attackStatus, bool skill_1, bool skill_2)
    {
        GetAtkObjPoint(attackStatus);
        if (skill_1)
        {
            Skill();
        }
        lifeTime = 5f;
    }

    void Update()
    {
        transform.Translate(Vector3.forward* Time.deltaTime * speed);
    }

    public void Skill()
    {
        throw new System.NotImplementedException();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            damageAction.Invoke();
            gameObject.SetActive(false);
        }
    }
}