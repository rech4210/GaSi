using System.Collections.Generic;
using System.IO;
using UnityEngine;
using KMS.Player.playerData;

public class DataManager : Manager<DataManager>, IGetDict<int,BuffData>,IGetDict<int,AttackData>, IGetDict<int,PlayerStatStruct>
{
    string path = null;
    int buffCounts = 100;

    #region 플레이어 정보
    PlayerStatStruct playerStatStruct;
    Transform playerTransform;

    public delegate void PlayerStatDelegate(PlayerStatStruct stat);
    public PlayerStatDelegate PlayerStatDele;

    public PlayerStatStruct _playerStat { get { return playerStatStruct; }set { }}
    public Transform _playerTransform { get { return playerTransform; } set { } }
    public void UpdatePlayerData(PlayerStatStruct stat, PlayerData player) 
    {
        playerStatStruct = stat;
        playerTransform = player.gameObject.transform;
    }


    #endregion
    #region 딕셔너리 반환 부분
    public Dictionary<int, BuffData> ReturnDict(Dictionary<int, BuffData> dict) {return BuffsArchive;}
    public Dictionary<int, AttackData> ReturnDict(Dictionary<int, AttackData> dict) { return  AttackArchive;}
    public Dictionary<int, PlayerStatStruct> ReturnDict(Dictionary<int, PlayerStatStruct> dict) { return playerLastData; }

    private Dictionary<int, BuffData> BuffsArchive = new();
    private Dictionary<int, AttackData> AttackArchive = new();
    private Dictionary<int, PlayerStatStruct> playerLastData = new();
    #endregion
    private void Awake()
    {
        //SaveBuffJson();
        //SaveAttackJson();
        var textasset  = Resources.Load<TextAsset>("Json/BuffData");
        BuffsArchive = LoadJson<BuffStructure, int, BuffData>(textasset).MakeDict();
        foreach (var item in BuffsArchive)
        {
            //UnityEngine.Debug.Log($"code:{item.Value}");
        }

        var txta = Resources.Load<TextAsset>("Json/AttackData");

        AttackArchive = LoadJson<AttackStructure, int, AttackData>(txta).MakeDict();
        foreach (var item in AttackArchive)
        {
            //UnityEngine.Debug.Log($"code:{item.Value}");
        }
    }

    #region Json 파싱 저장 및 호출
    public BuffStatEnum statEnum { get; set; }
    public void SaveBuffJson()
    {
        path = Path.Combine(Application.dataPath, StringManager.Instance.buffData) ?? "wrongPath";
        //string jsonData = null;

        File.WriteAllText(path, "");

        BuffStructure buffdata = new BuffStructure();

        for (int i = 0; i < buffCounts; i++)
        {
            BuffData buff = new BuffData((char)i, new BuffStat(1, 1, 1, 1), new CardInfo(statEnum = BuffStatEnum.empty, "1", "1", "1", "1", "1"));

            buffdata.buffDatas[i] = buff;
        }
        string jsonData = JsonUtility.ToJson(buffdata, true);
        File.WriteAllText(path, jsonData);
    }
    public AttackType attackType { get; set; }
    public AttackCardEnum attackEnum { get; set; }
    public void SaveAttackJson()
    {
        path = Path.Combine(Application.dataPath, StringManager.Instance.attakData) ?? "wrongPath";
        //string jsonData = null;
        if (path == null) return;

        File.WriteAllText(path, "");

        AttackStructure structure = new AttackStructure();

        for (int i = 0; i < 10; i++)
        {
            AttackData attackData = new AttackData((char)i, new AttackStatus(attackType, 1, 1, 1, 1, 1), new AttackCardInfo(attackEnum, "1", "1", "1", "1", "1"));

            structure.attackDatas[i] = attackData;

        }
        string jsonData = JsonUtility.ToJson(structure, true);
        File.WriteAllText(path, jsonData);
    }


    //public void SaveJson<T>(string path)
    //{
    //    if(path == null) return;

    //    File.WriteAllText(path, "");

    //    string jsonData = JsonUtility.ToJson(structure, true);
    //    File.WriteAllText(path, jsonData);
    //}

    Loader LoadJson<Loader, Key, Value>(TextAsset path) where Loader : IDataLoader<Key, Value>
    {
        //if (string.IsNullOrEmpty(path))
        //{
        //    Debug.Log(path + "null or empty");
        //}
        //string jsonData = File.ReadAllText(path);
        //TextAsset textAsset= Resources.Load<TextAsset>(path);
        Loader data = JsonUtility.FromJson<Loader>(path.ToString());
        //Debug.Log(data);
        return data;
    }
    private string SetPass(string _path)
    {
        return path = Path.Join(Application.dataPath, _path);
    }

 
    #endregion
}


