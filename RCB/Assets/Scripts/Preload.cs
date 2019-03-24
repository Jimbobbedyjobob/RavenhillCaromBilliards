using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preload : MonoBehaviour {

    public LoadLevel loader;

	void Start ()
    {
        loader.LoadScene(1);
    }
}
