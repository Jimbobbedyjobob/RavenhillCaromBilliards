using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityFunctions {

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

    public static bool QuaternionSlerpLocal(Transform p_SlerpObject, Quaternion p_FromRotation, Quaternion p_ToRotation, float p_TimeCount)
    {
        Debug.Log("Local Rotation Slerp on " + p_SlerpObject.name + " RUNNING!!");

        if (p_SlerpObject.localRotation != p_ToRotation)
        {
            p_SlerpObject.localRotation = Quaternion.Slerp(p_FromRotation, p_ToRotation, p_TimeCount);
            p_TimeCount = p_TimeCount + Time.deltaTime;
            return false;
        }
        else return true;

    }

    public static bool QuaternionSlerpWorld(Transform p_SlerpObject, Quaternion p_FromRotation, Quaternion p_ToRotation, float p_TimeCount)
    {
        Debug.Log("World Rotation Slerp on " + p_SlerpObject.name + " RUNNING!!");

        if (p_SlerpObject.rotation != p_ToRotation)
        {
            p_SlerpObject.rotation = Quaternion.Slerp(p_FromRotation, p_ToRotation, p_TimeCount);
            p_TimeCount = p_TimeCount + Time.deltaTime;
            return false;
        }
        else return true;
    }


    public static bool Vector3SlerpWorld(Transform p_SlerpObject, Vector3 p_FromPosition, Vector3 p_ToPosition, float p_JourneyTime, float p_StartTime)
    {
        Debug.Log("POsition Slerp on " + p_SlerpObject.name + " RUNNING!!");

        if (p_SlerpObject.position != p_ToPosition)
        {

            Vector3 midpoint = (p_FromPosition + p_ToPosition) * 0.5f;

            Vector3 startRelativeToCenter = p_FromPosition - midpoint;
            Vector3 endRelativeToCenter = p_ToPosition - midpoint;

            float fractionComplete = (Time.deltaTime - p_StartTime) / p_JourneyTime;

            p_SlerpObject.position = Vector3.Slerp(startRelativeToCenter, endRelativeToCenter, fractionComplete);

            return false;
        }
        else return true;
    }
}
