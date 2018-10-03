using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "MiniGameDictory", menuName = "MiniGame/Dictory", order = 0)]
[System.Serializable]

public class MiniGameDictory : ScriptableObject {

    public List<CharString> Dictory = new List<CharString>();
    public List<Character> CharacteList = new List<Character>();

}

[System.Serializable]
public class CharString
{
    [SerializeField]
    public char code;
    public KeyCode meaning;
}

[System.Serializable]
public class Character
{
    [SerializeField]
    public int ID;
    public string Name;
    public GameObject Data;
}