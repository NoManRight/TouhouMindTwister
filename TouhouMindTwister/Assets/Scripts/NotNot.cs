using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotNot : MonoBehaviour
{
    public enum Direction
    {
        Up = 0,
        Down,
        Left,
        Right,
        Direction_Total,
    }
    GameObject Cube;
    Quaternion CubeRotation;
    public float timer, timerbeforestart, rolltimer;
    public bool start, keyeventon, isrolling;
    public int timesplayed, score;
    public Direction answer;
    KeyCode kanswer;

    public float speed = 0;
    // Use this for initialization
    void Start()
    {
        //Version 1
        Cube = transform.GetChild(0).gameObject;
        CubeRotation = Cube.transform.rotation;
        timer = 0;
        timerbeforestart = 3;
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
            //update text on screen
        }
        else
        {
            if (start)
            {
                int rannum = Random.Range(0, (int)Direction.Direction_Total);
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
                    default:
                        Debug.LogWarning("Value over 3");
                        break;
                }
                timer = 7;
                keyeventon = true;
                start = false;
            }
            else
            {
                if (keyeventon)
                {
                    timer -= Time.deltaTime;
                    if(timer <= 0)
                    {
                        keyeventon = false;
                        isrolling = true;
                        timesplayed++;
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
                        start = true;
                        rolltimer = 0;
                        isrolling = false;
                    }
                }
            }
        }

















        
    }

    private void OnGUI()
    {
        if (keyeventon)
        {
            if (Input.anyKeyDown && Event.current.type == EventType.KeyDown)
            {
                Debug.Log(Event.current.keyCode);
                if (Event.current.keyCode == kanswer)
                {
                    // you got it right, next
                    score++;
                    // maybe add timer to score?
                }
                else
                {
                    // you got it wrong, next
                }
                timesplayed++;
                isrolling = true;
                keyeventon = false;
            }
        }
    }
}
