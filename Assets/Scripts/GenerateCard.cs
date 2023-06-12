using System.Collections.Generic;
using UnityEngine;
public class GenerateCard : MonoBehaviour
{
    private char buffCode;
    private Dictionary<char, BuffStat> allBuffArchive;
    public GameObject cardPrefab;
    //���� ī�� �����տ� �� ���, �̹��� ���� ������ ����ü�� �ʿ��ҵ�.

    public int cardCount;

    private void Start()
    {
        if (GameObject.FindWithTag("BuffManager")
            .TryGetComponent<BuffManager>(out BuffManager buffManager))
        {
            this.allBuffArchive = buffManager.DicToGenerate();
        }
        Generate();
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
            cardObj.GetComponent<PowerUp>()
                .GetRandomBuffCode((char)Random.Range(1, allBuffArchive.Count + 1));
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
