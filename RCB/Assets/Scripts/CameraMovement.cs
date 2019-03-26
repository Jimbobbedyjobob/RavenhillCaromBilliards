using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public MatchLogic matchLogic;

    public Transform playerBall;
    public Transform playerCanvasHost;
    public Transform cameraRoot;

    private Quaternion toForCamRotation;
    private Quaternion fromForCamRotation;
    private float timeCountCamRot = 0.0f;
    private bool initialCamRotationAchieved = false;

    private Quaternion toForRootRotation;
    private Quaternion fromForRootRotation;
    private float timeCountRootRot = 0.0f;
    private bool initialRootRotationAchieved = false;

    private Vector3 fromForRootPosition;
    private float rootPositionJourneyTime = 1.5f;
    private float rootPositionStartTime = 0.0f;
    private bool initialRootPositionAchieved = false;

    private PlayState currentPlayState;

    void Start()
    {
        if (playerBall == null)
        {
            Debug.LogError("Camera cannot find Ball!!");
        }

        toForCamRotation = transform.rotation;
        matchLogic.playStateUpdate.AddListener(PlayStateUpdateReaction);
    }

    void Update()
    {
        if (currentPlayState == PlayState.SHOTRUNNING)
        {
            transform.LookAt(playerBall, Vector3.up);
        }
        if (currentPlayState == PlayState.PLAYERINPUT)
        {
            // Slerp to goal values
            // Returns bool ready for the next frame
            if(!initialCamRotationAchieved)
            {
                initialCamRotationAchieved = UtilityFunctions.QuaternionSlerpLocal(transform, fromForCamRotation, toForCamRotation, timeCountCamRot);
                if (!initialCamRotationAchieved) Debug.Log("Slerp To CAM ROT ACHIEVED!!");
            }

            if (!initialRootRotationAchieved)
            {
                initialRootRotationAchieved = UtilityFunctions.QuaternionSlerpWorld(cameraRoot, fromForRootRotation, playerCanvasHost.rotation, timeCountRootRot);
                if (initialRootRotationAchieved) Debug.Log("Slerp To ROOT ROT ACHIEVED!!");
            }

            if (!initialRootPositionAchieved)
            {
                initialRootPositionAchieved = UtilityFunctions.Vector3SlerpWorld(cameraRoot, fromForRootPosition, playerBall.position, rootPositionJourneyTime, rootPositionStartTime);
                if (initialRootPositionAchieved) Debug.Log("Slerp To ROOT POS ACHIEVED!!");
            }
        }
    }

    private void PlayStateUpdateReaction(PlayState p_UpdatedState)
    {
        currentPlayState = p_UpdatedState;
        if (currentPlayState == PlayState.PLAYERINPUT)
        {
            // Reset all Slerp time counters
            // Set all Slerp 'from' values
            timeCountCamRot = 0.0f;
            timeCountRootRot = 0.0f;
            rootPositionStartTime = Time.time;
            fromForCamRotation = transform.localRotation;
            fromForRootRotation = cameraRoot.rotation;
            fromForRootPosition = cameraRoot.position;
            initialCamRotationAchieved = false;
            initialRootRotationAchieved = false;
            initialRootPositionAchieved = false;
            Debug.Log("Slerp To Aim @ CAM REQUIRED!!");
        }
    }
}
