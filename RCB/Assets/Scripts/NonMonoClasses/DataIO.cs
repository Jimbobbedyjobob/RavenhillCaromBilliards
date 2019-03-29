using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataIO {

    public static bool lastSessionStatsExist = false;
    private static string gameDataPath = "/billiardsStats.json";

    public static void LoadLastSessionStats()
    {
        string filePath;
        filePath = Application.persistentDataPath + gameDataPath;
        Debug.Log(filePath);

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

    public static void SaveGameStats(StatTriple p_LastGameStats)
    {
        string filePath;
        filePath = Application.persistentDataPath + gameDataPath;
        Debug.Log(filePath);
        string statsAsJson = JsonUtility.ToJson(p_LastGameStats);
        File.WriteAllText(filePath, statsAsJson);

    }
}
