using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuFunctionality : MonoBehaviour {

    LoadLevel loader;

    // Text Containers
    public Text noDataWarning;
    public GameObject statsPanel;

    // UI Objects
    public Text shotsText;
    public Text pointsText;
    public Text timeText;

    private void Awake()
    {
        loader = new LoadLevel();

        AssignStatValuesToDisplay();
    }

    void AssignStatValuesToDisplay()
    {
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

    public void OnPressNewGame()
    {
        noDataWarning.text += "New Game Press Registered @ End Menu";

        PushCurentStatsToLast();
        loader.LoadScene(1);
    }

    public void OnPressReturnToMain()
    {
        noDataWarning.text += "Return Press Registered @ End Menu";

        PushCurentStatsToLast();
        loader.LoadScene(0);
    }

    void PushCurentStatsToLast()
    {
        GameStatCarrier.CurrentStatsToLastSessionStats();
        DataIO.SaveGameStats();
    }
}
