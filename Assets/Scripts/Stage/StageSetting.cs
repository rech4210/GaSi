using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageSetting : MonoBehaviour
{
    //�� ������������ ������ ��ġ�ҽ� �ʱⰪ �������ֱ�
    public abstract void StageOn();
    public abstract void StageOff();
}