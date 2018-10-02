using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "MiniGameDictory", menuName = "MiniGame/Dictory", order = 0)]
[System.Serializable]

public class MiniGameDictory : ScriptableObject {

    public List<CharString> Dictory = new List<CharString>();

}

[System.Serializable]
public class CharString
{
    [SerializeField]
    public char code;
    public KeyCode meaning;
}