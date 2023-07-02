using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    Vector3 Mytransform;

    Rigidbody Myrigid;
    [Range(5, 20)] public float _MovePower = 15;
    [Range(5, 20)] public float _JumpPower = 5;

    Vector3 normalizedVector; // �븻������ ����
    private Dictionary<KeyCode, Vector3> keyValuePairs = new Dictionary<KeyCode, Vector3>();
    

    void Start()
    {
        AddDictionary();
        Mytransform = this.gameObject.transform.position;
        Myrigid = GetComponent<Rigidbody>();
        Myrigid.MovePosition(Mytransform);  // ���� ������Ʈ�� �ʱ� ��ġ�� �̵�
    }


    void FixedUpdate()
    {
        Mytransform = transform.position;

        if (Input.anyKey)
        {
            KeySetting();
        }
    }


    void KeySetting()
    {
        foreach (var dic in keyValuePairs)
        {
            if (Input.GetKey(dic.Key))
            {
                vector_Normalized(dic.Value);
            }
        }
    }

    void vector_Normalized(Vector3 vector3)
    {

        normalizedVector = vector3.normalized * _MovePower * Time.deltaTime; //�밢�� �������� �ε巴�� �ϱ����� ���� �븻������ ����
        Mytransform += normalizedVector;
        Myrigid.MovePosition(Mytransform);


        // ���� �浹 ����, ���� ��ǥ��� �̵��� ���������� ���� �̵� ����
    }

    public void AddDictionary()
    {
        keyValuePairs.Add(KeyCode.W, transform.forward);
        keyValuePairs.Add(KeyCode.A, -transform.right);
        keyValuePairs.Add(KeyCode.S, -transform.forward);
        keyValuePairs.Add(KeyCode.D, transform.right);
    }
}
