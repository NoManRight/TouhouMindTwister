using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotNot : MonoBehaviour
{
    public enum Direction
    {
        Up = 0,
        Down,
        Left,
        Right,
        Not_Up,
        Not_Down,
        Not_Left,
        Not_Right,
        Direction_Total,
    }
    GameObject Cube;
    GameObject txt_dir;
    GameObject txt_score;
    GameObject txt_clear;
    GameObject scrollbar_gametimer;
    GameObject txt_starttimer;
    Quaternion CubeRotation;
    public float timer, timerbeforestart, rolltimer;
    public bool start, keyeventon, isrolling;
    public int timesplayed, score, rannum;
    public Direction answer;
    KeyCode kanswer;

    public float speed = 0;
    // Use this for initialization
    void Start()
    {
        //Version 1
        Cube = transform.GetChild(0).gameObject;
        CubeRotation = Cube.transform.rotation;
        txt_dir = transform.GetChild(1).GetChild(0).gameObject;
        txt_score = transform.GetChild(1).GetChild(1).gameObject;
        txt_clear = transform.GetChild(1).GetChild(2).gameObject;
        scrollbar_gametimer = transform.GetChild(1).GetChild(3).gameObject;
        txt_starttimer = transform.GetChild(1).GetChild(4).gameObject;
        timer = 0;
        timerbeforestart = 3;
        txt_starttimer.transform.GetComponent<Text>().text = ((int)timerbeforestart).ToString();
        start = true;
        keyeventon = false;
        isrolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerbeforestart > 0)
        {
            timerbeforestart -= Time.deltaTime;
            txt_starttimer.transform.GetComponent<Text>().text = ((int)timerbeforestart).ToString();
            //update text on screen
        }
        else
        {
            if (txt_starttimer.activeSelf)
            {
                txt_starttimer.SetActive(false);
            }
            if (start)
            {
                rannum = Random.Range(0, (int)Direction.Direction_Total);
                switch (rannum)
                {
                    case 0:
                        answer = Direction.Up;
                        kanswer = (KeyCode)PlayerPrefs.GetInt("key_up");
                        break;
                    case 1:
                        answer = Direction.Down;
                        kanswer = (KeyCode)PlayerPrefs.GetInt("key_down");
                        break;
                    case 2:
                        answer = Direction.Left;
                        kanswer = (KeyCode)PlayerPrefs.GetInt("key_left");
                        break;
                    case 3:
                        answer = Direction.Right;
                        kanswer = (KeyCode)PlayerPrefs.GetInt("key_right");
                        break;
                    case 4:
                        answer = Direction.Not_Up;
                        kanswer = (KeyCode)PlayerPrefs.GetInt("key_up");
                        break;
                    case 5:
                        answer = Direction.Not_Down;
                        kanswer = (KeyCode)PlayerPrefs.GetInt("key_down");
                        break;
                    case 6:
                        answer = Direction.Not_Left;
                        kanswer = (KeyCode)PlayerPrefs.GetInt("key_left");
                        break;
                    case 7:
                        answer = Direction.Not_Right;
                        kanswer = (KeyCode)PlayerPrefs.GetInt("key_right");
                        break;
                    default:
                        Debug.LogWarning("Value over 3");
                        break;
                }
                if (rannum > 3)
                {
                    string temp = (answer.ToString()).Replace("Not_", "Not ");
                    txt_dir.GetComponent<Text>().text = temp;
                }
                else
                {
                    txt_dir.GetComponent<Text>().text = answer.ToString();
                }
                timer = 7;
                scrollbar_gametimer.transform.GetComponent<Scrollbar>().value = 1.0f;
                scrollbar_gametimer.transform.GetChild(1).GetComponent<Text>().text = ((int)timer).ToString();
                scrollbar_gametimer.SetActive(true);
                keyeventon = true;
                start = false;
            }
            else
            {
                if (keyeventon)
                {
                    timer -= Time.deltaTime;
                    scrollbar_gametimer.transform.GetComponent<Scrollbar>().value = timer/7;
                    scrollbar_gametimer.transform.GetChild(1).GetComponent<Text>().text = ((int)timer).ToString();
                    if (timer <= 0)
                    {
                        txt_dir.GetComponent<Text>().text = "Missed";
                        if (rannum > 3)
                        {
                            randomlychoosendirection();
                        }
                        keyeventon = false;
                        isrolling = true;
                        timesplayed++;
                        scrollbar_gametimer.transform.GetComponent<Scrollbar>().value = 1.0f;
                        scrollbar_gametimer.SetActive(false);
                    }
                }
                if(isrolling)
                {
                    if(rolltimer < 90)
                    {
                        if(answer == Direction.Up)
                        {
                            Cube.transform.Rotate(Vector3.left * Time.deltaTime * speed, Space.World);
                        }
                        else if (answer == Direction.Down)
                        {
                            Cube.transform.Rotate(Vector3.right * Time.deltaTime * speed, Space.World); // moved left
                        }
                        else if (answer == Direction.Left)
                        {
                            Cube.transform.Rotate(Vector3.down * Time.deltaTime * speed, Space.World); //moved up
                        }
                        else if (answer == Direction.Right)
                        {
                            Cube.transform.Rotate(Vector3.up * Time.deltaTime * speed, Space.World); //moved down
                        }
                        else
                        {
                            Debug.LogWarning("Missing Roll Direction");
                            return;
                        }
                        rolltimer += Time.deltaTime * speed;
                    }
                    else
                    {
                        Cube.transform.rotation = CubeRotation;
                        if (answer == Direction.Up)
                        {
                            Cube.transform.Rotate(Vector3.left * 90, Space.World);
                        }
                        else if (answer == Direction.Down)
                        {
                            Cube.transform.Rotate(Vector3.right * 90, Space.World);
                        }
                        else if (answer == Direction.Left)
                        {
                            Cube.transform.Rotate(Vector3.down * 90, Space.World);
                        }
                        else if (answer == Direction.Right)
                        {
                            Cube.transform.Rotate(Vector3.up * 90, Space.World);
                        }
                        CubeRotation = Cube.transform.rotation;
                        start = true;
                        rolltimer = 0;
                        isrolling = false;
                    }
                }
            }
        }
        txt_score.transform.GetComponent<Text>().text = "Score: " + score.ToString();
        txt_clear.transform.GetComponent<Text>().text = "Round: " + timesplayed.ToString();
    }

    private void OnGUI()
    {
        if (keyeventon)
        {
            if (Input.anyKeyDown && Event.current.type == EventType.KeyDown)
            {
                if (keycheck(Event.current.keyCode))
                {
                    Debug.Log(Event.current.keyCode);
                    if (rannum < 4)
                    {
                        if (Event.current.keyCode == kanswer)
                        {
                            // you got it right, next
                            score++;
                            // maybe add timer to score?
                            txt_dir.GetComponent<Text>().text = "Correct";
                        }
                        else
                        {
                            // you got it wrong, next
                            txt_dir.GetComponent<Text>().text = "Wrong";
                        }
                    }
                    else
                    {
                        if (Event.current.keyCode != kanswer)
                        {
                            // you got it right, next
                            score++;
                            // maybe add timer to score?
                            txt_dir.GetComponent<Text>().text = "Correct";
                            playerchoosendirection(Event.current.keyCode);
                        }
                        else
                        {
                            // you got it wrong, next
                            txt_dir.GetComponent<Text>().text = "Wrong";
                            randomlychoosendirection();
                        }
                    }
                    timesplayed++;
                    isrolling = true;
                    keyeventon = false;
                    scrollbar_gametimer.transform.GetComponent<Scrollbar>().value = 1.0f;
                    scrollbar_gametimer.SetActive(false);
                }
            }
        }
    }

    private bool keycheck(KeyCode input)
    {
        if(input == (KeyCode)PlayerPrefs.GetInt("key_up") || 
            input == (KeyCode)PlayerPrefs.GetInt("key_down") || 
            input == (KeyCode)PlayerPrefs.GetInt("key_left") || 
            input == (KeyCode)PlayerPrefs.GetInt("key_right"))
        {
            return true;
        }
        return false;
    }

    private void playerchoosendirection(KeyCode input)
    {
        Direction temp = Direction.Direction_Total;
        if (input == (KeyCode)PlayerPrefs.GetInt("key_up"))
        {
            temp = Direction.Up;
        }
        else if (input == (KeyCode)PlayerPrefs.GetInt("key_down"))
        {
            temp = Direction.Down;
        }
        else if (input == (KeyCode)PlayerPrefs.GetInt("key_left"))
        {
            temp = Direction.Left;
        }
        else if (input == (KeyCode)PlayerPrefs.GetInt("key_right"))
        {
            temp = Direction.Right;
        }
        answer = temp;
    }

    private void randomlychoosendirection()
    {
        rannum -= 4;
        while ((rannum + 4) == (int)answer)
        {
            rannum = Random.Range(0, 4);
            Debug.Log("rannum = " + rannum.ToString());
        }
        answer = (Direction)rannum;
        Debug.Log("rannum = " + rannum.ToString());
    }
}
