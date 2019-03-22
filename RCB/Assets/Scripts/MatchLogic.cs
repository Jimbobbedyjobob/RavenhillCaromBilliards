using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLogic : MonoBehaviour {

    public StatsCanvasFunctionality statCanvas;

    private enum PlayState
    {
        AIMANDPOWER,
        PLAYINGOUT,
        GAMEOVER
    }
    private PlayState currentState;

    private LoadLevel loader = new LoadLevel();

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
        GameStatCarrier.UpdateCurrentTime(time);
        statCanvas.UpdateTime();
    }

    void CheckForGameOver()
    {
        if(GameStatCarrier.GetStats().currentSession.points >= 3)
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
        loader.LoadScene(2);
    }
    #endregion
}
