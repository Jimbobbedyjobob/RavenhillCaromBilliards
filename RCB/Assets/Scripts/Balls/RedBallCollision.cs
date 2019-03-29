using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallCollision : MonoBehaviour
{
    [Header("AudioClip Array")]
    public AudioSource audioSource;
    public AudioClip[] soundFX = new AudioClip[2];

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball" && collision.gameObject.name == "Ball_Yellow")
        {
            audioSource.clip = soundFX[0];
            audioSource.Play();
        }
        else if (collision.gameObject.tag == "Pad")
        {
            audioSource.clip = soundFX[1];
            audioSource.Play();
            UtilityFunctions.RichocetRigidbody(rb, collision);
        }
    }
}
