using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatCarrier {

    private static PlayerStats sessionStats = new PlayerStats();
    public static PlayerStats GetStats ()
    {
        return sessionStats;
    }

    public static bool isContinuedSession = false;

    public static bool pointRegisteredThisShot = false;

    public static void InitialiseStats(StatTriple p_LastSessionStats)
    {
        sessionStats.lastSession = p_LastSessionStats;
    }

	public static void UpdateCurrentShots ()
    {
        sessionStats.currentSession.shots += 1;
    }

    public static void UpdateCurrentPoints()
    {
        sessionStats.currentSession.points += 1;
        pointRegisteredThisShot = true;
    }

    public static void UpdateCurrentTime(float p_time)
    {
        sessionStats.currentSession.time += p_time;
    }

    public static void CurrentStatsToLastSessionStats()
    {
        sessionStats.lastSession = sessionStats.currentSession;
        sessionStats.currentSession = new StatTriple();
    }
}


[System.Serializable]
public struct PlayerStats {

    public StatTriple lastSession;
    public StatTriple currentSession;
}

[System.Serializable]
public struct StatTriple {

    public int shots;
    public int points;
    public float time;
}
