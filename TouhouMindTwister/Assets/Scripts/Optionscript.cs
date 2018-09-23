using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Optionscript : MonoBehaviour {
    public GameObject ScreenSizeSelector;
    public GameObject QualitySelector;
    public GameObject KeyControl;
    public GameObject MuteControl;
    public GameObject VolumeSelector;
    public GameObject PlayerCamera;
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
    bool CurrentInputMute;
    float CurrentVolume;
    bool checkforkeyinput;
    int whichkeytocheckfor;

	// Use this for initialization
	void Start () {
		
        CurrentScreenSize = (ScreenSize)PlayerPrefs.GetInt("screensize");
        ScreenSizeSelector.GetComponent<Dropdown>().value = (int)CurrentScreenSize;

        CurrentQuality = (Quality)PlayerPrefs.GetInt("quality");
        QualitySelector.GetComponent<Dropdown>().value = (int)CurrentQuality;
        KeyboardLayout = new int[(int)KeyboardControls.key_total]
            {
                PlayerPrefs.GetInt("key_up"),
                PlayerPrefs.GetInt("key_down"),
                PlayerPrefs.GetInt("key_left"),
                PlayerPrefs.GetInt("key_right")
            };
        KeyControl.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = ((KeyCode)KeyboardLayout[0]).ToString();
        KeyControl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = ((KeyCode)KeyboardLayout[1]).ToString();
        KeyControl.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>().text = ((KeyCode)KeyboardLayout[2]).ToString();
        KeyControl.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>().text = ((KeyCode)KeyboardLayout[3]).ToString();

        if (PlayerPrefs.GetInt("mute") == 0)
        {
            CurrentInputMute = false;
        }
        else
        {
            CurrentInputMute = true;
        }
        MuteControl.GetComponent<Toggle>().isOn = CurrentInputMute;
        PlayerCamera.GetComponent<AudioSource>().mute = CurrentInputMute;

        CurrentVolume = PlayerPrefs.GetInt("volume");
        VolumeSelector.GetComponent<Slider>().value = CurrentVolume;
        PlayerCamera.GetComponent<AudioSource>().volume = CurrentVolume;
        VolumeSelector.transform.GetChild(4).GetComponent<Text>().text = CurrentVolume.ToString();

        //Resolution[] resolution = Screen.resolutions;
        //foreach(Resolution res in resolution)
        //{
        //    Debug.Log(res.width + "x" + res.height);
        //}
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
    }

    public void OnValueChangedQuality()
    {
        CurrentQuality = (Quality)QualitySelector.GetComponent<Dropdown>().value;
        // change quality code here
    }

    public void OnValueChangedMute()
    {
        CurrentInputMute = MuteControl.GetComponent<Toggle>().isOn;
        PlayerCamera.GetComponent<AudioSource>().mute = CurrentInputMute;
        if (!CurrentInputMute)
        {
            PlayerCamera.GetComponent<AudioSource>().Play();
        }
    }

    public void OnValueChangedVolume()
    {
        CurrentVolume = (VolumeSelector.GetComponent<Slider>().value);
        Debug.Log(CurrentVolume.ToString());
        PlayerCamera.GetComponent<AudioSource>().volume = (CurrentVolume/100);
        VolumeSelector.transform.GetChild(4).GetComponent<Text>().text = CurrentVolume.ToString();
    }

    public void SaveandExit()
    {
        PlayerPrefs.SetInt("screensize", (int)CurrentScreenSize);
        PlayerPrefs.SetInt("quality", (int)CurrentQuality);
        PlayerPrefs.SetInt("key_up", KeyboardLayout[0]);
        PlayerPrefs.SetInt("key_down", KeyboardLayout[1]);
        PlayerPrefs.SetInt("key_left", KeyboardLayout[2]);
        PlayerPrefs.SetInt("key_right", KeyboardLayout[3]);
        if(!CurrentInputMute)
        {
            PlayerPrefs.SetInt("mute", 0);
        }
        else
        {
            PlayerPrefs.SetInt("mute", 1);
        }
        PlayerPrefs.SetInt("volume", (int)(CurrentVolume*100));
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
        if(Warning.active == true)
        {
            Warning.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
