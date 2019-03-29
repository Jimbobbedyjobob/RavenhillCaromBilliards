using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{   
    [Header("Scripts from other scene objects")]
    public MatchLogic matchLogic;

    [Header("Objects from elsewhere in scene")]
    public Transform playerBall;
    public Transform playerCanvasHost;
    public Transform cameraTransform;

    // Camera Root Rotation Variables
    private Quaternion toRootGoalRot;
    private bool isRootRotCorrect = false;

    // Camera Root Position Variables
    private Vector3 toRootGoalPos;
    private Vector3 fromRootPos;
    private float rootPosJourneyTime = 1.5f;
    private float rootPosStartTime = 0.0f;
    private bool isRootPosCorrect = false;

    private PlayState currentPlayState;

    private void Start()
    {
        EventHub.PlayStateUpdate.AddListener(PlayStateUpdateListener);
        EventHub.CameraInPosition.Invoke();
    }

    void Update()
    {
        if (currentPlayState == PlayState.CAMERAAIMING)
        {
            if (!isRootRotCorrect)
            {
                transform.rotation = toRootGoalRot;
                isRootRotCorrect = true;
            }

            CheckBallPosition();

            if (!isRootPosCorrect)
            {
                isRootPosCorrect = UtilityFunctions.PosSlerpWorld(  transform, fromRootPos, toRootGoalPos, rootPosJourneyTime,  rootPosStartTime);
            }

            if (isRootRotCorrect && isRootPosCorrect)
            {
                EventHub.CameraInPosition.Invoke();
            }
        } 

        if (currentPlayState == PlayState.BALLRELEASED || currentPlayState == PlayState.REPLAYING)
        {
            transform.LookAt(playerBall, Vector3.up);
        }
    }

    private void CalculateInitialAimDirecion()
    {
        float yOffset = 0.04f;
        Vector3 yOffsetCenter = new Vector3(0f, yOffset, 0f);
        Vector3 directionToCenter = yOffsetCenter - playerBall.position;
        toRootGoalRot = Quaternion.LookRotation(directionToCenter, Vector3.up);
    }

    private void CheckBallPosition()
    {
        toRootGoalPos = playerBall.position;
        if( transform.position != toRootGoalPos)
        {
            isRootPosCorrect = false;
        }
    }

    private void PlayStateUpdateListener(PlayState p_UpdatedState)
    {
        currentPlayState = p_UpdatedState;
        if (currentPlayState == PlayState.CAMERAAIMING)
        {
            // Reset all Slerp Variables
            CalculateInitialAimDirecion();
            rootPosStartTime = Time.time;
            CheckBallPosition();
            fromRootPos = transform.position;
            isRootRotCorrect = false;
            isRootPosCorrect = false;
        }
    }
}
