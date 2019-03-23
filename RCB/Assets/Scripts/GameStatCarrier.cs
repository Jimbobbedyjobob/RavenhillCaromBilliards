using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatCarrier : MonoBehaviour {

    private PlayerStats sessionStats = new PlayerStats();
    public PlayerStats GetStats ()
    {
        return sessionStats;
    }

    public bool isContinuedSession = false;

    public void InitialiseStats(StatTriple p_LastSessionStats)
    {
        sessionStats.lastSession = p_LastSessionStats;
    }

	public void UpdateCurrentShots ()
    {
        sessionStats.currentSession.shots += 1;
        Debug.Log("Shot Count Updated  to " + sessionStats.currentSession.shots);
    }

    public void UpdateCurrentPoints()
    {
        sessionStats.currentSession.points += 1;
        Debug.Log("Point Count Updated  to " + sessionStats.currentSession.points);
    }

    public void UpdateCurrentTime(float p_time)
    {
        sessionStats.currentSession.time += p_time;
    }

    public void CurrentStatsToLastSessionStats()
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
