using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallCollision : MonoBehaviour
{
    [Header("Scripts from other scene objects")]
    public StatsCanvasFunctionality statCanvas;

    [Header("AudioClip Array")]
    public AudioSource audioSource;
    public AudioClip[] soundFX = new AudioClip[2];

    private Rigidbody rb;
    private GameObject[] hits = new GameObject[2];

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        EventHub.PlayStateUpdate.AddListener(PlayStateUpdateListener);
    }

    #region Ball Collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            audioSource.clip = soundFX[0];
            audioSource.Play();
            if(!GameStatCarrier.pointRegisteredThisShot)
            {
                UpdateBallContactsArray(collision);
            }
        }
        else if (collision.gameObject.tag == "Pad")
        {
            audioSource.clip = soundFX[1];
            audioSource.Play();
            UtilityFunctions.RichocetRigidbody(rb, collision);
        }
    }

    void UpdateBallContactsArray(Collision p_Collision)
    {
        if (hits[0] == null && hits[1] == null)
        {
            hits[0] = p_Collision.gameObject;
        }
        else if (hits[0].name != p_Collision.gameObject.name && hits[1] == null)
        {
            hits[1] = p_Collision.gameObject;
        }

        if (hits[0] != null && hits[1] != null)
        {
            AssessHaveHitAllBalls();
        }
    }

    void AssessHaveHitAllBalls()
    {
        if (hits[0].name != hits[1].name && !GameStatCarrier.pointRegisteredThisShot)
        {
            UpdatePointCount();
            hits = new GameObject[2];
        }
    }
    #endregion

    #region Game Statistics Updates
    void UpdateShotCount()
    {
        GameStatCarrier.UpdateCurrentShots();
        statCanvas.UpdateShotsUI();
    }

    void UpdatePointCount()
    {
        GameStatCarrier.UpdateCurrentPoints();
        statCanvas.UpdatePointsUI();
    }
    #endregion

    // Listener
    private void PlayStateUpdateListener(PlayState p_UpdatedState)
    {
        if(p_UpdatedState == PlayState.CAMERAAIMING)
        {
            hits = new GameObject[2];
        }
    }
}
