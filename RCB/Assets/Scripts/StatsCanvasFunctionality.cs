using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsCanvasFunctionality : MonoBehaviour {

    // UI Objects
    public Text shotsText;
    public Text pointsText;
    public Text timeText;

    // Stat Variables
    float time = 0.0f;
	
	// Update is called once per frame
	void Update ()
    {    
        if(Input.GetKeyDown("w"))
        {
            DataIO.SaveGameStats();
        }
    }

    public void UpdateTime()
    {
        time = GameStatCarrier.GetStats().currentSession.time;
        timeText.text =  DisplayUtility.PresentTimeValueInMInutesAndSeconds(time);
    }

    public void UpdateShotsUI()
    {
        shotsText.text = "";
        shotsText.text += GameStatCarrier.GetStats().currentSession.shots;
    }

    public void UpdatePointsUI()
    {
        pointsText.text = "";
        pointsText.text += GameStatCarrier.GetStats().currentSession.points;
    }
}
