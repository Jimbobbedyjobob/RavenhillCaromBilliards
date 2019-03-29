using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEscapeDetection : MonoBehaviour
{
    [Header("Scripts from other scene objects")]
    public ReplayFunctionality replayer;

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("A Ball has escaped bounds!!");
        EventHub.BallOutofBoundsEvent.Invoke();
    }
}
