using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayState
{
    PLAYERINPUT,
    SHOTRUNNING,
    GAMEOVER
}

[System.Serializable]
public class PlayStateEvent : UnityEvent<PlayState>
{
}

public class MatchLogic : MonoBehaviour {

    public StatsCanvasFunctionality statCanvas;
    public PlayStateEvent playStateUpdate;

    private PlayState currentState;
    private LoadLevel loader = new LoadLevel();

    private void Awake()
    {
        if (playStateUpdate == null)
        {
            playStateUpdate = new PlayStateEvent();
        }

    }

    void Start ()
    {
        if (statCanvas == null || loader == null)
        {
            Debug.LogError("PlayerBallCollision is missing script references!!");
        }

        SetStateAimAndPower();
        UpdateTime();
    }

    private void OnEnable()
    {
        playStateUpdate.Invoke(currentState);
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
    public void SetStateAimAndPower()
    {
        currentState = PlayState.PLAYERINPUT;
        Debug.Log("GameState = " + currentState);
        playStateUpdate.Invoke(currentState);
    }

    public void SetStatePlayingOut()
    {
        GameStatCarrier.pointRegisteredThisShot = false;
        currentState = PlayState.SHOTRUNNING;
        Debug.Log("GameState = " + currentState);
        playStateUpdate.Invoke(currentState);
    }

    void SetStateGameOver()
    {
        currentState = PlayState.GAMEOVER;
        loader.LoadScene(2);
        Debug.Log("GameState = " + currentState);
        playStateUpdate.Invoke(currentState);
    }
    #endregion
}
