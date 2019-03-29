﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBallCollision : MonoBehaviour
{
    [Header("AudioClip Array")]
    public AudioSource audioSource;
    public AudioClip padFX;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pad")
        {
            audioSource.clip = padFX;
            audioSource.Play();
        }
    }
}
