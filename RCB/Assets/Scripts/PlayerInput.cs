using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public MatchLogic matchLogic;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (matchLogic.CanReceiveInput())
        {
            CheckForInput();
        }
	}

    void CheckForInput()
    {

    }
}
