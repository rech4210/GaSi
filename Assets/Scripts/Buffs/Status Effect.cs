using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public abstract class StatusEffect : MonoBehaviour, ISetCardInfo/*, IStatusEffect*/
{
    char buffCode;
    protected BuffManager buffManager;
    CardInfo cardInfo;
    BuffStat buffStat;

    public char _BuffCode { get { return buffCode; } set { buffCode = value; } }
    public CardInfo _CardInfo { get { return cardInfo; } set { cardInfo = value; } }
    public BuffStat _BuffStat { get { return buffStat; } set { buffStat = value; } }

    public abstract void OnChecked();
    protected void FindBuffManager(BuffManager buff)
    {
        try
        {
            if (GameObject.FindWithTag("BuffManager")
            .TryGetComponent<BuffManager>(out buff))
            {
                this.buffManager = buff;
            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.LogError($"���� ���:{this.name }$���� ����: {e.Message}");
            throw e;
        }
    }
    public virtual void GetRandomCodeWithInfo(BuffData data)
    { _BuffCode = data.buffCode; _CardInfo = data.cardInfo; _BuffStat = data.stat; }

    [SerializeField] private TextMeshProUGUI buffname;
    [SerializeField] private TextMeshProUGUI information;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image frontImage;
    [SerializeField] private Image backImage;
    // �̺κ��� ī�带 ���������� �ϸ� �и� ������ �����.
    // �ٸ� ������Ʈ ������Ű�� �ű⼭ �������� �� ������ �ϴ�.
    public virtual void SetCardInfo()
    {
        buffname.text = cardInfo.cardName;
        information.text = cardInfo.information;
        description.text = cardInfo.description;
        frontImage.sprite = Resources.Load<Sprite>(Path.Combine(StringManager.Instance.buffCardResource, cardInfo.fRImage));
        backImage.sprite = Resources.Load<Sprite>(Path.Combine(StringManager.Instance.buffCardResource, cardInfo.bGImage));
    }
}
