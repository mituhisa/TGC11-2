using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public GameObject Canvas;
    bool flg;
    float gamepadX = 0;
    float waitTime = 0;

    // Use this for initialization
    void Start()
    {
        flg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flg == false)
        {
            gamepadX = Input.GetAxis("Horizontal");
            Debug.Log(gamepadX);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || gamepadX == -1 && flg == false)
        {
            if (Canvas.transform.position.x < 0)
            {
                Canvas.transform.position += new Vector3(6.5f, 0, 0);
                flg = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || gamepadX == 1 && flg == false)
        {
            if (Canvas.transform.position.x > -13)
            {
                Canvas.transform.Translate(-6.5f, 0, 0);
                flg = true;
            }
        }

        if(flg == true)
        {
            waitTime += Time.deltaTime;
            if (waitTime > 0.1f)
            {
                flg = false;
                waitTime = 0;
            }
        }
    }
}
