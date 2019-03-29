using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayState
{
    CAMERAAIMING,
    PLAYERINPUT,
    BALLRELEASED,
    REPLAYING,
    GAMEOVER
}

public class MatchLogic : MonoBehaviour
{
    [Header("Scripts from other scene objects")]
    public StatsCanvasFunctionality statCanvas;

    [Header("Objects from elsewhere in scene")]
    public Rigidbody playerBall;
    public Rigidbody redBall;
    public Rigidbody yellowBall;
    public Transform cameraRoot;

    private PlayState currentState = PlayState.PLAYERINPUT;
    private LoadLevel loader = new LoadLevel();

    private void Awake()
    {
        EventHub.InitializeEvents();
    }

    void Start ()
    {
        EventHub.BallReleased.AddListener(SetStateBallReleased);
        EventHub.CameraInPosition.AddListener(SetStatePlayerInput);
        EventHub.ReplayingEvent.AddListener(SetStateReplaying);
        EventHub.ReplayComplete.AddListener(SetStateCameraAiming);
        //EventHub.BallOutofBoundsEvent.AddListener(BallOOBReaction);
        SetStatePlayerInput();
    }

    private void Update ()
    {
        UpdateTime();
        if (currentState == PlayState.BALLRELEASED)
        {
            if (UtilityFunctions.CheckForAllBallsStopped(playerBall, redBall, yellowBall))
            {
                SetStateCameraAiming();
            }
        }
        CheckForGameOver();
    }

    private void UpdateTime()
    {
        float time = Time.timeSinceLevelLoad;
        GameStatCarrier.UpdateCurrentTime(time);
        statCanvas.UpdateTime();
    }

    private void CheckForGameOver()
    {
        if(GameStatCarrier.GetStats().currentSession.points >= 3)
        {
            SetStateGameOver();
        }
    }

    private BallPositions UpdatedBallPositions()
    {
        BallPositions ballPositions = new BallPositions();
        ballPositions.PlayerBallPos = playerBall.transform.position;
        ballPositions.RedBallPos = redBall.transform.position;
        ballPositions.YellowBallPos = yellowBall.transform.position;
        return ballPositions;
    }

    private InputReleaseVectors PreReplayCameraPosition()
    {
        InputReleaseVectors vectors = new InputReleaseVectors();
        vectors.cameraPosition = cameraRoot.position;
        return vectors;
    }

    #region Game States / Listeners
    private void SetStateCameraAiming()
    {
        currentState = PlayState.CAMERAAIMING;
        EventHub.PlayStateUpdate.Invoke(currentState);
    }

    private void SetStatePlayerInput()
    {
        currentState = PlayState.PLAYERINPUT;
        EventHub.PlayStateUpdate.Invoke(currentState);
    }

    private void SetStateBallReleased()
    {
        GameStatCarrier.pointRegisteredThisShot = false;
        // Send Ball Position Data to ReplayFunctionality
        EventHub.UpdateBallPositionDataEvent.Invoke(UpdatedBallPositions(), false);

        currentState = PlayState.BALLRELEASED;
        EventHub.PlayStateUpdate.Invoke(currentState);
    }

    private void SetStateReplaying()
    {
        // Send Ball Position Data to ReplayFunctionality
        EventHub.UpdateBallPositionDataEvent.Invoke(UpdatedBallPositions(), true);
        EventHub.UpdateReleaseVectorDataEvent.Invoke(PreReplayCameraPosition(), true);

        currentState = PlayState.REPLAYING;
        EventHub.PlayStateUpdate.Invoke(currentState);
    }

    private void SetStateGameOver()
    {
        GameStatCarrier.isFirstShotPlayed = false;
        currentState = PlayState.GAMEOVER;
        loader.LoadScene(2);
        EventHub.PlayStateUpdate.Invoke(currentState);
    }

    //private void BallOOBReaction()
    //{
    //    EventHub.UpdateBallPositionDataEvent.Invoke(UpdatedBallPositions(), true);
    //}
    #endregion
}
