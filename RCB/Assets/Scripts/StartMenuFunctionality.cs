using System.Collections;
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
    public Text shotsText;
    public Text pointsText;
    public Text timeText;

    LoadLevel loader;

    private void Awake()
    {
        AudioListener.volume = currentVolume;
        volumeSlider.value = currentVolume;

        if (GameStatCarrier.isContinuedSession)
        {
            DisplayUtility.DisplayLastMatchStats(   statsPanel, 
                                                    noDataWarning,
                                                    shotsText,
                                                    pointsText,
                                                    timeText);
        }
        else ReadExternalLastSessionStats();

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

    void ReadExternalLastSessionStats()
    {
        DataIO.LoadLastSessionStats();

        if (DataIO.lastSessionStatsExist)
        {
            DisplayUtility.DisplayLastMatchStats(statsPanel,
                                        noDataWarning,
                                        shotsText,
                                        pointsText,
                                        timeText);
        }
        else
        {
            noDataWarning.enabled = true;
            statsPanel.SetActive(false);
        }
    }

}
