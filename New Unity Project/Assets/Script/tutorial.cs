using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour {

    public GameObject text;
    public GameObject text2;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (transform.name == "tutorial4")
        {
            if (Input.GetKey(KeyCode.V) || Input.GetButton("Paste"))
            {
                text.gameObject.SetActive(false);
                text2.gameObject.SetActive(true);
            }
            else
            {
                text2.gameObject.SetActive(false);
                text.gameObject.SetActive(true);
            }
        }
        else
        {
            text.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        text.gameObject.SetActive(false);
    }
}
