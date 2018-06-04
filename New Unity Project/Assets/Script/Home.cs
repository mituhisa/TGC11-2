using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour {

    Rigidbody2D rb;
    Rigidbody2D cam;
    Vector2 nowpos;
    Vector3 campos;
    public Transform title;
    public Transform HomeCanvas;
    public Transform StageCanvas;
    public Transform yazirusi;
    public GameObject SelectCamera;
    public Transform Marker;
    float MoveCamX;
    float SelectFlg;


	// Use this for initialization
	void Start () {
        rb = title.GetComponent<Rigidbody2D>();
        cam = SelectCamera.GetComponent<Rigidbody2D>();
        HomeCanvas.gameObject.SetActive(false);
        yazirusi.gameObject.SetActive(false);
        Marker.gameObject.SetActive(false);
        MoveCamX = 0;
        SelectFlg = 0;
	}

    // Update is called once per frame
    void Update() {
        if (title.transform.position.y < 3)
        {
            nowpos = rb.position;
            nowpos += new Vector2(0, 0.05f);
            rb.MovePosition(nowpos);
        }
        if(title.transform.position.y >= 3 && SelectFlg == 0)
        {
            HomeCanvas.gameObject.SetActive(true);
            yazirusi.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (yazirusi.transform.position.y < 0)
                {
                    yazirusi.transform.Translate(-1.3f, 1.1f, 0);
                }
                else
                {
                    yazirusi.transform.Translate(1.3f, -1.1f, 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (yazirusi.transform.position.y > -1)
                {
                    yazirusi.transform.Translate(1.3f, -1.1f, 0);
                }
                else
                {
                    yazirusi.transform.Translate(-1.3f, 1.1f, 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            { 
                if(yazirusi.transform.position.y == 0)
                {
                    MoveCamX = 1;
                }
                else
                {
                    SceneManager.LoadScene("End");
                }
            }
            if(SelectCamera.transform.position.x < 24)
            {
                campos = cam.position;
                campos += new Vector3(MoveCamX, 0, 0);
                cam.MovePosition(campos);
            }
            else
            {
                SelectFlg = 1;
                MoveCamX = 0;
                Marker.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if(SelectFlg == 1)
        {
            Marker.gameObject.SetActive(true);
            //マーカー上移動
            if (Input.GetKeyDown(KeyCode.UpArrow) && Marker.transform.position.y < 2.05f && Marker.transform.position.x >= 17.2f)
            {
                Marker.transform.Translate(0, 3.1f, 0);
            }else if (Input.GetKeyDown(KeyCode.UpArrow) && Marker.transform.position.y == 2.05f && Marker.transform.position.x >= 17.2f)
            {
                if (StageCanvas.transform.position.y > 0)
                {
                    StageCanvas.transform.Translate(0, -3.1f, 0);
                }
            }
            //マーカー左移動
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Marker.transform.position.x > 17.2f)
            {
                Marker.transform.Translate(-7.2f, 0, 0);
            }else if(Input.GetKeyDown(KeyCode.LeftArrow) && Marker.transform.position.x == 17.2f)
            {
                Marker.transform.Translate(-7.2f, 0, 0);
                Marker.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            //マーカー下移動
            if (Input.GetKeyDown(KeyCode.DownArrow) && Marker.transform.position.y > -2.05f && Marker.transform.position.x >= 17.2f)
            {
                Marker.transform.Translate(0, -3.1f, 0);
            }else if(Input.GetKeyDown(KeyCode.DownArrow) && Marker.transform.position.y == -2.05f && Marker.transform.position.x >= 17.2f)
            {
                if (StageCanvas.transform.position.y < 8.2f)
                {
                    StageCanvas.transform.Translate(0, 3.1f, 0);
                }
            }
            //マーカー右移動
            if (Input.GetKeyDown(KeyCode.RightArrow) && Marker.transform.position.x < 31.6f)
            {
                Marker.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                Marker.transform.Translate(7.2f, 0, 0);
            }

            if(Marker.transform.position.x < 17.2f && Input.GetKeyDown(KeyCode.Space))
            {
                MoveCamX = -1;
                Marker.transform.Translate(7.2f, 0, 0);
            }

            if(SelectCamera.transform.position.x > 0)
            {
                campos = cam.position;
                campos += new Vector3(MoveCamX, 0, 0);
                cam.MovePosition(campos);
            }
            else
            {
                MoveCamX = 0;
                SelectFlg = 0;
                Marker.gameObject.SetActive(false);
            }
        }
    }
}
