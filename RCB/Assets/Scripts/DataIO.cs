using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataIO : MonoBehaviour {

    public bool lastSessionStatsExist = false;
    private string gameDataPath = "/SaveGameData/billiardsStats.json";

    // Utility Classes
    GameStatCarrier statCarrier;

    private void Start()
    {
        statCarrier = GetComponent<GameStatCarrier>();
    }

    public void LoadLastSessionStats()
    {
        string filePath;
        filePath = Application.dataPath + gameDataPath;

        if (File.Exists(filePath))
        {
            string statsAsJson = File.ReadAllText(filePath);
            StatTriple lastSessionStats = JsonUtility.FromJson<StatTriple>(statsAsJson);
            statCarrier.InitialiseStats(lastSessionStats);
            lastSessionStatsExist = true;
        }
        else
        {
            Debug.Log("Possible Error: No Previous Game Stats Found!");
            lastSessionStatsExist = false;
        }
    }

    public void SaveGameStats()
    {
        StatTriple lastGameStats = statCarrier.GetStats().lastSession;

        string filePath;
        string statsAsJson = JsonUtility.ToJson(lastGameStats);
        filePath = Application.dataPath + gameDataPath;
        File.WriteAllText(filePath, statsAsJson);
    }
}
