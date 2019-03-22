using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallCollision : MonoBehaviour {

    public StatsCanvasFunctionality statCanvas;

	void Start ()
    {

    }

    void Update ()
    {
        TestInput();
    }

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
