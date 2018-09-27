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
    GameObject cubecontroller;
    GameObject[] Cubes;
    GameObject txt_dir;
    GameObject txt_score;
    GameObject txt_clear;
    GameObject scrollbar_gametimer;
    GameObject txt_starttimer;
    GameObject Monster;
    GameObject Player;
    Quaternion CubeRotation;
    public float timer, timerbeforestart, movetimer;
    public bool start, keyeventon, ismoving, regreted;
    public int timesplayed, score, rannum;
    public Direction answer, playeranswer;
    KeyCode kanswer;

    public float speed = 0;
    // Use this for initialization
    void Start()
    {
        //Version 1
        cubecontroller = this.transform.GetChild(0).gameObject;
        Cubes = new GameObject[cubecontroller.transform.childCount];
        for (int i = 0; i < cubecontroller.transform.childCount; ++i)
        {
            Cubes[i] = cubecontroller.transform.GetChild(i).gameObject;
        }
        Monster = Cubes[2].transform.GetChild(6).gameObject;
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
        ismoving = false;
        regreted = false;
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
                // need to do a backtrack
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
                            Randomlychoosendirection();
                        }
                        keyeventon = false;
                        ismoving = true;
                        timesplayed++;
                        scrollbar_gametimer.transform.GetComponent<Scrollbar>().value = 1.0f;
                        scrollbar_gametimer.SetActive(false);
                    }
                }
                if(ismoving && !regreted)
                {
                    //in the mids of changing this
                    if(movetimer < 12)
                    {
                        if(playeranswer == Direction.Up)
                        {
                            Moveupdown(true);
                        }
                        else if (playeranswer == Direction.Down)
                        {
                            Moveupdown(false);
                        }
                        else if (playeranswer == Direction.Left)
                        {
                            Moveleftright(false);
                        }
                        else if (playeranswer == Direction.Right)
                        {
                            Moveleftright(true);
                        }
                        else
                        {
                            Debug.LogWarning("Missing move Direction");
                            return;
                        }
                        movetimer += Time.deltaTime * speed;
                    }
                    else
                    {
                        if (playeranswer == Direction.Up)
                        {
                            GameObject temp = Cubes[0];
                            Cubes[0] = Cubes[2];
                            Cubes[2] = Cubes[4];
                            Cubes[4] = temp;
                        }
                        else if (playeranswer == Direction.Down)
                        {
                            GameObject temp = Cubes[4];
                            Cubes[4] = Cubes[2];
                            Cubes[2] = Cubes[0];
                            Cubes[0] = temp;
                        }
                        else if (playeranswer == Direction.Left)
                        {
                            GameObject temp = Cubes[1];
                            Cubes[1] = Cubes[2];
                            Cubes[2] = Cubes[3];
                            Cubes[3] = temp;
                        }
                        else if (playeranswer == Direction.Right)
                        {
                            GameObject temp = Cubes[3];
                            Cubes[3] = Cubes[2];
                            Cubes[2] = Cubes[1];
                            Cubes[1] = temp;
                        }
                        Reset();
                        start = true;
                        movetimer = 0;
                        ismoving = false;
                    }
                }
                //else if (ismoving && regreted)
                //{
                //    if (movetimer > 0)
                //    {
                //        if (playeranswer == Direction.Up)
                //        {
                //            Moveupdown(true);
                //        }
                //        else if (playeranswer == Direction.Down)
                //        {
                //            Moveupdown(false);
                //        }
                //        else if (playeranswer == Direction.Left)
                //        {
                //            Moveleftright(false);
                //        }
                //        else if (playeranswer == Direction.Right)
                //        {
                //            Moveleftright(true);
                //        }
                //        else
                //        {
                //            Debug.LogWarning("Missing move Direction");
                //            return;
                //        }
                //        movetimer += Time.deltaTime * speed;
                //    }
                //    else
                //    {
                //        Constrain();
                //        if (answer == Direction.Up)
                //        {
                //            GameObject temp = Cubes[0];
                //            Cubes[0] = Cubes[2];
                //            Cubes[2] = Cubes[4];
                //            Cubes[4] = temp;
                //        }
                //        else if (answer == Direction.Down)
                //        {
                //            GameObject temp = Cubes[4];
                //            Cubes[4] = Cubes[2];
                //            Cubes[2] = Cubes[0];
                //            Cubes[0] = temp;
                //        }
                //        else if (answer == Direction.Left)
                //        {
                //            GameObject temp = Cubes[1];
                //            Cubes[1] = Cubes[2];
                //            Cubes[2] = Cubes[3];
                //            Cubes[3] = temp;
                //        }
                //        else if (answer == Direction.Right)
                //        {
                //            GameObject temp = Cubes[3];
                //            Cubes[3] = Cubes[2];
                //            Cubes[2] = Cubes[1];
                //            Cubes[1] = temp;
                //        }
                //        start = true;
                //        movetimer = 0;
                //        ismoving = false;
                //    }

                //}
            }
        }
        txt_score.transform.GetComponent<Text>().text = "Score: " + score.ToString();
        txt_clear.transform.GetComponent<Text>().text = "Round: " + timesplayed.ToString();
    }

    private void OnGUI()
    {
        if (keyeventon)
        {
            if (!ismoving)
            {
                if (Input.anyKeyDown && Event.current.type == EventType.KeyDown)
                {
                    if (Keycheck(Event.current.keyCode))
                    {
                        // and changing this too
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
                                // you gt it wrong, next
                                txt_dir.GetComponent<Text>().text = "Wrong";
                                SpawnGameOverMonster(Event.current.keyCode);
                            }
                        }
                        else
                        {
                            if(Event.current.keyCode != kanswer)
                            {
                                // you got it right, next
                                score++;
                                // maybe add timer to score?
                                txt_dir.GetComponent<Text>().text = "Correct";
                                //Playerchoosendirection(Event.current.keyCode);
                            }
                            else
                            {
                                // you gt it wrong, next
                                txt_dir.GetComponent<Text>().text = "Wrong";
                                SpawnGameOverMonster(Event.current.keyCode);
                                //Randomlychoosendirection();
                            }
                        }
                        Playerchoosendirection(Event.current.keyCode);
                        timesplayed++;
                        keyeventon = false;
                        ismoving = true;
                        scrollbar_gametimer.transform.GetComponent<Scrollbar>().value = 1.0f;
                        scrollbar_gametimer.SetActive(false);
                    }
                }
            }
            //if (ismoving && !regreted)
            //{

            //}
        }
    }

    private bool Keycheck(KeyCode input)
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

    private void Playerchoosendirection(KeyCode input) //seems to have error with monster movements
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
        playeranswer = temp;
    }

    private void Randomlychoosendirection()
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

    private void Moveupdown(bool input)
    {
        if (input)
        {
            for (int i = 0; i < Cubes.Length; ++i)
            {
                if (i % 2 == 0)
                {
                    Cubes[i].transform.localPosition = new Vector3(Cubes[i].transform.localPosition.x, Cubes[i].transform.localPosition.y + (speed * Time.deltaTime), Cubes[i].transform.localPosition.z);
                }
            }
        }
        else
        {
            for (int i = 0; i < Cubes.Length; ++i)
            {
                if (i % 2 == 0)
                {
                    Cubes[i].transform.localPosition = new Vector3(Cubes[i].transform.localPosition.x, Cubes[i].transform.localPosition.y - (speed * Time.deltaTime), Cubes[i].transform.localPosition.z);
                }
            }
        }
    }

    private void Moveleftright(bool input)
    {
        if (input)
        {
            for (int i = 0; i < Cubes.Length; ++i)
            {
                if (i == 1 || i == 2 || i == 3)
                {
                    Cubes[i].transform.localPosition = new Vector3(Cubes[i].transform.localPosition.x + (speed * Time.deltaTime), Cubes[i].transform.localPosition.y, Cubes[i].transform.localPosition.z);
                }
            }
        }
        else
        {
            for (int i = 0; i < Cubes.Length; ++i)
            {
                if (i == 1 || i == 2 || i == 3)
                {
                    Cubes[i].transform.localPosition = new Vector3(Cubes[i].transform.localPosition.x - (speed * Time.deltaTime), Cubes[i].transform.localPosition.y, Cubes[i].transform.localPosition.z);
                }
            }
        }
    }

    private void Constrain()
    {
        for (int i = 0; i < Cubes.Length; ++i)
        {
            if (Cubes[i].transform.localPosition.x >= 23)
            {
                Cubes[i].transform.localPosition = new Vector3(-12, Cubes[i].transform.localPosition.y, Cubes[i].transform.localPosition.z);
            }
            else if (Cubes[i].transform.localPosition.x <= -23)
            {
                Cubes[i].transform.localPosition = new Vector3(12, Cubes[i].transform.localPosition.y, Cubes[i].transform.localPosition.z);
            }
            if (Cubes[i].transform.localPosition.y >= 23)
            {
                Cubes[i].transform.localPosition = new Vector3(Cubes[i].transform.localPosition.x, -12, Cubes[i].transform.localPosition.z);
            }
            else if (Cubes[i].transform.localPosition.y <= -23)
            {
                Cubes[i].transform.localPosition = new Vector3(Cubes[i].transform.localPosition.x, 12, Cubes[i].transform.localPosition.z);
            }
        }
    }

    private void Reset()
    {
        Cubes[0].transform.localPosition = new Vector3(0, 12, 0);
        Cubes[1].transform.localPosition = new Vector3(-12, 0, 0);
        Cubes[2].transform.localPosition = new Vector3(0, 0, 0);
        Cubes[3].transform.localPosition = new Vector3(12, 0, 0);
        Cubes[4].transform.localPosition = new Vector3(0, -12, 0);
    }

    private void SpawnGameOverMonster(KeyCode Direction)
    {
         if (Direction == (KeyCode)PlayerPrefs.GetInt("key_up"))
        {
            Monster.transform.SetParent(Cubes[0].transform);
            Monster.transform.localPosition = new Vector3(-0.5f, 0, -0.25f);
            Monster.transform.Rotate(new Vector3(180, 0, 0));
        }
        else if (Direction == (KeyCode)PlayerPrefs.GetInt("key_down"))
        {
            Monster.transform.SetParent(Cubes[4].transform);
            Monster.transform.localPosition = new Vector3(-0.5f, 0, 0.25f);
        }
        if (Direction == (KeyCode)PlayerPrefs.GetInt("key_left"))
        {
            Monster.transform.SetParent(Cubes[0].transform);
            Monster.transform.localPosition = new Vector3(-0.5f, 0.25f, 0);
            Monster.transform.Rotate(new Vector3(90, 0, 0));
        }
        if (Direction == (KeyCode)PlayerPrefs.GetInt("key_up"))
        {
            Monster.transform.SetParent(Cubes[0].transform);
            Monster.transform.localPosition = new Vector3(-0.5f, -0.25f, 0);
            Monster.transform.Rotate(new Vector3(-90, 0, 0));
        }
    }
}
