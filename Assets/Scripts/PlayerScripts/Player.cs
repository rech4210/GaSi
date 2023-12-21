using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class Player : MonoBehaviour
{
    Vector3 direction;
    Vector3 desVector;
    Vector3 refValue = Vector3.zero;
    Rigidbody rigid;

    [SerializeField] private float velocityLimit = 0.2f;
    private float walkDeaccelerationOnX;
    private float walkDeaccelerationOnZ;
    [SerializeField] private float walkDeacceleration = 5f;

    [SerializeField][Range(5, 30)] private float movePower = 10;

    Vector3 normalizedVector; // �븻������ ����
    private Dictionary<KeyCode, Vector3> keyValuePairs = new Dictionary<KeyCode, Vector3>();
    

    void Start()
    {
        //AddDictionary();
        rigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * movePower;
    }
    private void FixedUpdate()
    {
        rigid.velocity = new Vector3(Mathf.SmoothDamp(direction.x, 0, ref walkDeaccelerationOnX,walkDeacceleration),0,
                                     Mathf.SmoothDamp(direction.z, 0,ref walkDeaccelerationOnZ,walkDeacceleration));
        if (rigid.velocity.magnitude < velocityLimit)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
        //rigid.velocity = Vector3.SmoothDamp(rigid.velocity, direction, ref refValue, .2f) * Time.fixedDeltaTime * movePower;
    }

    //void KeySetting()aw
    //{
    //    foreach (var dic in keyValuePairs)
    //    {
    //        if (Input.GetKey(dic.Key))
    //        {
    //            vector_Normalized(dic.Value);
    //        }
    //    }
    //}

    Vector3 vector_Normalized(Vector3 vector3)
    {

        return normalizedVector = vector3 * movePower; //�밢�� �������� �ε巴�� �ϱ����� ���� �븻������ ����
        // ���� �浹 ����, ���� ��ǥ��� �̵��� ���������� ���� �̵� ����
    }

    //public void AddDictionary()
    //{
    //    keyValuePairs.Add(KeyCode.W, transform.forward);
    //    keyValuePairs.Add(KeyCode.A, -transform.right);
    //    keyValuePairs.Add(KeyCode.S, -transform.forward);
    //    keyValuePairs.Add(KeyCode.D, transform.right);
    //}
    ////add dash
}
