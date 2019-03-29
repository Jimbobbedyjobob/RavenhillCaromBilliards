using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RicochetVectors
{
    public Vector3 ricochetDirection;
    public Vector3 ricochetVelocity;
}

public class UtilityFunctions {

    public static string PresentTimeValueInMinutesAndSeconds(float p_RawTimeValue)
    {
        float totalSeconds = p_RawTimeValue;
        // remove any elapsed hours
        totalSeconds %= 3600.0f;
        int minutes = Mathf.FloorToInt(totalSeconds / 60.0f);
        // Remove any elapsed minutes
        totalSeconds %= 60.0f;
        int seconds = Mathf.FloorToInt(totalSeconds / 1.0f);

        // Create the String
        string newTimeText = minutes.ToString("00") + ":" + seconds.ToString("00");

        return newTimeText;
    }

    public static bool RotSlerpWorld(Transform p_SlerpObject, Quaternion p_FromRotation, Quaternion p_ToRotation, float p_TimeCount)
    {
        if (p_SlerpObject.rotation != p_ToRotation)
        {
            p_SlerpObject.rotation = Quaternion.Slerp(p_FromRotation, p_ToRotation, p_TimeCount);
            p_TimeCount = p_TimeCount + Time.deltaTime;
            return false;
        }
        else return true;
    }


    public static bool PosSlerpWorld(Transform p_SlerpObject, Vector3 p_FromPosition, Vector3 p_ToPosition, float p_JourneyTime, float p_StartTime)
    {
        if (p_SlerpObject.position != p_ToPosition)
        {
            Vector3 midpoint = new Vector3(0f, 0.04f, 0f);

            Vector3 startRelativeToCenter = p_FromPosition - midpoint;
            Vector3 endRelativeToCenter = p_ToPosition - midpoint;

            float fractionComplete = (Time.time - p_StartTime) / p_JourneyTime;

            p_SlerpObject.position = Vector3.Slerp(startRelativeToCenter, endRelativeToCenter, fractionComplete);
            p_SlerpObject.position += midpoint;

            return false;
        }
        else return true;
    }

    public static bool CheckForAllBallsStopped(Rigidbody p_PlayerBall, Rigidbody p_RedBall, Rigidbody p_YellowBall)
    {
        float playerBallvelocity = p_PlayerBall.velocity.magnitude;
        float redBallvelocity = p_RedBall.velocity.magnitude;
        float yellowBallvelocity = p_YellowBall.velocity.magnitude;

        if (playerBallvelocity <= 0.02f &&
            redBallvelocity <= 0.02f &&
            yellowBallvelocity <= 0.02f)
        {
            p_PlayerBall.velocity = Vector3.zero;
            p_RedBall.velocity = Vector3.zero;
            p_YellowBall.velocity = Vector3.zero;
            return true;
        }
        else return false;
    }

    // Wasn't getting a lot of Ricochet with the basic physics from the Cushions, 
    // so I added this...
    public static void RichocetRigidbody(Rigidbody p_RB,  Collision p_Collision)
    {
        Vector3 ricochetDirection = new Vector3();

        if (p_Collision.gameObject.name == "North")
        { ricochetDirection = Vector3.Reflect(p_RB.velocity, -Vector3.forward); }
        else if (p_Collision.gameObject.name == "South")
        { ricochetDirection = Vector3.Reflect(p_RB.velocity, Vector3.forward); }
        else if (p_Collision.gameObject.name == "East")
        { ricochetDirection = Vector3.Reflect(p_RB.velocity, -Vector3.right); }
        else if (p_Collision.gameObject.name == "West")
        { ricochetDirection = Vector3.Reflect(p_RB.velocity, Vector3.right); }

        float magnitude = p_RB.velocity.magnitude;
        p_RB.velocity = ricochetDirection;
        p_RB.velocity *= magnitude;
    }
}
