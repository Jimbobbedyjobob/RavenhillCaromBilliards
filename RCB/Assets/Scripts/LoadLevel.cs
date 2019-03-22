using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel {

    public void LoadScene(int p_LevelIndex)
    {
        SceneManager.LoadScene(p_LevelIndex, LoadSceneMode.Single);
    }
}
