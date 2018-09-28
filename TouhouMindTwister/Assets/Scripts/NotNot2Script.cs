using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotNot2Script : MonoBehaviour {

    public MiniGameDictory miniGameDictory;
    
	// Use this for initialization
	void Start () {
        int rannum = Random.Range(0, 8);

        string name = miniGameDictory.Dictory[rannum].meaning;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
