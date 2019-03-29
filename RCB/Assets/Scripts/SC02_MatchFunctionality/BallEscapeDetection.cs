using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEscapeDetection : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("A Ball has escaped bounds!!");
        EventHub.BallOutofBoundsEvent.Invoke();
    }
}
