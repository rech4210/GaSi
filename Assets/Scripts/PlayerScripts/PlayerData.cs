using UnityEngine;

namespace KMS.Player.playerData
{
    public class PlayerData : MonoBehaviour
    {
        PlayerStatStruct playerStat;

        public PlayerStatStruct _PlayerStatStruct { get { return playerStat; } }

        // 버프 -> 데이터매니저 -> 플레이어 -> 데이터 매니저

        public void UpdatePlayerStat(PlayerStatStruct stat)
        {
            playerStat = stat;
            //playerStat.PrintPlayerStat();
            DataManager.Instance.UpdatePlayerData(playerStat, this);
        }

        private void Awake()
        {
            DataManager.Instance.PlayerStatDele = null;
            DataManager.Instance.PlayerStatDele = UpdatePlayerStat;
            DataManager.Instance.PlayerStatDele?.Invoke(new PlayerStatStruct(10, 3, 15, 0, 0, 0, 0, 0, 0));
        }

        private void OnDisable()
        {
            DataManager.Instance.PlayerStatDele = null;
        }
    }
}

