using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Optionscript : MonoBehaviour {
    public GameObject ScreenSizeSelector;
    public GameObject QualitySelector;
    public GameObject KeyControl;
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
    bool checkforkeyinput;
    int whichkeytocheckfor;

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
        KeyControl.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = ((KeyCode)KeyboardLayout[0]).ToString();
        KeyControl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = ((KeyCode)KeyboardLayout[1]).ToString();
        KeyControl.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>().text = ((KeyCode)KeyboardLayout[2]).ToString();
        KeyControl.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>().text = ((KeyCode)KeyboardLayout[3]).ToString();

        Resolution[] resolution = Screen.resolutions;
        foreach(Resolution res in resolution)
        {
            Debug.Log(res.width + "x" + res.height);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnGUI()
    {
        if (checkforkeyinput)
        {
            if (Input.anyKeyDown && Event.current.type == EventType.KeyDown)
            {
                Debug.Log(Event.current.keyCode);
                KeyboardLayout[whichkeytocheckfor] = (int)Event.current.keyCode;
                KeyControl.transform.GetChild(1).GetChild(whichkeytocheckfor).GetChild(0).GetComponent<Text>().text = ((KeyCode)KeyboardLayout[whichkeytocheckfor]).ToString();
                checkforkeyinput = false;
            }
        }
        
    }

    public void OnButtonPress(int i)
    {
        whichkeytocheckfor = i;
        checkforkeyinput = true;
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
                break;
            case 1: //1280x800
                if (UnityEngine.Screen.width != 1280 || UnityEngine.Screen.height != 800)
                {
                    UnityEngine.Screen.SetResolution(1280, 800, false);
                    Debug.Log("setting to 1280x800");
                }
                break;
            case 2: //1280x960
                if (UnityEngine.Screen.width != 1280 || UnityEngine.Screen.height != 960)
                {
                    UnityEngine.Screen.SetResolution(1280, 960, false);
                    Debug.Log("setting to 1280x960");
                }
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
        PlayerPrefs.SetInt("key_up", KeyboardLayout[0]);
        PlayerPrefs.SetInt("key_down", KeyboardLayout[1]);
        PlayerPrefs.SetInt("key_left", KeyboardLayout[2]);
        PlayerPrefs.SetInt("key_right", KeyboardLayout[3]);
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
