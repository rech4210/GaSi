using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ���� Ÿ�� ��ü

public abstract class Buff : MonoBehaviour
{

    public int point;
    public abstract void OnChecked();
    public abstract void BuffUse();
    public abstract void BuffUp();
    

}
