using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InputReleaseData : UnityEvent<InputReleaseVectors, bool>
{
}
[System.Serializable]
public class PlayStateEvent : UnityEvent<PlayState>
{
}
[System.Serializable]
public class PositionReleaseData : UnityEvent<BallPositions, bool>
{
}
[System.Serializable]
public class CollisionSFXRelayEvent : UnityEvent<AudioClip, string>
{
}

public static class EventHub
{
    // Events Invoked by PlayerInput
    public static UnityEvent BallReleased;
    public static InputReleaseData UpdateReleaseVectorDataEvent;
    // Events Invoked by MatchLogic (& ReplayFunctions)
    public static PlayStateEvent PlayStateUpdate;
    public static PositionReleaseData UpdateBallPositionDataEvent;
    // Events Invoked by CameraMovement
    public static UnityEvent CameraInPosition;
    // Events Invoked by ReplayFunctionality
    public static UnityEvent ReplayingEvent;
    public static UnityEvent ReplayComplete;
    // Events Invoked by BallEscapeDetection
    public static UnityEvent BallOutofBoundsEvent;

    // INitialize all events
    public static void InitializeEvents()
    {
        // Events Invoked by PlayerInput
        if (BallReleased == null)
            BallReleased = new UnityEvent();
        if (UpdateReleaseVectorDataEvent == null)
            UpdateReleaseVectorDataEvent = new InputReleaseData();
        // Events Invoked by MatchLogic
        if (PlayStateUpdate == null)
            PlayStateUpdate = new PlayStateEvent();
        if (UpdateBallPositionDataEvent == null)
            UpdateBallPositionDataEvent = new PositionReleaseData();
        // Events Invoked by CameraMovement
        if (CameraInPosition == null)
            CameraInPosition = new UnityEvent();
        // Events Invoked by ReplayFunctionality
        if (ReplayingEvent == null)
            ReplayingEvent = new UnityEvent();
        if (ReplayComplete == null)
            ReplayComplete = new UnityEvent();
        // Events Invoked by BallEscapeDetection
        if (BallOutofBoundsEvent == null)
            BallOutofBoundsEvent = new UnityEvent();
    }
}
