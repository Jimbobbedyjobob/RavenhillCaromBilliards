using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenuFunctionality : MonoBehaviour
{

    [Header("Text Containers")]
    public Text noDataWarning;
    public GameObject statsPanel;

    [Header("Stat Display Texts")]
    public Text shotsData;
    public Text pointsData;
    public Text timeData;

    private LoadLevel loader;

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            OnPressReturnToMain();
        }
        if (Input.GetKeyDown("2"))
        {
            OnPressNewGame();
        }
    }

    public void OnPressNewGame()
    {
        loader.LoadScene(1);
    }

    public void OnPressReturnToMain()
    {
        loader.LoadScene(0);
    }

    void PushCurrentStatsToLast()
    {
        GameStatCarrier.CurrentStatsToLastSessionStats();
    }

    private void Start()
    {
        PushCurrentStatsToLast();

        DataIO.SaveGameStats(GameStatCarrier.GetStats().lastSession);

        if (loader == null)
        {
            loader = new LoadLevel();
        }

        DisplayLastMatchStats();
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

}
