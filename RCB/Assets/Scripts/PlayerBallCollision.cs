using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallCollision : MonoBehaviour {

    public StatsCanvasFunctionality statCanvas;

    // Utility Classes
    GameStatCarrier statCarrier;

    void Start ()
    {
        GameObject singleton = GameObject.Find("Singleton");

        if (singleton != null)
        {
            statCarrier = singleton.GetComponent<GameStatCarrier>();
        }
        else Debug.LogError("PlayerBall Cannot Find Singleton Scripts!");
    }

    void Update ()
    {
        TestInput();
    }

    void UpdateShotCount()
    {
        statCarrier.UpdateCurrentShots();
        statCanvas.UpdateShotsUI();
    }

    void UpdatePointCount()
    {
        statCarrier.UpdateCurrentPoints();
        statCanvas.UpdatePointsUI();
    }

    #region TEST FUNCTIONS ONLY
    void TestInput()
    {
        if (Input.GetKeyDown("s"))
        {
            Debug.Log("S Pressed");
            UpdateShotCount();
        }

        if (Input.GetKeyDown("p"))
        {
            Debug.Log("P Pressed");
            UpdatePointCount();
        }
    }

    #endregion
}
