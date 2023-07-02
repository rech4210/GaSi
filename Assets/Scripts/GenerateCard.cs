using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//json ���� ���� ��η� ��������Ʈ ��������

public class GenerateCard : MonoBehaviour
{
    private float timeSinceStart;
    private char buffCode;
    private Dictionary<char, BuffStat> statGenerateDic;
    private Dictionary<char, CardInfo> infoGenerateDic;
    private Dictionary<char, AttackStatus> attackStatusGenerateDic;
    private Dictionary<char, AttackCardInfo> attacInfoGenerateDic;

    public GameObject cardPrefab;
    public GameObject attackCardPrefab;

    StatusEffect buffEffect;
    //���� ī�� �����տ� �� ���, �̹��� ���� ������ ����ü�� �ʿ��ҵ�.

    public int cardCount;

    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEventData;

    [SerializeField]
    EventSystem eventSystem;


    private void Start()
    {
        
        graphicRaycaster = GetComponent<GraphicRaycaster>();

        if (GameObject.FindWithTag("BuffManager")
            .TryGetComponent<BuffManager>(out BuffManager buffManager))
        {
            this.statGenerateDic = buffManager.StatToGenerate();
            this.infoGenerateDic = buffManager.InfoToGenerate();
            this.attackStatusGenerateDic = buffManager.AttackStatToGenerate();
            this.attacInfoGenerateDic = buffManager.AttackInfoToGenerate();
        }

        AttackGenerate();
    }

    // �� �κп��� ������ ��� ó������?

    // 1. attack�� buff �������� �Լ��� ���� ����� ó���Ѵ�.
    // 2. �б�� ó���Ѵ�.
    // 3. ������ �ʰ� �ϳ��� ������Ʈ�� �ް�, Add ������Ʈ ó��

    // ���� ���ʷ���Ʈ ����ؾ� �� ��.
    /*
     * 1. ī�带 ������ �̺�Ʈ�� ó���Ǿ�, ��ü�� �����Ǿ�� ��.
     * 2. ������ ��ü�� ������Ʈ�� �������� ������.
     * 3. �̹� ������ ��ü�鵵 �ڵ������� ������Ʈ �ǵ��� ������ ��.
     * 4. �׷��ٸ� �� ī�忡 Ư������� �־ �����Ͽ��� �ϴ���..? �ƴ϶�� 
     * 5. �ϰ����� ��ɰ� �Լ� ����� ���� ���ʷ����;ȿ� ����Ʈ�� �����ؾ��Ұ� ����.
    */
    private void Update()
    {
        if ((timeSinceStart  = Time.realtimeSinceStartup)% 30 == 0)
        {
            
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) /*�����̽��ٷε�*/)
        {
            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();

            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            //�ߺ� ������ ���� ��� ����
            foreach (var result in raycastResults) 
            {
                if(result.gameObject.transform.parent.TryGetComponent<StatusEffect>(out StatusEffect statusEffect))
                {
                    buffEffect = statusEffect;
                    buffEffect.OnChecked();
                }
                else if(result.gameObject.transform.parent.TryGetComponent<AbstractAttack>(out AbstractAttack attack))
                {
                    attack.OnChecked();
                }
                else
                { Debug.Log("not defined raytarget"); }
            }
        }
    }

    // ��� ����
    /* 1. ī�� ����
     * 2. �ڵ� ���� Ȯ�� ���
     * 3. ī�� ���� �ð�
     * 
     */
    void BuffGenerate() // -> ���⿡ Ư��, �Ǹ�, õ��, �Ϲ�ī�� �����ϵ��� ¥��
    {
        for (int i = 0; i < cardCount; i++)
        {
            var cardObj = Instantiate(cardPrefab,this.transform);
            //buffCode = (char)Random.Range(1, statGenerateDic.Count + 1);
            // ������ ī��� ��ġ�ϴ°͵� ������ ��.
            char _buffCode = (char)0;

            var targetCard = cardObj.GetComponent<StatusEffect>() ??  null;

            if (infoGenerateDic.TryGetValue(_buffCode, out CardInfo cardInfo))
            {
                targetCard?.GetRandomCodeWithInfo(buffCode, cardInfo, statGenerateDic[buffCode]);
                targetCard?.SetCardInfo();
                //������ ��� ��������ٰ���?
            }
            else Debug.Log("Missing value");
            
            //Will be change
        }
    }

    void AttackGenerate()
    {
        
        //ī�� ī��Ʈ ����
        for (int i = 0; i < cardCount; i++)
        {
            var cardObj = Instantiate(attackCardPrefab, this.transform);

            cardObj.GetComponent<RectTransform>().anchoredPosition = new Vector2((-210 + (i * 140)),0f) ;

            //buffCode = (char)Random.Range(1, statGenerateDic.Count + 1);
            char _attackCode = (char)UnityEngine.Random.Range(0,3);
            AbstractAttack targetCard = null;
            switch (attackStatusGenerateDic[_attackCode].attackType)
            {
                case AttackType.laser:
                    targetCard = cardObj.AddComponent<LaserAttack>();
                    break;
                case AttackType.guided:
                    targetCard = cardObj.AddComponent<TrapAttack>();
                    break;
                case AttackType.bullet:
                    targetCard = cardObj.AddComponent<BulletAttack>();
                    break;
                case AttackType.trap:
                    targetCard = cardObj.AddComponent<TrapAttack>();
                    break;
                default:
                    Debug.Log("There is no maching attack type");
                    break;
            }

            // ���� ������ ��� ó���Ұ���? ���⼭ �����ð���?
            if (attacInfoGenerateDic.TryGetValue(_attackCode, out AttackCardInfo attacinfo))
            {
                targetCard?.GetRandomCodeWithInfo(_attackCode, attacinfo, attackStatusGenerateDic[_attackCode]);
                Debug.Log(targetCard?._AttackCardInfo.attackCardEnum);
                targetCard?.SetCardInfo();
            }
            else Debug.Log("Missing value");

            //Will be change
        }


        //for (int i = 0; i < 3; i++)
        //{
        //    buffStorage = (BuffEnumStorage)Random.Range(0, buffStorageLength);
        //    var d = Instantiate(cardPrefab, buffManager.transform);

        //    var asd = buffManager.ReturnBuff(buffStorage);

        //}
    }
}
