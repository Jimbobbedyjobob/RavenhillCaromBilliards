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

    // Utility Classes
    LoadLevel loader;
    GameStatCarrier statCarrier;
    DisplayUtility display;
    DataIO readWrite;

    private void Awake()
    {
        GameObject singleton = GameObject.Find("Singleton");

        if (singleton != null)
        {
            loader = singleton.GetComponent<LoadLevel>();
            statCarrier = singleton.GetComponent<GameStatCarrier>();
            display = singleton.GetComponent<DisplayUtility>();
            readWrite = singleton.GetComponent<DataIO>();
        }
        else Debug.LogError("StartMenu Cannot Find Singleton Scripts!");
    }

    private void Start()
    {
        AudioListener.volume = currentVolume;
        volumeSlider.value = currentVolume;

        if (statCarrier.isContinuedSession)
        {
            display.DisplayLastMatchStats(statsPanel,
                                                    noDataWarning,
                                                    shotsText,
                                                    pointsText,
                                                    timeText);
        }
        else ReadExternalLastSessionStats();

        volumeSlider.onValueChanged.AddListener(delegate { VolumeSliderChangeCheck(); });
    }

    public void VolumeSliderChangeCheck()
    {
        Debug.Log(volumeSlider.value);
    }

    public void OnPressStart()
    {
        statCarrier.isContinuedSession = true;
        loader.LoadScene(2, noDataWarning);
    }

    void ReadExternalLastSessionStats()
    {
        readWrite.LoadLastSessionStats();

        if (readWrite.lastSessionStatsExist)
        {
            display.DisplayLastMatchStats(statsPanel,
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
