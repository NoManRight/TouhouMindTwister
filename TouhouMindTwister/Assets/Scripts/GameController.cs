using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public Transform PlayerCamera;
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
            PlayerPrefs.SetInt("key_up", 119); // w
        }
        if (!PlayerPrefs.HasKey("key_down"))
        {
            PlayerPrefs.SetInt("key_down", 115); // s
        }
        if (!PlayerPrefs.HasKey("key_left"))
        {
            PlayerPrefs.SetInt("key_left", 97); // a
        }
        if (!PlayerPrefs.HasKey("key_right"))
        {
            PlayerPrefs.SetInt("key_right", 100); // d
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
}
