using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuFunctionality : MonoBehaviour {

    [Header("Slider")]
    public Slider volumeSlider;
    float currentVolume = 0.5f;
    [Header("Text Containers")]
    public Text noDataWarning;
    public GameObject statsPanel;
    [Header("Stat Display Texts")]
    public Text shotsData;
    public Text pointsData;
    public Text timeData;

    private LoadLevel loader;

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
        timeData.text = UtilityFunctions.PresentTimeValueInMInutesAndSeconds(tempRawTime);
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
	
    public void VolumeSliderChangeCheck()
    {
        Debug.Log(volumeSlider.value);
    }

    public void OnPressStart()
    {
        GameStatCarrier.isContinuedSession = true;
        loader.LoadScene(1);
    }

    void Start ()
    {
        volumeSlider.onValueChanged.AddListener(delegate { VolumeSliderChangeCheck(); });

        if (loader == null)
        {
            loader = new LoadLevel();
        }
    }
}
