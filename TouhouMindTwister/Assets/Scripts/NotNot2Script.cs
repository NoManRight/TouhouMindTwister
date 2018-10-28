using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotNot2Script : MonoBehaviour
{

    public MiniGameDictory miniGameDictory;
    enum MAP
    {
        NOT = 0,
        UP,
        DOWN,
        LEFT,
        RIGHT,
        OR,
        AND,
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
    GameObject SkillImage;
    public float timer, timerbeforestart, movetimer;
    public bool start, keyeventon, ismoving;
    public bool score2, gameisover;
    public bool[] each_results;
    public int timesplayed, score, rannum;
    public string instruction;
    //public Direction answer, playeranswer;
    MAP kanswer;

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
        SkillImage = transform.GetChild(1).GetChild(5).gameObject;
        timer = 0;
        timerbeforestart = 3;
        txt_starttimer.transform.GetComponent<Text>().text = ((int)timerbeforestart).ToString();
        start = true;
        keyeventon = false;
        ismoving = false;
        score2 = false;
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
            if (!gameisover)
            {
                if (txt_starttimer.activeSelf)
                {
                    txt_starttimer.SetActive(false);
                }
                if (start)
                {
                    instruction = generate_instruction();
                    simplify(instruction);
                    ThrowAwayInstructions(instruction);
                    //Debug.Log(instruction);
                    txt_dir.GetComponent<Text>().text = ReadableInstruction(instruction);
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
                        scrollbar_gametimer.transform.GetComponent<Scrollbar>().value = timer / 7;
                        scrollbar_gametimer.transform.GetChild(1).GetComponent<Text>().text = ((int)timer).ToString();
                        if (timer <= 0)
                        {
                            txt_dir.GetComponent<Text>().text = "Game Over";
                            gameisover = true;
                            timer = 5;

                            //keyeventon = false;
                            //start = true;
                            //timerbeforestart = 3;
                            //txt_starttimer.SetActive(true);
                            //timesplayed++;
                            //scrollbar_gametimer.transform.GetComponent<Scrollbar>().value = 1.0f;
                            //scrollbar_gametimer.SetActive(false);
                            //SkillImage.SetActive(false);
                        }
                    }
                    if (ismoving)
                    {
                        if (movetimer < 12)
                        {
                            if (kanswer == MAP.UP)
                            {
                                Moveupdown(false);
                            }
                            else if (kanswer == MAP.DOWN)
                            {
                                Moveupdown(true);
                            }
                            else if (kanswer == MAP.LEFT)
                            {
                                Moveleftright(true);
                            }
                            else if (kanswer == MAP.RIGHT)
                            {
                                Moveleftright(false);
                            }
                            else
                            {
                                //Debug.LogWarning("Missing move Direction");
                                return;
                            }
                            movetimer += Time.deltaTime * speed;
                        }
                        else
                        {
                            if (kanswer == MAP.UP)
                            {
                                GameObject temp = Cubes[4];
                                Cubes[4] = Cubes[2];
                                Cubes[2] = Cubes[0];
                                Cubes[0] = temp;
                            }
                            else if (kanswer == MAP.DOWN)
                            {
                                GameObject temp = Cubes[0];
                                Cubes[0] = Cubes[2];
                                Cubes[2] = Cubes[4];
                                Cubes[4] = temp;
                            }
                            else if (kanswer == MAP.LEFT)
                            {
                                GameObject temp = Cubes[3];
                                Cubes[3] = Cubes[2];
                                Cubes[2] = Cubes[1];
                                Cubes[1] = temp;
                            }
                            else if (kanswer == MAP.RIGHT)
                            {
                                GameObject temp = Cubes[1];
                                Cubes[1] = Cubes[2];
                                Cubes[2] = Cubes[3];
                                Cubes[3] = temp;
                            }
                            Reset();
                            start = true;
                            movetimer = 0;
                            ismoving = false;
                        }
                    }
                }
            }
            else
            {
                if(timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    GameOver();
                }
            }
            txt_score.transform.GetComponent<Text>().text = "Score: " + score.ToString();
            txt_clear.transform.GetComponent<Text>().text = "Round: " + timesplayed.ToString();
        }
    }

    private void OnGUI()
    {
        if (!gameisover)
        {
        if (Input.anyKeyDown && Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode == (KeyCode)PlayerPrefs.GetInt("key_skill"))
            {
                //Debug.Log("skill used");
                //GameController.instance.miniGameDictory.CharacteList[GameController.instance.PlayerCharacter].Data.GetComponent<Skill_Alice>().; will complete when alice skill is complete
                if (GameController.instance.PlayerCharacter == 2 && !score2)
                {
                    score2 = true;
                    SkillImage.SetActive(true);
                }
            }
        }
        if (keyeventon)
        {
            if (Input.anyKeyDown && Event.current.type == EventType.KeyDown)
            {
                if (Keycheck(Event.current.keyCode))
                {
                    // and changing this too
                    //Debug.Log(Event.current.keyCode);
                    if (determine(instruction, Event.current.keyCode))
                    {
                        if (score2)
                        {
                            score++;
                            score2 = false;
                        }
                        score++;
                        // maybe add timer to score?
                        txt_dir.GetComponent<Text>().text = "Correct";
                    }
                    else
                    {
                        // you gt it wrong, next
                        //txt_dir.GetComponent<Text>().text = "Wrong";
                        //SpawnGameOverMonster(Event.current.keyCode);
                        txt_dir.GetComponent<Text>().text = "Game Over";
                        gameisover = true;
                        timer = 5;
                    }
                    
                    Playerchoosendirection(Event.current.keyCode);
                    timesplayed++;
                    keyeventon = false;
                    ismoving = true;
                    scrollbar_gametimer.transform.GetComponent<Scrollbar>().value = 1.0f;
                    scrollbar_gametimer.SetActive(false);
                    SkillImage.SetActive(false);
                    if(score2)
                    {
                        score2 = false;
                    }
                }
            }
            }

        }
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
                    instruction += miniGameDictory.Dictory[rannum].code.ToString() + ",";
                    //Debug.Log(instruction);
                    i++;
                }
                else
                {
                    instruction += miniGameDictory.Dictory[(int)MAP.NOT].code.ToString();
                    //Debug.Log(instruction);
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
                instruction += miniGameDictory.Dictory[rannum].code.ToString() + ","; //either and/or
                i++;
                //Debug.Log(instruction);
                continue;
            }
        }
        //Debug.Log(instruction);
        return instruction;
    }

    bool determine(string instruction, KeyCode input)
    {
        string[] temp = instruction.Split(',');
        each_results = new bool[temp.Length-1];
        string operands = temp[1];
        if (!operands.Contains(miniGameDictory.Dictory[(int)MAP.OR].code.ToString()) && !operands.Contains(miniGameDictory.Dictory[(int)MAP.AND].code.ToString()))
        {
            temp = new string[1] { temp[0] };
        }
        else
        {

            temp[1] = temp[2];
            temp = new string[2] { temp[0], temp[1] };
        }
        for (int i = 0; i < temp.Length; ++i)
        {
            bool not = false;
            if (temp[i].Contains(miniGameDictory.Dictory[(int)MAP.NOT].code.ToString()))
            {
                not = true;
                temp[i] = temp[i].Substring(1);
            }
            //Debug.Log(not ^ true);
            //Debug.Log(temp[i] + " " + ConvertUserInputString(input)+ " "+ not);
            each_results[i] = (not ^ (temp[i] == ConvertUserInputString(input)));
            //Debug.Log(each_results[i]);
        }
        return process_operands(each_results, operands);
    }

    string simplify(string instructions)
    {
        instruction = instructions.Replace("!!", string.Empty);
        Debug.Log(instruction);
        return instruction;
    }

    bool process_operands(bool[] result, string operands)
    {
        int counter = 0;
        bool Result = each_results[0];
        foreach (bool compare in result)
        {
            if (counter == 2)
            {
                break;
            }
            if (operands.Contains(miniGameDictory.Dictory[(int)MAP.AND].code.ToString()) && compare == false)
            {
                Result = false;
                
            }
            else if (compare == true)
            {
                if ((Result == true && operands.Contains(miniGameDictory.Dictory[(int)MAP.AND].code.ToString())) || operands.Contains(miniGameDictory.Dictory[(int)MAP.OR].code.ToString()))
                {
                    Result = true;
                }
            }
            counter++;
        }
        return Result;
    }

    string ConvertUserInputString(KeyCode input)
    {
        if (input == (KeyCode)PlayerPrefs.GetInt("key_up"))
        {
            return "0";
        }
        else if (input == (KeyCode)PlayerPrefs.GetInt("key_down"))
        {
            return "1";
        }
        else if (input == (KeyCode)PlayerPrefs.GetInt("key_left"))
        {
            return "2";
        }
        else if (input == (KeyCode)PlayerPrefs.GetInt("key_right"))
        {
            return "3";
        }
        return null;
    }

    void ThrowAwayInstructions(string instructions)
    {
        string[] temp = instruction.Split(',');
        if (temp[0].Contains(temp[2]) || temp[2].Contains(temp[0])) //condition 1: where both condition 1 and 2 is same/!same
        {
            instruction = temp[0] + "," + temp[2] + ",";
        }
        else if (!temp[0].Contains(miniGameDictory.Dictory[(int)MAP.NOT].code.ToString()) && !temp[2].Contains(miniGameDictory.Dictory[(int)MAP.NOT].code.ToString()))
        {
            instruction = temp[0] + "," + temp[2] + ",";
        }
    }

    string ReadableInstruction(string instructions)
    {
        string[] temp = instructions.Split(',');
        if (!temp[1].Contains(miniGameDictory.Dictory[(int)MAP.OR].code.ToString()) && !temp[1].Contains(miniGameDictory.Dictory[(int)MAP.AND].code.ToString()))
        {
            temp = new string[1] { temp[0] };
        }
        string Readable = "";
        foreach(string parts in temp)
        {
            for(int i = 0; i < parts.Length; ++i)
            {
                switch (parts[i])
                {
                    case '!':
                        Readable += "Not ";
                        break;
                    case '0':
                        Readable += "Up ";
                        break;
                    case '1':
                        Readable += "Down ";
                        break;
                    case '2':
                        Readable += "Left ";
                        break;
                    case '3':
                        Readable += "Right ";
                        break;
                    case '|':
                        Readable += "Or ";
                        break;
                    case '&':
                        Readable += "And ";
                        break;
                }
            }
        }
        return Readable.Trim();
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

    private void Playerchoosendirection(KeyCode input)
    {
        if (input == (KeyCode)PlayerPrefs.GetInt("key_up"))
        {
            kanswer = MAP.UP;
        }
        else if (input == (KeyCode)PlayerPrefs.GetInt("key_down"))
        {
            kanswer = MAP.DOWN;
        }
        else if (input == (KeyCode)PlayerPrefs.GetInt("key_left"))
        {
            kanswer = MAP.LEFT;
        }
        else if (input == (KeyCode)PlayerPrefs.GetInt("key_right"))
        {
            kanswer = MAP.RIGHT;
        }
    }

    private void SpawnGameOverMonster(KeyCode Direction)
    {
        if (Direction == (KeyCode)PlayerPrefs.GetInt("key_up"))
        {

            Monster.transform.SetParent(Cubes[0].transform);
            Monster.transform.localPosition = new Vector3(-0.5f, 0, -0.25f);
            Monster.transform.Rotate(new Vector3(0, 180, 0));
        }
        else if (Direction == (KeyCode)PlayerPrefs.GetInt("key_down"))
        {
            Monster.transform.SetParent(Cubes[4].transform);
            Monster.transform.localPosition = new Vector3(-0.5f, 0, 0.25f);
        }
        else if (Direction == (KeyCode)PlayerPrefs.GetInt("key_left"))
        {
            Monster.transform.SetParent(Cubes[1].transform);
            Monster.transform.localPosition = new Vector3(-0.5f, 0.25f, 0);
            Monster.transform.Rotate(new Vector3(0, 90, 0));
        }
        else if (Direction == (KeyCode)PlayerPrefs.GetInt("key_right"))
        {
            Monster.transform.SetParent(Cubes[3].transform);
            Monster.transform.localPosition = new Vector3(-0.5f, -0.25f, 0);
            Monster.transform.Rotate(new Vector3(0, -90, 0));
        }
        Monster.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Monster.SetActive(true);
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

    private void Reset()
    {
        Cubes[0].transform.localPosition = new Vector3(0, 12, 0);
        Cubes[1].transform.localPosition = new Vector3(-12, 0, 0);
        Cubes[2].transform.localPosition = new Vector3(0, 0, 0);
        Cubes[3].transform.localPosition = new Vector3(12, 0, 0);
        Cubes[4].transform.localPosition = new Vector3(0, -12, 0);
    }

    private void GameOver()
    {
        GameController.instance.ChangeScene(0);//main menu
    }
}
