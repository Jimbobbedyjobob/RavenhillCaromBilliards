using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Transform playerBall;

    public MatchLogic matchLogic;
    public Canvas playerUI;
    public RectTransform pointerTransform;

    public float pointerScaleMultiplier;

	void Start ()
    {
        pointerTransform = pointerTransform.transform as RectTransform;

        if (playerUI == null || matchLogic == null)
        {
            Debug.LogError("PlayerBallCollision is missing script references!!");
        }

        matchLogic.playStateUpdate.AddListener(PlayStateUpdateReaction);
    }
	
    private void PlayStateUpdateReaction(PlayState p_UpdatedState)
    {
        if (p_UpdatedState == PlayState.PLAYERINPUT)
        {
            SetUIVisibility(true);
            SetUIPosition();
        }
        else if (p_UpdatedState == PlayState.SHOTRUNNING)
        {
            SetUIVisibility(false);
        }
    }

    void SetUIPosition()
    {
        playerUI.transform.position = playerBall.position;
    }

    void SetUIVisibility(bool p_VisibilityBool)
    {
        playerUI.enabled = p_VisibilityBool;
    }

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
