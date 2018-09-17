using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
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
    }
	
	// Update is called once per frame
	void Update () {
		
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
