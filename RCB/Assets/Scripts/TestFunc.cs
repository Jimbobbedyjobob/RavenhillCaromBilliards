using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestFunc : MonoBehaviour {

    LoadLevel loader;

    private void Start()
    {
        loader = new LoadLevel();
    }

    void Update () {
        if(Input.GetKeyDown("1"))
        {
            loader.LoadScene(0);
        }
        if (Input.GetKeyDown("2"))
        {
            loader.LoadScene(1);
        }
        if (Input.GetKeyDown("3"))
        {
            loader.LoadScene(2);
        }
    }
}
