using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Optionscript : MonoBehaviour {
    public GameObject ScreenSizeSelector;
    public GameObject QualitySelector;
    public GameObject Warning;
    enum ScreenSize
    {
        S1024x768 = 0,
        S16x9,
        S16x10
    }
    enum Quality
    {
        Low = 0,
        Mid,
        High,
    }
    ScreenSize CurrentScreenSize;
    Quality CurrentQuality;
	// Use this for initialization
	void Start () {
		
        CurrentScreenSize = (ScreenSize)PlayerPrefs.GetInt("screensize");
        CurrentQuality = (Quality)PlayerPrefs.GetInt("quality");
        ScreenSizeSelector.GetComponent<Dropdown>().value = (int)CurrentScreenSize;
        QualitySelector.GetComponent<Dropdown>().value = (int)CurrentQuality;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnValueChangedScreen()
    {
        CurrentScreenSize = (ScreenSize)ScreenSizeSelector.GetComponent<Dropdown>().value;
        // change screen size code here
    }

    public void OnValueChangedQuality()
    {
        CurrentQuality = (Quality)QualitySelector.GetComponent<Dropdown>().value;
        // change quality code here
    }

    public void SaveandExit()
    {
        PlayerPrefs.SetInt("screensize", (int)CurrentScreenSize);
        PlayerPrefs.SetInt("quality", (int)CurrentQuality);
        PlayerPrefs.SetInt("key_up", 119); // w
        PlayerPrefs.SetInt("key_down", 115); // s
        PlayerPrefs.SetInt("key_left", 97); // a
        PlayerPrefs.SetInt("key_right", 100); // d
        Exit();
    }

    public void ExitWithoutSave()
    {
        Warning.SetActive(true);
    }

    public void Cancel()
    {
        Warning.SetActive(false);
    }

    public void Exit()
    {
        this.gameObject.SetActive(false);
    }
}
