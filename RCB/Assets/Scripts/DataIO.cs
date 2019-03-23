using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataIO {

    public static bool lastSessionStatsExist = false;
    private static string gameDataPath = "/SaveGameData/billiardsStats.json";

    public static void LoadLastSessionStats()
    {
        string filePath;
        filePath = Application.dataPath + gameDataPath;

        if (File.Exists(filePath))
        {
            string statsAsJson = File.ReadAllText(filePath);
            StatTriple lastSessionStats = JsonUtility.FromJson<StatTriple>(statsAsJson);
            GameStatCarrier.InitialiseStats(lastSessionStats);
            lastSessionStatsExist = true;
        }
        else
        {
            Debug.Log("Possible Error: No Previous Game Stats Found!");
            lastSessionStatsExist = false;
        }
    }

    public static void SaveGameStats()
    {
        StatTriple lastGameStats = GameStatCarrier.GetStats().lastSession;

        string filePath;
        string statsAsJson = JsonUtility.ToJson(lastGameStats);
        filePath = Application.dataPath + gameDataPath;
        File.WriteAllText(filePath, statsAsJson);
    }
}
