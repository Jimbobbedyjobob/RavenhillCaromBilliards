using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Transform playerBall;

    public MatchLogic matchLogic;
    public Transform playerCanvasHost;
    public RectTransform pointerTransform;

    public float pointerScaleMultiplier;

    private Vector3 toInitialAimVector;
    private Quaternion toForInitialAimRotation;
    private Quaternion fromForInitialAimRotation;
    private float timeCountInitiamAimRot = 0.0f;
    private bool initialAimRotationAchieved = false;

    private PlayState currentPlayState;

    void Start ()
    {
        if (playerCanvasHost == null || matchLogic == null)
        {
            Debug.LogError("PlayerBallCollision is missing script references!!");
        }

        matchLogic.playStateUpdate.AddListener(PlayStateUpdateReaction);

        CalculateInitialAimDireciont();
    }

    private void Update()
    {
        if(currentPlayState == PlayState.PLAYERINPUT && !initialAimRotationAchieved)
        {
            initialAimRotationAchieved = UtilityFunctions.QuaternionSlerpWorld(playerCanvasHost, fromForInitialAimRotation, toForInitialAimRotation, timeCountInitiamAimRot);
            if(initialAimRotationAchieved) Debug.Log("SlerpToInitialAimRotation COMPLETED!!");
            //SlerpToInitialAimRotation();
        }
    }

    private void PlayStateUpdateReaction(PlayState p_UpdatedState)
    {
        currentPlayState = p_UpdatedState;
        if (currentPlayState == PlayState.PLAYERINPUT)
        {
            initialAimRotationAchieved = false;
            fromForInitialAimRotation = playerCanvasHost.rotation;
            CalculateInitialAimDireciont();
            SetUIVisibility(true);
            SetUIPosition();
            Debug.Log("Slerp To Aim @ UI REQUIRED!!");
        }
        else if (currentPlayState == PlayState.SHOTRUNNING)
        {
            SetUIVisibility(false);
        }
    }

    void SetUIPosition()
    {
        playerCanvasHost.transform.position = playerBall.position;
    }

    void SetUIVisibility(bool p_VisibilityBool)
    {
        playerCanvasHost.gameObject.SetActive(p_VisibilityBool);
    }

    private void CalculateInitialAimDireciont()
    {
        float yOffset = 0.04f;
        Vector3 yOffsetCenter = new Vector3(0f, yOffset, 0f);
        Vector3 directionToCenter = yOffsetCenter - playerBall.position;
        toForInitialAimRotation = Quaternion.LookRotation(directionToCenter, Vector3.up);
    }

    //private void SlerpToInitialAimRotation()
    //{
    //    if (playerCanvasHost.transform.rotation != toForInitialAimRotation)
    //    {
    //        playerCanvasHost.rotation = Quaternion.Slerp(fromForInitialAimRotation, toForInitialAimRotation, timeCountInitiamAimRot);
    //        timeCountInitiamAimRot = timeCountInitiamAimRot + Time.deltaTime;
    //    }
    //    else
    //    {
    //        initialAimRotationAchieved = true;
    //        Debug.Log("SlerpToInitialAimRotation COMPLETED!!");
    //    }
    //}

    public void IncreasePowerIndicator()
    {
        float newHeight = pointerTransform.rect.height + (Time.deltaTime * pointerScaleMultiplier);
        pointerTransform.sizeDelta = new Vector2(100f, newHeight);
    }

    public void ResetPowerIndicator()
    {
        pointerTransform.sizeDelta = new Vector2(100f, 100f);
    }
}
