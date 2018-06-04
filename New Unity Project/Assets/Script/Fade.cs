using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    float alfa;
    public GameObject yazirusi; 
    float fadespeed = 0.01f;
    float red, green, blue;

	// Use this for initialization
	void Start () {
        red = GetComponent<Text>().color.r;
        green = GetComponent<Text>().color.g;
        blue = GetComponent<Text>().color.b;
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().color = new Color(red, green, blue, alfa);
        alfa += fadespeed;
        if (alfa >= 1) yazirusi.SetActive(true);
	}
}
