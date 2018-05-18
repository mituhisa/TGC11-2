using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGage : MonoBehaviour {
    float time;
	// Use this for initialization
	void Start () {
        GameObject perent= transform.parent.gameObject;
        time = perent.GetComponent<Item>().deletTime;
        Vector2 Pos = perent.transform.position;
        Pos.y += -0.5f;
        gameObject.transform.position = Pos;
        iTween.ScaleTo(this.gameObject, iTween.Hash("x", 0f, "time", time));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
