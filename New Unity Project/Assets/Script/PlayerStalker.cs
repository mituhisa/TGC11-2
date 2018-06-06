using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStalker : MonoBehaviour {

    GameObject player;
    Vector3 initpos;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player") as GameObject;
        initpos = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.x > 0)
        {
            Vector3 pos = player.transform.position;
            if (player.transform.position.y < initpos.y)
            {
                pos.y = initpos.y;
            }
            pos += new Vector3(0, 0.95f, -10);
            transform.position = pos;
        }
        else
        {
            Vector3 pos = player.transform.position;
            pos.x = 0;
            pos += new Vector3(0, 0.95f, -10);
            transform.position = pos;
        }

        if(player.transform.position.x > 36.5f)
        {
            Vector3 pos = player.transform.position;
            pos.x = 36.5f;
            pos += new Vector3(0, 0.95f, -10);
            transform.position = pos;
        }
	}
}
