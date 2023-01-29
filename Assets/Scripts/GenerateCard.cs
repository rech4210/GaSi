using System.Collections.Generic;
using UnityEngine;
public class GenerateCard : MonoBehaviour
{
    public BuffManager buffManager;
    public GameObject cardPrefab; //- >����� �ٸ� ���۷�����
    BuffStorage buffStorage;
    int maxRange;

    private Dictionary<BuffStorage, Dictionary<StatusEffect, BuffStat>>
            dictFullBuff = new Dictionary<BuffStorage, Dictionary<StatusEffect, BuffStat>>();

    [SerializeField]
    private Dictionary<StatusEffect,BuffStat> 
        dictFullBuffStatus = new Dictionary<StatusEffect,BuffStat>();

    int buffStorageLength = System.Enum.GetValues(typeof(BuffStorage)).Length;

    private void Awake()
    {
        for (int i = 0; i < buffStorageLength; i++)
        {
            dictFullBuff.Add(buffStorage = (BuffStorage)i, dictFullBuffStatus); // ��ųʸ��� �� �ֱ�
            // �̰� �Ŵ����� �����ұ�?
        }

        // ���� ���ʷ���Ʈ
    }
    private void Start()
    {
        Generate();
    }
    void Generate()
    {
        for (int i = 0; i < 5; i++)
        {
            buffStorage = (BuffStorage)Random.Range(0, buffStorageLength);
            var d = Instantiate(cardPrefab, buffManager.transform);

            if (buffManager.CheckBuff(buffStorage))
            {
            }
            else
            {
                var buffToTest = buffManager.ReturnBuff(buffStorage);
                d.AddComponent(buffToTest.GetType()); 
                // �ν��Ͻ��� ���� ���۷��� ���� ��� �Ұ���?
            }
        }
    }
}
