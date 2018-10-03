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
    public bool not, correct;
    public int timesplayed, score, rannum, number, condition;
    public string instruction;
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
        not = false;
        instruction = generate_instruction();

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    string generate_instruction()
    {
        instruction = "";
        int i = 0;
        int rannum = 0;
        while (i < 3)
        {
            // i = 0, randomize the first set of instructions
            // i = 1, randomize the conditions(and not)
            // if and/not is added, randomize the 3rd set of instruction
            if (i == 0 || i == 2)
            {
                rannum = Random.Range(0, 2);
                if(rannum == 1)
                {
                    rannum = Random.Range(1, 5);
                    instruction += miniGameDictory.Dictory[rannum].code;
                    i++;
                }
                else
                {
                    instruction += miniGameDictory.Dictory[rannum].code;
                }
            }
            else
            {
                rannum = Random.Range(0, 2);
                if (rannum == 0)
                {
                    i++;
                    continue;
                }
                rannum = Random.Range(5, 7);
                instruction += miniGameDictory.Dictory[rannum].code; //either and/or
                continue;
            }
        }
        return instruction;
    }

    void determine(string instruction, KeyCode input)
    {
        if (Keycheck(input))
        {
            return;
        }
        correct = true;
        condition = 0;
        simplify(instruction);
        for (int i = 0; i < instruction.Length; ++i)
        {
            if (instruction[i] == miniGameDictory.Dictory[6].code)
            {
                condition = 1;
                break;
            }
            else if (instruction[i] == miniGameDictory.Dictory[5].code)
            {
                condition = 2;
                break;
            }
            else
            {
                continue;
            }
        }
        for (int i = 0; i < instruction.Length; ++i)
        {
            if (instruction[i] == miniGameDictory.Dictory[0].code)
            {
                not = true;
                continue;
            }
            else if (instruction[i] == miniGameDictory.Dictory[5].code || instruction[i] == miniGameDictory.Dictory[6].code)
            {
                continue;
            }
            foreach (CharString codes in miniGameDictory.Dictory)
            {
                if (codes.code == instruction[i])
                {
                    kanswer = codes.meaning;
                    break;
                }
            }
            if (kanswer == input)
            {
                if (not && condition != 2)
                {
                    correct = false;
                    not = false;
                    break;
                }
                else if (not)
                {
                    correct = false;
                }
                not = false;
                continue;
            }
            else
            {
                if (!not && condition != 2)
                {
                    correct = false;
                    not = false;
                    break;
                }
                else if (!not)
                {
                    correct = false;
                }
                not = false;
                continue;
            }
        }
    }

    string simplify(string instruction)
    {
        return instruction.Replace("!!",string.Empty); 
    }

    private bool Keycheck(KeyCode input)
    {
        if (input == (KeyCode)PlayerPrefs.GetInt("key_up") ||
            input == (KeyCode)PlayerPrefs.GetInt("key_down") ||
            input == (KeyCode)PlayerPrefs.GetInt("key_left") ||
            input == (KeyCode)PlayerPrefs.GetInt("key_right"))
        {
            return true;
        }
        return false;
    }
}
