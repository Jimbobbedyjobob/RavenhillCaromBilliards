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
            CalculateNewDirection(collision);
        }
    }

    void UpdateBallContactsArray(Collision p_Collision)
    {
        if (hits[0] == null && hits[1] == null)
        {
            hits[0] = p_Collision.gameObject;
        }
        else if (hits[0].name == p_Collision.gameObject.name && hits[1] == null)
        {
            // Do Nothing
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

    // Wasn't getting a nice Ricochet with the basic physics from the Cushions, 
    // so I added this...
    void CalculateNewDirection(Collision p_Collision)
    {
        Vector3 ricochetDirection = new Vector3();

        if (p_Collision.gameObject.name == "North")
        { ricochetDirection = Vector3.Reflect(rb.velocity, -Vector3.forward); }
        else if (p_Collision.gameObject.name == "South")
        { ricochetDirection = Vector3.Reflect(rb.velocity, Vector3.forward); }
        else if (p_Collision.gameObject.name == "East")
        { ricochetDirection = Vector3.Reflect(rb.velocity, -Vector3.right); }
        else if (p_Collision.gameObject.name == "West")
        { ricochetDirection = Vector3.Reflect(rb.velocity, Vector3.right); }

        float magnitude = rb.velocity.magnitude;
        rb.velocity = ricochetDirection;
        rb.velocity *= magnitude;
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
