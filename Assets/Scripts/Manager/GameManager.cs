using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    [SerializeField] protected GameObject player;

    private void Awake()
    {
        // �������� �ʱ�ȭ, �÷��̾� ���� �ʱ�ȭ, ��� ó�����ֱ�
        var obj = Instantiate(player, new Vector3(0,.5f,0),Quaternion.identity);
        obj.SetActive(true);
    }
}
