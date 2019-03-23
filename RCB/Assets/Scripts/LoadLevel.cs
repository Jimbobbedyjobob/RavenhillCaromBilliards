﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    public void LoadScene(int p_LevelIndex )
    {
        Debug.Log("Lead Level Called");
        SceneManager.LoadScene(p_LevelIndex, LoadSceneMode.Single);
    }
}
