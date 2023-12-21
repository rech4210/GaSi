using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    [SerializeField] protected GameObject player;

    private void Awake()
    {
        // �������� �ʱ�ȭ, �÷��̾� ���� �ʱ�ȭ, ��� ó�����ֱ�
        Instantiate(player, new Vector3(0,.5f,0),Quaternion.identity);
    }
}
