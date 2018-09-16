using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScript : MonoBehaviour {
    public RectTransform Gamemode;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PressedGamemode()
    {
        Gamemode.gameObject.SetActive(true);
    }

    public void ExitGamemode()
    {
        Gamemode.gameObject.SetActive(false);
    }
}
