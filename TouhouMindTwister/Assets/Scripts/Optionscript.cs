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
        S1280x800,
        S1280x960
    }
    enum Quality
    {
        Low = 0,
        Mid,
        High,
    }
    enum KeyboardControls
    {
        key_up = 0,
        key_down,
        key_left,
        key_right,
        //more to be added
        key_total,
    }

    ScreenSize CurrentScreenSize;
    Quality CurrentQuality;
    int[] KeyboardLayout;
	// Use this for initialization
	void Start () {
		
        CurrentScreenSize = (ScreenSize)PlayerPrefs.GetInt("screensize");
        CurrentQuality = (Quality)PlayerPrefs.GetInt("quality");
        ScreenSizeSelector.GetComponent<Dropdown>().value = (int)CurrentScreenSize;
        QualitySelector.GetComponent<Dropdown>().value = (int)CurrentQuality;
        KeyboardLayout = new int[(int)KeyboardControls.key_total]
            {
                PlayerPrefs.GetInt("key_up"),
                PlayerPrefs.GetInt("key_down"),
                PlayerPrefs.GetInt("key_left"),
                PlayerPrefs.GetInt("key_right")
            };
        //for (int i = 0; i < KeyboardLayout.Length; ++i)
        //{

        //}

        Resolution[] resolution = Screen.resolutions;
        foreach(Resolution res in resolution)
        {
            Debug.Log(res.width + "x" + res.height);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.anyKeyDown && Event.current.type == EventType.KeyDown)
        {
            Debug.Log(Event.current.keyCode);
            KeyCode key = Event.current.keyCode;
        }
	}

    public void OnValueChangedScreen()
    {
        CurrentScreenSize = (ScreenSize)ScreenSizeSelector.GetComponent<Dropdown>().value;
        // change screen size code here
        switch ((int)CurrentScreenSize)
        {
            case 0: //1024x768
                if (UnityEngine.Screen.width != 1024 || UnityEngine.Screen.height != 768)
                {
                    UnityEngine.Screen.SetResolution(1024, 768, false);
                    Debug.Log("setting to 1024x768");
                }
                Debug.Log("0");
                break;
            case 1: //1280x800
                if (UnityEngine.Screen.width != 1280 || UnityEngine.Screen.height != 800)
                {
                    UnityEngine.Screen.SetResolution(1280, 800, false);
                    Debug.Log("setting to 1280x800");
                }
                Debug.Log("1");
                break;
            case 2: //1280x960
                if (UnityEngine.Screen.width != 1280 || UnityEngine.Screen.height != 960)
                {
                    UnityEngine.Screen.SetResolution(1280, 960, false);
                    Debug.Log("setting to 1280x960");
                }
                Debug.Log("2");
                break;
            default:
                Debug.LogWarning("why am i here?");
                break;
        }
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
