using TMPro;
using UnityEngine;

public enum UIAniamtionType
{
    Pop,
    RollUp,
    Boom
};

namespace KMS.UI.UIanimation
{
    public class UIanimation : MonoBehaviour
    {
        [SerializeField] private TMP_Text textPrefab;
        private GameObject UIEffectTarget;
        

        UIAniamtionType uiAniType = UIAniamtionType.Pop;
        public void MakeText(int point)
        {
            var asset = Instantiate(textPrefab, Vector3.zero,Quaternion.identity,this.transform);
            asset.text = point.ToString();

            switch (uiAniType)
            {
                case UIAniamtionType.Pop:
                    PopAniamtion(asset);
                    break;
                case UIAniamtionType.RollUp:
                    RollUpAniamtion(asset);
                    break;
                case UIAniamtionType.Boom:
                    BoomAniamtion(asset);
                    break;
            }
        }

        private void PopAniamtion(TMP_Text text)
        {
            text.GetComponent<Rigidbody>().AddExplosionForce(1f, this.transform.position, 1f, 0.5f, ForceMode.Impulse);
        }
        private void RollUpAniamtion(TMP_Text text)
        {

        }
        private void BoomAniamtion(TMP_Text text)
        {

        }
    }
}
