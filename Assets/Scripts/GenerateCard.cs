using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//json ���� ���� ��η� ��������Ʈ ��������


public class GenerateCard : MonoBehaviour
{
    private char buffCode;
    private Dictionary<char, BuffStat> statGenerateDic;
    private Dictionary<char, CardInfo> infoGenerateDic;
    public GameObject cardPrefab;
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
            //------�� �κ�, ���� �����Ͱ� ������ ��� �ֽ�ȭ�� �Ƹ� �ȵɰ���.. �� �κ� �����Ŵ������� ����------//
            this.statGenerateDic = buffManager.StatToGenerate();
            this.infoGenerateDic = buffManager.InfoToGenerate();

        }
        Generate();
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();

            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            //�ߺ� ������ ���� ��� ����
            foreach (var result in raycastResults) 
            {
                if(result.gameObject.transform.parent.TryGetComponent<StatusEffect>(out StatusEffect component))
                {
                    buffEffect = component;
                    buffEffect.OnChecked();
                }
                else if(result.gameObject.transform.parent.TryGetComponent<AbstractAttack>(out AbstractAttack component2))
                {

                    Debug.Log("ã���� ���� ������Ʈ ���");
                }
                
            }
        }
    }

    // ��� ����
    /* 1. ī�� ����
     * 2. �ڵ� ���� Ȯ�� ���
     * 3. 
     * 
     */
    void Generate() // -> ���⿡ Ư��, �Ǹ�, õ��, �Ϲ�ī�� �����ϵ��� ¥��
    {
        for (int i = 0; i < cardCount; i++)
        {
            var cardObj = Instantiate(cardPrefab,this.transform);
            //buffCode = (char)Random.Range(1, statGenerateDic.Count + 1);
            // ������ ī��� ��ġ�ϴ°͵� ������ ��.
            buffCode = (char)0;

            if (infoGenerateDic.TryGetValue(buffCode, out CardInfo cardInfo))
            {
                cardObj.GetComponent<StatusEffect>()
                .GetRandomCodeWithInfo(buffCode, cardInfo);
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
