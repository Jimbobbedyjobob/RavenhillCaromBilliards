using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuFunctionality : MonoBehaviour {

    // Text Containers
    public Text noDataWarning;
    public GameObject statsPanel;

    // UI Objects
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
        else Debug.LogError("EndMenu Cannot Find Singleton Scripts!");

        AssignStatValuesToDisplay();
    }

    void AssignStatValuesToDisplay()
    {
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

    public void OnPressNewGame()
    {
        noDataWarning.text += "New Game Press Registered @ End Menu";

        PushCurentStatsToLast();
        loader.LoadScene(2, noDataWarning);
    }

    public void OnPressReturnToMain()
    {
        noDataWarning.text += "Return Press Registered @ End Menu";

        PushCurentStatsToLast();
        loader.LoadScene(1, noDataWarning);
    }

    void PushCurentStatsToLast()
    {
        statCarrier.CurrentStatsToLastSessionStats();
        readWrite.SaveGameStats();
    }
}
