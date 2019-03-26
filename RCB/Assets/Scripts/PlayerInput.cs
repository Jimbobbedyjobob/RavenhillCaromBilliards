using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    [Header("Scripts from other scene objects")]
    public PlayerUI playerUI;
    public StatsCanvasFunctionality statCanvas;
    public MatchLogic matchLogic;
    [Header("Objects from elswqhere in scene")]
    public GameObject playerCanvasHost;
    public Rigidbody playerBall;

    private Vector3 aimDirection = new Vector3();
    private Vector3 shotVector = new Vector3();

    public  float shotPowerMultiplier = 10000f;

    private float shotPower = 0.0f;
    private float roationInput;

    private PlayState currentPlayState;

	void Start ()
    {
        if (playerUI == null || statCanvas == null || matchLogic == null)
        {
            Debug.LogError("PlayerBallCollision is missing script references!!");
        }

        matchLogic.playStateUpdate.AddListener(PlayStateUpdateReaction);
        UpdateAimDirection();
    }

    private void Update()
    {
        if (currentPlayState == PlayState.PLAYERINPUT)
        {
            CheckForInput();
        }
        else if (currentPlayState == PlayState.SHOTRUNNING)
        {
            CheckForBallHasStopped();
        }
    }

    private void PlayStateUpdateReaction(PlayState p_UpdatedState)
    {
        currentPlayState = p_UpdatedState;
    }

    void CheckForInput()
    {
        if(Input.GetAxis("Horizontal") != 0.0f)
        {
            roationInput = Input.GetAxis("Horizontal");
            UpdateAimDirection();
        }

        if(Input.GetKey("space"))
        {
            shotPower += Time.deltaTime;
            playerUI.IncreasePowerIndicator();
        }

        if(Input.GetKeyUp("space"))
        {
            
            matchLogic.SetStatePlayingOut();
            ReleaseBall();
        }
    }

    void UpdateAimDirection()
    {
        Vector3 rotateYDegrees = new Vector3(0f, roationInput, 0f);
        playerCanvasHost.transform.Rotate(rotateYDegrees, Space.Self);
        aimDirection = playerCanvasHost.transform.forward;
    }

    void UpdateShotVector()
    {
        shotVector = aimDirection;
        shotVector *= shotPower;
    }

    void ReleaseBall()
    {
        UpdateShotVector();
        shotPower = 0.0f;
        GameStatCarrier.UpdateCurrentShots();
        statCanvas.UpdateShotsUI();
        playerUI.ResetPowerIndicator();
        playerBall.AddForce(aimDirection, ForceMode.VelocityChange);
    }

    void CheckForBallHasStopped()
    {
        if(playerBall.velocity.magnitude <= 0.01f)
        {
            matchLogic.SetStateAimAndPower();
            playerBall.velocity = Vector3.zero;
        }
    }
}
