using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointer : MonoBehaviour
{
    [Header("Scripts from other scene objects")]
    public RectTransform pointerTransform;

    [Header("Objects from elsewhere in scene")]
    public Transform playerBall;

    [Header("Vars for Testing")]
    public float pointerScaleMultiplier;

    private void Start ()
    {
        EventHub.PlayStateUpdate.AddListener(PlayStateUpdateListener);
    }

    private void SetUIVisibility(bool p_VisibilityBool)
    {
        pointerTransform.gameObject.SetActive(p_VisibilityBool);
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

    // Listener
    private void PlayStateUpdateListener(PlayState p_UpdatedState)
    {
        if (p_UpdatedState == PlayState.PLAYERINPUT)
        {
            SetUIVisibility(true);
        }
        else if (p_UpdatedState == PlayState.BALLRELEASED || p_UpdatedState == PlayState.REPLAYING)
        {
            SetUIVisibility(false);
        }
    }

}
