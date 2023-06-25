using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractAttack : MonoBehaviour, ISetCardInfo
{
    char attackCode;
    AttackType attackType;
    private AttackStatus attackStatus;
    private AttackCardInfo attackInfo;
    public AttackCardInfo _attackInfo { get { return attackInfo; }}

    //public AttackStatus _attackStatus { get {return attackStatus ;} set { SetAttackStatus(value); }}

    //use when checked
    // �� �Լ����� �����Ŵ����� �ֽ�ȭ ����������� ������?
    public abstract void OnChecked();


    public virtual void GetRandomCodeWithInfo(char _attackCode, AttackCardInfo _cardInfo, AttackStatus _attackStatus)
    { this.attackCode = _attackCode; attackInfo = _cardInfo; this.attackStatus = _attackStatus; }

    // ����Ÿ������ ������������ ���� �Լ��� ���� ���� ����. json�� ���� Ÿ�Ա��� ����Ѵٸ� ����������.
    public virtual void CalcAttackStatus(float calcNum, string statType)
    {
        this.attackStatus.rank++;
        switch (statType)
        {
            case "duration":
                this.attackStatus.duration *= calcNum; break;
            case "point":
                this.attackStatus.point += (int)calcNum; break;
            case "scale":
                this.attackStatus.scale *= calcNum; break;
            default:
                break;
        }
    }

    public virtual void SetCardInfo()
    {
        if (this.transform.GetChild(0).GetChild(1)
            .TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI buffname))
        {
            buffname.text = _attackInfo.attackName;
        }
        else Debug.LogError("Not Setted Object You're null!!");

        if (this.transform.GetChild(0).GetChild(2)
            .TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI information))
        {
            information.text = _attackInfo.information;
        }
        else Debug.LogError("Not Setted Object You're null!!");

        if (this.transform.GetChild(0).GetChild(3)
            .TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI description))
        {
            description.text = _attackInfo.description;
        }
        else Debug.LogError("Not Setted Object You're null!!");


        if (this.transform.GetChild(0).GetChild(0).TryGetComponent<Image>(out Image frontImage))
        {

            frontImage.sprite = Resources.Load<Sprite>(Path.Combine("AttackCardResource/", _attackInfo.fRImage));
            if (frontImage.sprite == null)
            {
                Debug.Log($"There is no resource__{_attackInfo.fRImage} at: " + Path.Combine(Application.dataPath + "/AttackCardResource/", ""));
            }
        }
        else
        {
            Debug.Log("wrong Path in child frontImage");
        }

        if (this.transform.GetChild(0).TryGetComponent<Image>(out Image backImage))
        {

            backImage.sprite = Resources.Load<Sprite>(Path.Combine("AttackCardResource/", _attackInfo.bGImage));
            if (backImage.sprite == null)
            {
                Debug.Log($"There is no resource__{_attackInfo.bGImage} at: " + Path.Combine(Application.dataPath + "/AttackCardResource/", ""));
            }
        }
        else
        {
            Debug.Log("wrong Path in child backImage");
        }
        //this.transform.GetChild(0).GetChild(1).GetComponent<TextMeshPro>().text = cardInfo.BuffEnumName;
        //this.transform.GetChild(2).GetComponent<TextMeshPro>().text = cardInfo.information;
        //this.transform.GetChild(3).GetComponent<TextMeshPro>().text = cardInfo.description;

        //this.GetComponent<Image>().sprite = cardInfo.FRImage;
        //data = buffManager.SetBuffData(buffCode, stat);
        //Debug.Log(stat);
    }
}
