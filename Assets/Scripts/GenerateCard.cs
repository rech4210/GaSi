using System.Collections.Generic;
using UnityEngine;
public class GenerateCard : MonoBehaviour
{
    public BuffManager buffManager;
    public GameObject cardPrefab; //- >����� �ٸ� ���۷�����
    BuffEnumStorage buffStorage;

    int buffStorageLength = System.Enum.GetValues(typeof(BuffEnumStorage)).Length;

    private void Start()
    {
        Generate();
    }
    void Generate() // -> ���⿡ Ư��, �Ǹ�, õ��, �Ϲ�ī�� �����ϵ��� ¥��
    {
        //for (int i = 0; i < 3; i++)
        //{
        //    buffStorage = (BuffEnumStorage)Random.Range(0, buffStorageLength);
        //    var d = Instantiate(cardPrefab, buffManager.transform);

        //    var asd = buffManager.ReturnBuff(buffStorage);

        //}
    }
}
