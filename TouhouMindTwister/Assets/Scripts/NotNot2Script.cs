using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotNot2Script : MonoBehaviour
{

    public MiniGameDictory miniGameDictory;

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
    //public Direction answer, playeranswer;
    KeyCode kanswer;

    public float speed = 0;

    // Use this for initialization
    void Start()
    {
        cubecontroller = this.transform.GetChild(0).gameObject;
        Cubes = new GameObject[cubecontroller.transform.childCount];
        for (int i = 0; i < cubecontroller.transform.childCount; ++i)
        {
            Cubes[i] = cubecontroller.transform.GetChild(i).gameObject;
        }
        Monster = Cubes[2].transform.GetChild(1).gameObject;
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

    }

    void generate_instruction()
    {
        string instruction = "";
        int i = 0;
        int rannum = 0;
        while (true)
        {
            // i = 0, randomize the first set of instructions
            // i = 1, randomize the conditions(and not)
            // if and/not is added, randomize the 3rd set of instruction
            if (i == 0)
            {
                rannum = Random.Range(0, 2);
                if(rannum == 1)
                {
                    rannum = Random.Range(0, 2);
                    i++;
                }
                else
                {
                    instruction += miniGameDictory.Dictory[rannum];
                }
            }
            if (i == 1)
            {
                rannum = Random.Range(0, 2);
                if (rannum == 0)
                {
                    break;
                }
                rannum = Random.Range(6, 8);
                instruction += miniGameDictory.Dictory[rannum]; //either and/or
                continue;
            }
            rannum = Random.Range(0, 5);
            instruction += miniGameDictory.Dictory[rannum]; //!0123
        }
    }
}
