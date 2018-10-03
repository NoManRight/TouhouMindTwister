using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public MiniGameDictory miniGameDictory;
    public string Levels;
    public Transform PlayerCamera;
    public int PlayerCharacter;
    int Gamemode;
    int MiniGameChoosen;
	// Use this for initialization
	void Start () {
	    if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        // init values
        Gamemode = 0;

        // Check and init values for playerpref
        if (!PlayerPrefs.HasKey("screensize"))
        {
            PlayerPrefs.SetInt("screensize", 0);
        }
        if (!PlayerPrefs.HasKey("quality"))
        {
            PlayerPrefs.SetInt("quality", 0);
        }
        if (!PlayerPrefs.HasKey("key_up"))
        {
            PlayerPrefs.SetInt("key_up", (int)KeyCode.W); // w
        }
        if (!PlayerPrefs.HasKey("key_down"))
        {
            PlayerPrefs.SetInt("key_down", (int)KeyCode.S); // s
        }
        if (!PlayerPrefs.HasKey("key_left"))
        {
            PlayerPrefs.SetInt("key_left", (int)KeyCode.A); // a
        }
        if (!PlayerPrefs.HasKey("key_right"))
        {
            PlayerPrefs.SetInt("key_right", (int)KeyCode.D); // d
        }
        if (!PlayerPrefs.HasKey("key_skill"))
        {
            PlayerPrefs.SetInt("key_skill", (int)KeyCode.Alpha1);
        }
        if (!PlayerPrefs.HasKey("mute"))
        {
            PlayerPrefs.SetInt("mute", 0);
        }
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetInt("volume", 100);
        }

        //loads the value into the game
        LoadStartingData();

        ChosenCharacter();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadStartingData()
    {
        switch (PlayerPrefs.GetInt("screensize"))
        {
            case 0: //1024x768
                if (Screen.width != 1024 || Screen.height != 768)
                {
                    Screen.SetResolution(1024, 768, false);
                    Debug.Log("setting to 1024x768");
                }
                break;
            case 1: //1280x800
                if (Screen.width != 1280 || Screen.height != 800)
                {
                    Screen.SetResolution(1280, 800, false);
                    Debug.Log("setting to 1280x800");
                }
                break;
            case 2: //1280x960
                if (Screen.width != 1280 || Screen.height != 960)
                {
                    Screen.SetResolution(1280, 960, false);
                    Debug.Log("setting to 1280x960");
                }
                break;
            default:
                Debug.LogWarning("why am i here?");
                break;
        }

        // Quality Load here
        
        if (PlayerPrefs.GetInt("mute") == 1)
        {
            PlayerCamera.GetComponent<AudioSource>().mute = true;
        }

        PlayerCamera.GetComponent<AudioSource>().volume = PlayerPrefs.GetInt("volume") / 100;
    }

    public void SetGameMode(int mode)
    {
        Gamemode = mode;
    }

    public int GetGameMode()
    {
        return Gamemode;
    }

    public void SetMiniGame(int mode)
    {
        MiniGameChoosen = mode;
    }

    public int GetMiniGame()
    {
        return MiniGameChoosen;
    }

    public void ChosenCharacter()
    {
        // takes in what player chose
        // and saves into player character
        PlayerCharacter = 2; //default for now
    }
}
