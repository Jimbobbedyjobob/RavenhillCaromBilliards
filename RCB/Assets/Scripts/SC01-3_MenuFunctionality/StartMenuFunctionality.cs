using UnityEngine;
using UnityEngine.UI;

public class StartMenuFunctionality : MonoBehaviour {

    [Header("Slider")]
    public Slider volumeSlider;
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
        AudioListener.volume = GameStatCarrier.volume;
        volumeSlider.value = GameStatCarrier.volume;

        if (GameStatCarrier.isContinuedSession)
        {
            DisplayLastMatchStats();
        }
        else ReadExternalLastSessionStats();
    }

    void Start()
    {
        if (loader == null)
        {
            loader = new LoadLevel();
        }
    }

    public void OnPressStart()
    {
        GameStatCarrier.isContinuedSession = true;
        loader.LoadScene(1);
    }

    void DisplayLastMatchStats()
    {
        noDataWarning.enabled = false;
        statsPanel.SetActive(true);
        shotsData.text = GameStatCarrier.GetStats().lastSession.shots.ToString();
        pointsData.text = GameStatCarrier.GetStats().lastSession.points.ToString();
        float tempRawTime = GameStatCarrier.GetStats().lastSession.time;
        timeData.text = UtilityFunctions.PresentTimeValueInMinutesAndSeconds(tempRawTime);
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
	
    public void VolumeSliderChange()
    {
        GameStatCarrier.volume = volumeSlider.value;
        AudioListener.volume = GameStatCarrier.volume;
    }
}
