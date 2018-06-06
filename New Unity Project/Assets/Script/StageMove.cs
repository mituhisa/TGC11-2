using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour {

    float scalelen;


	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(45, 45, 0);
	}
	
	// Update is called once per frame
	void Update () {
        scalelen = 0 - transform.position.x;
        if (scalelen < 0) scalelen *= -1;
        transform.localScale = new Vector3(45 - scalelen, 45 - scalelen, 0);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GetComponent<SpriteRenderer>().
    }
}
