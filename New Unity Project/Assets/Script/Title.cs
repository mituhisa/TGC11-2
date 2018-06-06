using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
    public enum Mode
    {
        Opning,
        Title,
        Menu,
        Select,
    };

    public GameObject charactor;
    public GameObject SMKT;
    public GameObject yazirusi;
    public Mode mode;
    Vector3 titlePos;

	// Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().name == "Home")
        {
            charactor.gameObject.SetActive(true);
            mode = Mode.Title;
        }
        else
        {
            mode = Mode.Opning;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case Mode.Opning:
                SMKT.SetActive(true);
                charactor.SetActive(true);
                break;
            case Mode.Title:
                SMKT.SetActive(false);
                break;
            case Mode.Menu:
                //矢印上下移動
                float gamepadY = Input.GetAxis("Vertical");
                if (Input.GetKeyDown(KeyCode.UpArrow) || gamepadY == 1)
                {
                    if(yazirusi.transform.position.y != -0.7f)
                    {
                        yazirusi.transform.position = new Vector3(-1.8f, -0.7f, 0);
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) || gamepadY == -1)
                {
                    if(yazirusi.transform.position.y != -1.9f)
                    {
                        yazirusi.transform.position = new Vector3(-1.7f, -1.9f, 0);
                    }
                }

                //選択分岐
                if(yazirusi.transform.position.y >= -0.7f)
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Copy") && charactor.transform.position.x > -4.5f)
                    {
                        charactor.GetComponent<charctor>().mode = charctor.Mode.Copy;
                        charactor.GetComponent<charctor>().waitTime = 0;
                        GameObject.Find("Line").GetComponent<Line>().senFlg = false;
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("End");
                }
                break;
            case Mode.Select:
                Debug.Log("SelectMode");
   
                break;
        }
	}
}
