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

    // Utility Classes
    GameStatCarrier statCarrier;
    DisplayUtility display;
    DataIO readWrite;

    void Update ()
    {    
        if(Input.GetKeyDown("w"))
        {
            readWrite.SaveGameStats();
        }
    }

    public void UpdateTime()
    {
        time = statCarrier.GetStats().currentSession.time;
        timeText.text = display.PresentTimeValueInMinutesAndSeconds(time);
    }

    public void UpdateShotsUI()
    {
        shotsText.text = "";
        shotsText.text += statCarrier.GetStats().currentSession.shots;
    }

    public void UpdatePointsUI()
    {
        pointsText.text = "";
        pointsText.text += statCarrier.GetStats().currentSession.points;
    }

    private void Awake()
    {
        GameObject singleton = GameObject.Find("Singleton");

        if (singleton != null)
        {
            statCarrier = singleton.GetComponent<GameStatCarrier>();
            display = singleton.GetComponent<DisplayUtility>();
            readWrite = singleton.GetComponent<DataIO>();
        }
        else Debug.LogError("StatsCanvas Cannot Find Singleton Scripts!");
    }
}
