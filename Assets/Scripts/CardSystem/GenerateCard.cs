using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GenerateCard : MonoBehaviour
{
    private float timeSinceStart;
    Queue<Action> actions = new Queue<Action>();

    public void getact(Action action)
    {
        actions.Enqueue(action);
    }

    BuffManager buffManager;
    AttackGenerator attackGenerator;

    private Dictionary<char, BuffData> buffArchive = new();
    private Dictionary<char, AttackData> attackArchive = new();

    private Dictionary<char, BuffStat> containStatGenerateDic = new();
    private Dictionary<char, AttackStatus> containAttackStatusGenerateDic = new();

    public GameObject cardPrefab;
    public GameObject attackCardPrefab;

    private List<GameObject> cards = new List<GameObject>();

    //���� ī�� �����տ� �� ���, �̹��� ���� ������ ����ü�� �ʿ��ҵ�.

    public int cardCount; // �̰� �� public���� �������.
    #region start �Լ��� point ����
    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEventData;

    [SerializeField]
    EventSystem eventSystem;

    private void Start()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();

        buffArchive = DataManager.Instance.ReturnDict(buffArchive);
        attackArchive = DataManager.Instance.ReturnDict(attackArchive);
        try
        {
            if (GameObject.FindWithTag("AttackGenerator")
            .TryGetComponent(out AttackGenerator attack))
            {
                this.attackGenerator = attack;
            }
            if (GameObject.FindWithTag("BuffManager")
            .TryGetComponent(out BuffManager buff))
            {
                this.buffManager = buff;
            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.LogError($"���� ���:{this.name}$���� ����: {e.Message}");
            throw e;
        }

        //test();
        //getact(()=>AttackGenerate());
        AttackGenerate();

    }

    #endregion

    // ���� ���ʷ���Ʈ ����ؾ� �� ��.
    /*
     * 1. ī�带 ������ �̺�Ʈ�� ó���Ǿ�, ��ü�� �����Ǿ�� ��.
     * 2. ������ ��ü�� ������Ʈ�� �������� ������.
     * 3. �̹� ������ ��ü�鵵 �ڵ������� ������Ʈ �ǵ��� ������ ��.
     * 4. �׷��ٸ� �� ī�忡 Ư������� �־ �����Ͽ��� �ϴ���..? �ƴ϶�� 
     * 5. �ϰ����� ��ɰ� �Լ� ����� ���� ���ʷ����;ȿ� ����Ʈ�� �����ؾ��Ұ� ����.
    */

    async void test()
    {
        Task a = new Task(() => AttackGenerate());
        a.Start();
        a.Wait(5000);
        await a;
        Debug.Log("delay end");
    }

    
    private void Update()
    {
        //if(actions.Count > 0)
        //{
        //    actions.Dequeue().Invoke();
        //}


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
                    statusEffect.OnChecked();
                    for (int i = 0; i < cards.Count; i++)
                    {
                        Destroy(cards[i]);
                    }
                    cards.Clear();
                }
                else if(result.gameObject.transform.parent.TryGetComponent<AbstractAttack>(out AbstractAttack attack))
                {
                    attack.OnChecked();
                    for (int i = 0; i < cards.Count; i++)
                    {
                        Destroy(cards[i]);
                    }
                    cards.Clear();
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


    //ī�� ������ ���ڿ� + ���� �����Ͽ� �ش��ϴ� ī�� �����ϵ���??
    void BuffGenerate() // -> ���⿡ Ư��, �Ǹ�, õ��, �Ϲ�ī�� �����ϵ��� ¥��
    {
        containStatGenerateDic = buffManager.ContainStatToGenerate();
        for (int i = 0; i < cardCount; i++)
        {
            var cardObj = Instantiate(cardPrefab,this.transform);
            //buffCode = (char)Random.Range(1, statGenerateDic.Count + 1);
            // ������ ī��� ��ġ�ϴ°͵� ������ ��.
            char _buffCode = (char)0;
            var data = buffArchive[_buffCode];

            var targetCard = cardObj.GetComponent<StatusEffect>() ??  null;
            if(containStatGenerateDic.TryGetValue(_buffCode, out BuffStat stat) /*&& containStatGenerateDic[_buffCode].rank > 0*/)
            {
                targetCard?.GetRandomCodeWithInfo(_buffCode, data.cardInfo, stat);
                targetCard?.SetCardInfo();
            }
            else
            {
                targetCard?.GetRandomCodeWithInfo(_buffCode, data.cardInfo, data.stat);
                targetCard?.SetCardInfo();
                //������ ��� ��������ٰ���?
            }
        }
    }

    void AttackGenerate()
    {
        // ���� : ���� ������ ī�� �����ϵ��� ���� �߰��ϱ�

        containAttackStatusGenerateDic = attackGenerator.ContainAttackStatToGenerate();

        //ī�� ī��Ʈ ����
        for (int i = 0; i < cardCount; i++)
        {
            var cardObj = Instantiate(attackCardPrefab, this.transform);
            cards.Add(cardObj);
            cardObj.GetComponent<RectTransform>().anchoredPosition = new Vector2((-210 + (i * 140)),0f);


            //buffCode = (char)Random.Range(1, statGenerateDic.Count + 1);
            char _attackCode = (char)UnityEngine.Random.Range(0,4);

            var data = attackArchive[_attackCode];

            AbstractAttack targetCard = null;

            switch (data.attackStatus.attackType)
            {
                case AttackType.laser:
                targetCard = cardObj.AddComponent<LaserAttack>();
                break;
                case AttackType.guided:
                    targetCard = cardObj.AddComponent<GuidedAttack>();
                    break;
                case AttackType.bullet:
                    targetCard = cardObj.AddComponent<BulletAttack>();
                    break;
                case AttackType.trap:
                    targetCard = cardObj.AddComponent<TrapAttack>();
                    break;
                default:
                    Debug.Log("There is no maching attack type ���ǵ��� ���� ī���Դϴ�");
                    break;
            }

            // ���� ��ųʸ��� ���� ����� �̷��� �ִٸ�.
            // ��, ��� ����ī��� ���� Ÿ���� �����Ҷ� ���
            if (containAttackStatusGenerateDic.TryGetValue(_attackCode, out AttackStatus containAttackStat))
            {
                targetCard?.GetRandomCodeWithInfo(data);
                Debug.Log(targetCard?._AttackData.attackInfo.attackCardEnum);
                targetCard?.SetCardInfo();
            }
            else
            {
                targetCard?.GetRandomCodeWithInfo(data);
                Debug.Log(targetCard?._AttackData.attackInfo.attackCardEnum);
                targetCard?.SetCardInfo();
            }

        }
    }
}
