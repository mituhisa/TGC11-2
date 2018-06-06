using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverController : MonoBehaviour {

    public Transform appearFloor1;
    public Transform appearFloor2;


    // Use this for initialization
    void Start () {
		 transform.localScale = new Vector3(-1, 1, 1);

        appearFloor1.gameObject.SetActive(false);
        appearFloor2.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {

        transform.localScale = new Vector3(1,1, 1);
        transform.Translate(0.1f, 0, 0);


        if(transform.tag == "lever1")
        {
            appearFloor1.gameObject.SetActive(true);
        }

        if (transform.tag == "lever2")
        {
            appearFloor2.gameObject.SetActive(true);
        }


    }



}
