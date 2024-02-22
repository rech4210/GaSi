using UnityEngine;
using KMS.Player.playerData;
using KMS.UI.UIanimation;

namespace KMS.Player.PlayerInteraction
{
    public class PlayerInteraction : MonoBehaviour
    {
        private PlayerData playerData;
        private PlayerStatStruct stat;
        [SerializeField]private UIanimation ianimation;

        bool isLive = true;

        private void Start()
        {
            playerData = GetComponent<PlayerData>();
        }

        private void GetPlayerStat()
        {
            stat = playerData._PlayerStatStruct;
        }

        public void GetDamaged(float dmg)
        {
            if(isLive)
            {
                GetPlayerStat();
                stat.health -= dmg;
                ianimation.MakeText((int)dmg); // �ε��Ҽ��� �߸�
                playerData.UpdatePlayerStat(stat);
                //�� �κ� �ֽ�ȭ �ȵǴ��� ( �Ѱ��� ���)
                Debug.Log(stat.health);

                if (stat.health <= 0f && isLive)
                {
                    isLive = false;
                    DeadEvents.Instance.ExecuteEvent();
                }
            }
        }
        void OnEnable() // �÷��̾� ������ �����ϱ����� ó����. Awake �Ǵ� manager���� ȣ���ϵ��� ����?
        {
            isLive = true;

            // �̺κ� �������� �������� �ʱ�ȭ
        }

        private void OnTriggerEnter(Collider other)
        {
            //if (other.gameObject.CompareTag("Background")) { }
            //else if (isLive)
            //{
            //    //����ó��?
            //    var attackObject = other.gameObject.GetComponent<LaserObj>();
            //    //�� �κ� �����ϼ� �ΰ��� ��ġ������.
            //    GetDamaged(attackObject.Point * 15);
            //    attackObject.OnHitTarget(); // �� �κ� �׼����� �Ѱ��ָ� ������ (�������� Ÿ�� ��ü�� Ȯ���ؾ��Ҷ�)
            //}
        }

    }
}

