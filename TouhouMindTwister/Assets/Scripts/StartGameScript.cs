using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartGameScript : MonoBehaviour {
    public RectTransform Gamemode;
    public RectTransform Option;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // main menu
    public void PressedGamemode()
    {
        Gamemode.gameObject.SetActive(true);
    }

    public void ExitGamemode()
    {
        Gamemode.gameObject.SetActive(false);
    }

    public void PressedOption()
    {
        Option.gameObject.SetActive(true);
    }

    public void ExitOption()
    {
        Option.gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    // game mode
    public void PressedContinous()
    {
        GameController.instance.SetGameMode(1);
        int rannum = Random.Range(0, GameController.instance.Levels.Length);
        SceneManager.LoadScene(rannum + 1);
        //random minigame here
    }

    public void PressedChallenge()
    {
        GameController.instance.SetGameMode(2);
        //level select here
    }
}
