﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Scripts from other scene objects")]
    public PlayerPointer playerPointer;
    public StatsCanvasFunctionality statCanvas;

    [Header("Objects from elsewhere in scene")]
    public GameObject cameraRoot;
    public Rigidbody playerBall;

    private Vector3 aimDirection = new Vector3();
    private Vector3 shotVector = new Vector3();

    public  float shotPowerMultiplier = 15f;
    public float maxShotPower = 100f;

    private float shotPower = 0.0f;
    private float roationInput;

    private PlayState currentPlayState;

    void Start ()
    {
        EventHub.PlayStateUpdate.AddListener(PlayStateUpdateListener);
        EventHub.BallOutofBoundsEvent.AddListener(BallOOBListener);
        UpdateAimDirection();
        currentPlayState = PlayState.PLAYERINPUT;
    }

    private void Update()
    {
        if (currentPlayState == PlayState.PLAYERINPUT)
        {
            playerBall.velocity = Vector3.zero;
            CheckForInput();
        }
    }

    void CheckForInput()
    {
        if (Input.GetAxis("Horizontal") != 0.0f)
        {
            roationInput = Input.GetAxis("Horizontal");
        }
        UpdateAimDirection();   // Put this here to ensure there's a vector even without Input

        if (Input.GetKey("space"))
        {
            shotPower += Time.deltaTime * shotPowerMultiplier;
            if (shotPower <= maxShotPower)
            {
                playerPointer.IncreasePowerIndicator();
            }         
        }

        if(Input.GetKeyUp("space"))
        {
            Debug.Log("FIIIIIIIIIIIIRE!!!!!!!");
            GameStatCarrier.isFirstShotPlayed = true;
            EventHub.BallReleased.Invoke();
            ReleaseBall();
        }
    }

    void ReleaseBall()
    {
        UpdateShotVector();
        // Send required shot Vectors to Replay
        EventHub.UpdateReleaseVectorDataEvent.Invoke(UpdatedReleaseVectors(), false);
        // Reset Items 
        shotPower = 0.0f;
        roationInput = 0.0f;
        playerPointer.ResetPowerIndicator();
        // Update Statistics
        GameStatCarrier.UpdateCurrentShots();
        statCanvas.UpdateShotsUI();
        // Release the Ball
        playerBall.AddForce(shotVector, ForceMode.VelocityChange);
    }

    #region Update-Variables Functions
    void UpdateAimDirection()
    {
        Vector3 rotateYDegrees = new Vector3(0f, roationInput, 0f);
        cameraRoot.transform.Rotate(rotateYDegrees, Space.World);
        aimDirection = cameraRoot.transform.forward;
        roationInput = 0.0f;
    }

    void UpdateShotVector()
    {
        shotVector = aimDirection.normalized;
        if (shotPower >= maxShotPower)
        {
            shotPower = maxShotPower;
        }
        shotVector *= shotPower;
    }

    private InputReleaseVectors UpdatedReleaseVectors()
    {
        InputReleaseVectors vectors = new InputReleaseVectors();
        vectors.shotVector = shotVector;
        vectors.cameraPosition = cameraRoot.transform.position;
        return vectors;
    }
    #endregion

    #region Listeners
    private void PlayStateUpdateListener(PlayState p_UpdatedState)
    {
        currentPlayState = p_UpdatedState;
        if(currentPlayState == PlayState.REPLAYING)
        {
            shotVector = new Vector3();
            aimDirection = new Vector3();
        }
    }

    private void BallOOBListener()
    {
        EventHub.UpdateReleaseVectorDataEvent.Invoke(UpdatedReleaseVectors(), false);
    }
    #endregion
}
