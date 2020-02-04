using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using Menu;

public class GameStarter : MonoBehaviour
{

    public GameSettings Settings;
    public GameObject Player1;
    public GameObject Player2;
    public PauseController PauseCont;
    public PostProcessingProfile profile;

    // Use this for initialization
    void Start ()
    {
        Instantiate(Player1);
        if (Settings.CoOp)
        {
            Instantiate(Player2);
        }
        PauseCont.SubscribeToPlayer1();
        profile.vignette.enabled = false;
        Time.timeScale = 1;
    }

}
