using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuFunctionality : MonoBehaviour {

    LoadLevel loader = new LoadLevel();

    public void OnPressNewGame()
    {
        loader.LoadScene(1);
    }

    public void OnPressReturnToMain()
    {
        GameStatCarrier.CurrentStatsToLastSessionStats();
        DataIO.SaveGameStats(GameStatCarrier.GetStats().lastSession);
        loader.LoadScene(0);
    }
}
