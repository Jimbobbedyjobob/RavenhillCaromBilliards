using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUtility : MonoBehaviour {

    // Utility Classes
    GameStatCarrier statCarrier;
    DisplayUtility display;

    private void Start()
    {
        statCarrier = GetComponent<GameStatCarrier>();
        display = GetComponent<DisplayUtility>();
    }

    public string PresentTimeValueInMinutesAndSeconds(float p_RawTimeValue)
    {
        // Clean Up Minnutes and Seconds values
        int cleanedSeconds = (int)p_RawTimeValue;
        int timeInMinutes = (int)p_RawTimeValue / 60;
        cleanedSeconds -= (timeInMinutes * 60);

        // Create the String
        string newTimeText = "";
        if (timeInMinutes < 10)
        {
            newTimeText += "0" + timeInMinutes;
        }
        else
        {
            newTimeText += timeInMinutes;
        }

        newTimeText += ":";

        if (cleanedSeconds < 10)
        {
            newTimeText += "0" + cleanedSeconds;
        }
        else
        {
            newTimeText += cleanedSeconds;
        }

        return newTimeText;
    }

    public void DisplayLastMatchStats(   GameObject p_StatsPanel,
                                                Text p_NoDataWarning, 
                                                Text p_ShotsData,
                                                Text p_PointsData,
                                                Text p_TimeData)
    {
        p_NoDataWarning.enabled = false;
        p_StatsPanel.SetActive(true);
        p_ShotsData.text = statCarrier.GetStats().lastSession.shots.ToString();
        p_PointsData.text = statCarrier.GetStats().lastSession.points.ToString();
        float tempRawTime = statCarrier.GetStats().lastSession.time;
        p_TimeData.text = display.PresentTimeValueInMinutesAndSeconds(tempRawTime);
    }
}
