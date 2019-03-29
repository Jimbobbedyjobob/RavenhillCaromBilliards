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

    // Camnera Rotations Variables
    private Quaternion toCamGoalRot;
    private Quaternion fromCamRot;
    private float timeCountCamRot = 0.0f;
    private bool isCamRotCorrect = false;

    // Camera Root Rotation Variables
    private Quaternion toRootGoalRot;
    private Quaternion fromRootRot;
    private float timeCountRootRot = 0.0f;
    private float rootRotSpeed = 10.0f;
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
        toCamGoalRot = cameraTransform.rotation;
        EventHub.PlayStateUpdate.AddListener(PlayStateUpdateListener);
        EventHub.CameraInPosition.Invoke();
    }

    void Update()
    {
        if (currentPlayState == PlayState.CAMERAAIMING)
        {
            // Change to goal values
            
            // Cam Root Rotation
            if (!isRootRotCorrect)
            {
                transform.rotation = toRootGoalRot;
                isRootRotCorrect = true;
            }

            // Camera Position
            CheckBallPosition(); // Just to make sure we're heading to the current ball pos
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

    private void CalculateInitialAimDireciont()
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

    // Listener
    private void PlayStateUpdateListener(PlayState p_UpdatedState)
    {
        currentPlayState = p_UpdatedState;
        if (currentPlayState == PlayState.CAMERAAIMING)
        {
            // Reset all Slerp Variables
            // Camera Rotation
            timeCountCamRot = 0.0f;
            fromCamRot = cameraTransform.localRotation;
            // Camera Root Rotation
            timeCountRootRot = 0.0f;
            fromRootRot = transform.rotation;
            CalculateInitialAimDireciont();
            // Camera Root Position
            rootPosStartTime = Time.time;
            CheckBallPosition();
            fromRootPos = transform.position;
            // Confirmation Bools
            isCamRotCorrect = false;
            isRootRotCorrect = false;
            isRootPosCorrect = false;
        }
    }
}
