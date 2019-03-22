﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuFunctionality : MonoBehaviour {

    // Slider Variables
    public Slider volumeSlider;
    float currentVolume = 0.5f;

    // Text Containers
    public Text noDataWarning;
    public GameObject statsPanel;

    // Stat Display Objects
    public Text shotsData;
    public Text pointsData;
    public Text timeData;

    LoadLevel loader;

    private void Awake()
    {
        AudioListener.volume = currentVolume;
        volumeSlider.value = currentVolume;

        if (GameStatCarrier.isContinuedSession)
        {
            DisplayLastMatchStats();
        }
        else ReadExternalLastSessionStats();
    }

    void DisplayLastMatchStats()
    {
        noDataWarning.enabled = false;
        statsPanel.SetActive(true);
        shotsData.text = GameStatCarrier.GetStats().lastSession.shots.ToString();
        pointsData.text = GameStatCarrier.GetStats().lastSession.points.ToString();
        float tempRawTime = GameStatCarrier.GetStats().lastSession.time;
        timeData.text = DisplayUtility.PresentTimeValueInMInutesAndSeconds(tempRawTime);
    }

    void ReadExternalLastSessionStats()
    {
        DataIO.LoadLastSessionStats();

        if (DataIO.lastSessionStatsExist)
        {
            DisplayLastMatchStats();
        }
        else
        {
            noDataWarning.enabled = true;
            statsPanel.SetActive(false);
        }
    }

    void Start ()
    {
        loader = new LoadLevel();
		volumeSlider.onValueChanged.AddListener(delegate { VolumeSliderChangeCheck(); });
    }
	
    public void VolumeSliderChangeCheck()
    {
        Debug.Log(volumeSlider.value);
    }

    public void OnPressStart()
    {
        GameStatCarrier.isContinuedSession = true;
        loader.LoadScene(1);
    }
}
