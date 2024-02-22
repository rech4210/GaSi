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
                ianimation.MakeText((int)dmg); // 부동소수점 잘림
                playerData.UpdatePlayerStat(stat);
                //이 부분 최신화 안되는중 ( 겉값만 계속)
                Debug.Log(stat.health);

                if (stat.health <= 0f && isLive)
                {
                    isLive = false;
                    DeadEvents.Instance.ExecuteEvent();
                }
            }
        }
        void OnEnable() // 플레이어 추적을 실행하기위해 처리함. Awake 또는 manager에서 호출하도록 제어?
        {
            isLive = true;

            // 이부분 스테이지 생성마다 초기화
        }

        private void OnTriggerEnter(Collider other)
        {
            //if (other.gameObject.CompareTag("Background")) { }
            //else if (isLive)
            //{
            //    //예외처리?
            //    var attackObject = other.gameObject.GetComponent<LaserObj>();
            //    //이 부분 수정하셈 두개가 겹치고있음.
            //    GetDamaged(attackObject.Point * 15);
            //    attackObject.OnHitTarget(); // 이 부분 액션으로 넘겨주면 좋을듯 (레이저가 타겟 개체를 확인해야할때)
            //}
        }

    }
}

