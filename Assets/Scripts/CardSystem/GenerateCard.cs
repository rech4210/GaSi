using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private Dictionary<int, BuffData> buffArchive = new();
    private Dictionary<int, AttackData> attackArchive = new();

    private Dictionary<int, BuffStat> containStatGenerateDic = new();
    private Dictionary<int, AttackStatus> containAttackStatusGenerateDic = new();

    public GameObject cardPrefab;
    public GameObject attackCardPrefab;

    private List<GameObject> buffCardList = new List<GameObject>();
    private List<GameObject> attackCardList = new List<GameObject>();


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

        InvokeRepeating("AttackGenerate", 0f, 10f);
        InvokeRepeating("BuffGenerate", 0.1f, 15f);

        // �� �κ� �׼� ��Ƽĳ��Ʈ�� ó���ϱ�

        //StartCoroutine(BuffGenerate());
        //StartCoroutine(AttackGenerate());

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
                    for (int i = 0; i < buffCardList.Count; i++)
                    {
                        Destroy(buffCardList[i]);
                        Time.timeScale = 1.0f;
                    }
                    buffCardList.Clear();
                }
                else if(result.gameObject.transform.parent.TryGetComponent<AbstractAttack>(out AbstractAttack attack))
                {
                    attack.OnChecked();
                    for (int i = 0; i < attackCardList.Count; i++)
                    {
                        Destroy(attackCardList[i]);
                        Time.timeScale = 1.0f;
                    }
                    attackCardList.Clear();
                }
                else { Debug.Log("not defined raytarget"); }

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
        //Time.timeScale = .0f;
        containStatGenerateDic = buffManager.ContainStatToGenerate();
        for (int i = 0; i < cardCount; i++)
        {
            var cardGameObj = Instantiate(cardPrefab, this.transform);
            //buffCode = (char)Random.Range(1, statGenerateDic.Count + 1);
            // ������ ī��� ��ġ�ϴ°͵� ������ ��.
            char _buffCode = (char)0;
            var data = buffArchive[_buffCode];

                
            var targetCard = cardGameObj.GetComponent<StatusEffect>() ?? null;
            buffCardList.Add(cardGameObj);
            if (containStatGenerateDic.TryGetValue(_buffCode, out BuffStat stat) /*&& containStatGenerateDic[_buffCode].rank > 0*/)
            {
                targetCard?.GetRandomCodeWithInfo(data);
                targetCard?.SetCardInfo();
            }
            else
            {
                targetCard?.GetRandomCodeWithInfo(data);
                targetCard?.SetCardInfo();
                //������ ��� ��������ٰ���?
            }
        }

        //yield return new WaitForSecondsRealtime(15f);
        Time.timeScale = .0f;

        //if (buffCardList.Count != 0)
        //{
        //    Debug.Log("�������� ���� �ູ �Ҹ�");
        //    RemoveCard(buffCardList);
        //}
    }

    
    void AttackGenerate()
    {

        containAttackStatusGenerateDic = attackGenerator.ContainAttackStatToGenerate();

        //ī�� ī��Ʈ ����
        for (int i = 0; i < cardCount; i++)
        {
            var cardGameObj = Instantiate(attackCardPrefab, this.transform);
            attackCardList.Add(cardGameObj);
            cardGameObj.GetComponent<RectTransform>().anchoredPosition = new Vector2((-210 + (i * 140)), 0f);


            //buffCode = (char)Random.Range(1, statGenerateDic.Count + 1);
            char _attackCode = (char)UnityEngine.Random.Range(0, 4);

            var data = attackArchive[_attackCode];

            AbstractAttack targetCard = null;

            switch (data.attackStatus.attackType)
            {
                case AttackType.laser:
                    targetCard = cardGameObj.AddComponent<LaserAttack>();
                    break;
                case AttackType.guided:
                    targetCard = cardGameObj.AddComponent<GuidedAttack>();
                    break;
                case AttackType.bullet:
                    targetCard = cardGameObj.AddComponent<BulletAttack>();
                    break;
                case AttackType.trap:
                    targetCard = cardGameObj.AddComponent<TrapAttack>();
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
                //Debug.Log(targetCard?._AttackData.attackInfo.attackCardEnum);
                targetCard?.SetCardInfo();
            }
            else
            {
                targetCard?.GetRandomCodeWithInfo(data);
                //Debug.Log(targetCard?._AttackData.attackInfo.attackCardEnum);
                targetCard?.SetCardInfo();
            }

        }

        //yield return new WaitForSecondsRealtime(15f);
        Time.timeScale = .0f;
        //if (attackCardList.Count != 0)
        //{
        //    Debug.Log("�������� ���� �г�Ƽ �ο�");
        //    RemoveCard(attackCardList);
        //}


    }


    private void RemoveCard(List<GameObject> carList)
    {
        for (int i = 0; i < carList.Count; i++) 
        {
            Destroy(carList[i]);
        }
        carList.Clear();
    }
}
