using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallCollision : MonoBehaviour
{

    public StatsCanvasFunctionality statCanvas;
    public MatchLogic matchLogic;
    public AudioSource audioSource;
    public AudioClip[] soundFX = new AudioClip[2];

    private Rigidbody rb;
    private GameObject[] hits = new GameObject[2];

    void Start()
    {
        if (statCanvas == null || matchLogic == null)
        {
            Debug.LogError("PlayerBallCollision is missing script references!!");
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("PlayerBall RigidBody is missing!!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            audioSource.clip = soundFX[0];
            audioSource.Play();
            UpdateBallContactsArray(collision);
        }
        else if (collision.gameObject.tag == "Pad")
        {
            audioSource.clip = soundFX[1];
            audioSource.Play();
            CalculateNewDirection(collision);
        }
    }

    #region Ball Collision
    void UpdateBallContactsArray(Collision p_Collision)
    {
        Debug.Log("Looping through Hits Array");

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

        string zeroName;
        string oneName;
        if (hits[0] != null)
        {
            zeroName = hits[0].name;
        }
        else zeroName = "No 1st Hit GO";
        if (hits[1] != null)
        {
            oneName = hits[1].name;
        }
        else oneName = "No 2nd Hit GO";

        Debug.Log("Hit Aray Objs; " + zeroName + ": " + oneName);

        if (hits[0] != null && hits[1] != null)
        {
            AssessHaveHitAllBalls();
        }
    }

    void AssessHaveHitAllBalls()
    {
        Debug.Log("AssessHaveHitAllBalls Called!!");
        if (hits[0].name != hits[1].name && !GameStatCarrier.pointRegisteredThisShot)
        {
            UpdatePointCount();
        }
    }

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

    #region Stat Updates
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
}
