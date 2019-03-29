using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatersInput : MonoBehaviour
{
    public StatsCanvasFunctionality statCanvas;

    // Frankly, this game is too hard to test patiently . . .
	void Update ()
    {
		if(Input.GetKeyDown("s"))
        {
            GameStatCarrier.UpdateCurrentShots();
            statCanvas.UpdateShotsUI();
        }
        if (Input.GetKeyDown("p"))
        {
            GameStatCarrier.UpdateCurrentPoints();
            statCanvas.UpdatePointsUI();
        }
    }
}

