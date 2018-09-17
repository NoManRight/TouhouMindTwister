using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () { //not done
        if (Input.GetKey((KeyCode)PlayerPrefs.GetInt("key_up")))
        {
            player.transform.position.Set(player.transform.position.x, player.transform.position.y+2, player.transform.position.z);
        }
        else if (Input.GetKey((KeyCode)PlayerPrefs.GetInt("key_down")))
        {
            player.transform.position.Set(player.transform.position.x, player.transform.position.y - 2, player.transform.position.z);
        }
        //if (Input.GetKey((KeyCode)PlayerPrefs.GetInt("key_left")))
        //{
        //    player.transform.position.Set(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);
        //}
        //else if (Input.GetKey((KeyCode)PlayerPrefs.GetInt("key_right")))
        //{
        //    player.transform.position.Set(player.transform.position.x, player.transform.position.y - 2, player.transform.position.z);
        //}
    }
}
