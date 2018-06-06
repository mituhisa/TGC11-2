using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public float SetCnt;
    public float deletTime = 2.0f;
    public Player player;
    float ItmCnt;
    // Use this for initialization
    void Start () {
        SetCnt = 0;
        ItmCnt = 0;
        player = GameObject.Find("player").GetComponent<Player>();
        //transform.GetChild(0).gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (SetCnt == 1)
        {
            if(player.Vcnt == 0)ItmCnt += 1;
            //transform.GetChild(0).gameObject.SetActive(true);
            if (ItmCnt / 60 == deletTime) Destroy(gameObject, deletTime); 
        }
	}
}
