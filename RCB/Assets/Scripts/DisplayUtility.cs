using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUtility {

    public static string PresentTimeValueInMInutesAndSeconds(float p_RawTimeValue)
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
}
