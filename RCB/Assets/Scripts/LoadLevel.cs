using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

    public void LoadScene(int p_LevelIndex, Text p_DatWarning)
    {
        p_DatWarning.text = "Lead Level Called " + p_LevelIndex;
        Debug.Log("Lead Level Called");
        SceneManager.LoadScene(p_LevelIndex, LoadSceneMode.Single);
    }

    public void LoadScene(int p_LevelIndex)
    {
        Debug.Log("Lead Level Called");
        SceneManager.LoadScene(p_LevelIndex, LoadSceneMode.Single);
    }
}

