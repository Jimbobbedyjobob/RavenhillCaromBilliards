using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct BallPositions
{
    public Vector3 PlayerBallPos;
    public Vector3 RedBallPos;
    public Vector3 YellowBallPos;
}

public struct InputReleaseVectors
{
    public Vector3 shotVector;
    public Vector3 cameraPosition;
}

public struct LastShotData
{
    public BallPositions positions;
    public InputReleaseVectors releaseData;
}

public class ReplayFunctionality : MonoBehaviour
{
    [Header("Objects from elsewhere in scene")]
    public Rigidbody playerBall;
    public Rigidbody redBall;
    public Rigidbody yellowBall;
    public Transform cameraRoot;

    [Header("Replay Items")]
    public string outOfBoundsMessage;
    public string replayMessage;
    public GameObject replayPanel;
    public Text replayText;

    private LastShotData preReplayData;
    private LastShotData lastShotData;

    private bool isReplaying = false;
    private bool isReplayFirstFrameFinished = false;

    private PlayState currentPlayState;

    private void Start()
    {
        replayPanel.SetActive(false);

        EventHub.PlayStateUpdate.AddListener(PlayStateUpdateListener);
        EventHub.UpdateReleaseVectorDataEvent.AddListener(RecieveInputReleaseDataListener);
        EventHub.UpdateBallPositionDataEvent.AddListener(RecvievePositionReleaseDataListener);
        EventHub.BallOutofBoundsEvent.AddListener(ResetOnOutOfBoundsEvent);
    }

    private void Update()
    {
        if(isReplaying)
        {
            if(isReplayFirstFrameFinished)
            {
                if (UtilityFunctions.CheckForAllBallsStopped(playerBall, redBall, yellowBall))
                {
                    isReplaying = false;
                    isReplayFirstFrameFinished = false;
                    ResetToPreReplayPositions();
                    EventHub.ReplayComplete.Invoke();
                }
            }
            isReplayFirstFrameFinished = true;
        }
    }

    // Activate Replay Message
    // Tell the MatchLogic that we're doing a Replay
    public void OnReplayPressed()
    {
        Debug.Log("Replay Pressed");
        if (currentPlayState == PlayState.PLAYERINPUT && GameStatCarrier.isFirstShotPlayed)
        {
            Debug.Log("Replay Accepted");
            EventHub.ReplayingEvent.Invoke();
            isReplaying = true;
            isReplayFirstFrameFinished = false;
            replayText.text = replayMessage;
            replayPanel.SetActive(true);
            // Set positions for start of replay
            SetPositions(lastShotData);
            // Fire Ball
            playerBall.AddForce(lastShotData.releaseData.shotVector, ForceMode.VelocityChange);
        }
    }

    public void ResetOnOutOfBoundsEvent()
    {
        Debug.Log("OOB Reset Called");
        SetPositions(lastShotData);
        replayText.text = outOfBoundsMessage;
        replayPanel.SetActive(true);
        EventHub.ReplayComplete.Invoke();
    }

    private void ResetToPreReplayPositions()
    {
        Debug.Log("Replay Reset Called");
        isReplayFirstFrameFinished = false;
        SetPositions(preReplayData);
        HideReplayMessages();
    }

    private void HideReplayMessages()
    {
        replayPanel.SetActive(false);
    }

    private void SetPositions(LastShotData p_ShotData)
    {
        // Reposition GameObjects
        playerBall.transform.position = p_ShotData.positions.PlayerBallPos;
        redBall.transform.position = p_ShotData.positions.RedBallPos;
        yellowBall.transform.position = p_ShotData.positions.YellowBallPos;
        cameraRoot.transform.position = p_ShotData.releaseData.cameraPosition;
        // Remove Velocity
        playerBall.velocity = Vector3.zero;
        redBall.velocity = Vector3.zero;
        yellowBall.velocity = Vector3.zero;
    }

    #region Listeners
    private void RecieveInputReleaseDataListener(InputReleaseVectors p_ReleaseData, bool p_IsReplay)
    {
        if(p_IsReplay)
        {
            preReplayData.releaseData = new InputReleaseVectors();
            preReplayData.releaseData = p_ReleaseData;
        }
        else
        {
            lastShotData.releaseData = new InputReleaseVectors();
            lastShotData.releaseData = p_ReleaseData;
        }
    }

    private void RecvievePositionReleaseDataListener(BallPositions p_BallPositions, bool p_IsReplay)
    {
        if (p_IsReplay)
        {
            preReplayData.positions = new BallPositions();
            preReplayData.positions = p_BallPositions;
        }
        else
        {
            lastShotData.positions = new BallPositions();
            lastShotData.positions = p_BallPositions;
        }
    }

    private void PlayStateUpdateListener(PlayState p_UpdatedState)
    {
        currentPlayState = p_UpdatedState;
        if (currentPlayState == PlayState.CAMERAAIMING || currentPlayState == PlayState.PLAYERINPUT)
        {
            HideReplayMessages();
        }
    }
    #endregion
}
