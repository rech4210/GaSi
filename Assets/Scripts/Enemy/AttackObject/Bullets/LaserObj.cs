using KMS.Player.playerData;
using KMS.Player.PlayerInteraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObj : AtkObjStat<LaserObj>, IUseSkill
{
    LaserTurret lt;
    Material laserMaterial;
    float scaleSpeed = 5f;
    int count = 0 ;
    float time = 0;
    Color emissionColor = new Color(0.5f, 0.5f, 0.5f);

    Transform PlayerPos;
    //PlayerInteraction playerInteraction;

    public override void Initialize(AttackStatus attackStatus, bool skill_1, bool skill_2)
    {
        GetAtkObjPoint(attackStatus);
        if(skill_1)
        {
            //더블 레이저
            Skill();
        }
    }


    void Start()
    {
        laserMaterial = GetComponent<MeshRenderer>().material;
        transform.rotation = new Quaternion(0,Random.rotation.y,0,Random.rotation.w);
        PlayerPos = GameManager.Instance._PlayerTransform;
        //playerInteraction = PlayerPos.GetComponent<PlayerInteraction>();


        // 플레이어를 상위 개체가 가지도록 하자.
        StartCoroutine(LaserLock());
        emissionColor *= 1.23f;
    }

    private void OnTriggerStay(Collider other)
    {
        var time = 0f;

        time = Time.deltaTime;
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && (time > 0.01f))
        {
            damageAction.Invoke();
            time = 0f;
        }
    }

    IEnumerator LaserLock()
    {
        var time = 0f;
        while (true)
        {
            time += Time.deltaTime;
            if (time > 1f)
            {
                break;
            }

            laserMaterial.color
            = new Color(Random.Range(.0f, 1), Random.Range(.0f, 1), Random.Range(.0f, 1))
            * ((1 + Mathf.Sin(Time.deltaTime)));

            yield return null;
        }

        GetComponent<BoxCollider>().enabled = true;
        laserMaterial.SetColor("_EmissionColor", emissionColor);
        gameObject.GetComponent<MeshRenderer>().material.shader = laserMaterial.shader;
        //laserMaterial.SetShaderPassEnabled("_EMISSION", false);
        //laserMaterial.SetShaderPassEnabled("_EMISSION", true);

        var scale = transform.lossyScale;
        time = 0f;
        while (true)
        {
            time += Time.deltaTime;
            transform.localScale = scale * ((Mathf.Cos(Time.deltaTime * scaleSpeed)) * 1f + Random.Range(4.5f, 5f));
            laserMaterial.SetColor("_EmissionColor", emissionColor);

            if (time > 0.4f)
            {
                Destroy(gameObject);
                break;
            }
            yield return null;
        }
    }
    public void Skill()
    {
    }
}
