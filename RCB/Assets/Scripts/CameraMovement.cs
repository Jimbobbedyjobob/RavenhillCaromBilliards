using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform playerBall;
    
	void Start ()
    {
        if (playerBall == null)
        {
            Debug.LogError("Camera cannot find Ball!!");
        }
	}
	
	void Update ()
    {
        transform.LookAt(playerBall, Vector3.up);
	}
}
