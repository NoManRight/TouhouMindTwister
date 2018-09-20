using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeinScript : MonoBehaviour {
    public float fadeinstart;
    public float fadeinend;
    public float fadeincurrent;
    public float fadespeed;

    bool fadein;
	// Use this for initialization
	void Start () {
        //fadeincurrent = this.
		if(fadeinstart < fadeinend)
        {
            fadein = true;
        }
        else if (fadeinstart > fadeinend)
        {
            fadein = false;
        }
        else // if both equals to each other, there is no fade in or fade out
        {
            Destroy(this.GetComponent<FadeinScript>());
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (fadein)
        {
            if (fadeinstart < fadeinend)
            {
                fadeincurrent += fadespeed;
            }
        }
	}
}
