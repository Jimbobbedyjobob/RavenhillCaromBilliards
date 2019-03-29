using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsCanvasFunctionality : MonoBehaviour {

    [Header("Stat Display Texts")]
    public Text shotsText;
    public Text pointsText;
    public Text timeText;

    // Stat Variables
    private float time = 0.0f;

    public void UpdateTime()
    {
        time = GameStatCarrier.GetStats().currentSession.time;
        timeText.text =  UtilityFunctions.PresentTimeValueInMinutesAndSeconds(time);
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
