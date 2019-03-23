using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLogic : MonoBehaviour {

    public StatsCanvasFunctionality statCanvas;

    // Utility Classes
    LoadLevel loader;
    GameStatCarrier statCarrier;

    private enum PlayState
    {
        AIMANDPOWER,
        PLAYINGOUT,
        GAMEOVER
    }
    private PlayState currentState;

    private void Awake()
    {
        GameObject singleton = GameObject.Find("Singleton");

        if (singleton != null)
        {
            loader = singleton.GetComponent<LoadLevel>();
            statCarrier = singleton.GetComponent<GameStatCarrier>();
        }
        else Debug.Log("MatchLogic Cannot Find Singleton Scripts!");
    }

    void Start ()
    {
        SetStateAimAndPower();
        UpdateTime();
    }

    void Update ()
    {
        UpdateTime();
        CheckForGameOver();
    }

    void UpdateTime()
    {
        float time = Time.timeSinceLevelLoad;
        statCarrier.UpdateCurrentTime(time);
        statCanvas.UpdateTime();
    }

    void CheckForGameOver()
    {
        if(statCarrier.GetStats().currentSession.points >= 3)
        {
            SetStateGameOver();
        }
    }

    #region Game State
    public bool CanReceiveInput()
    {
        if (currentState == PlayState.AIMANDPOWER)
        {
            return true;
        }
        else return false;
    }

    void SetStateAimAndPower()
    {
        currentState = PlayState.AIMANDPOWER;
    }

    void SetStatePlayingOut()
    {
        currentState = PlayState.PLAYINGOUT;
    }

    void SetStateGameOver()
    {
        currentState = PlayState.GAMEOVER;
        loader.LoadScene(3);
    }
    #endregion
}
