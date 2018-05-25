using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float speed = 8f;           //移動速度
    public float movebleRange = 23f;   //可動範囲
    public Transform SettingCanvas;    //テキスト用変数
    float downspeed;                   //落下速度
    Rigidbody2D rb;                    //プレイヤー移動用変数
    Collider2D cb;                     //isTrigger用変数
    GameObject Item;                   //アイテム情報格納変数
    GameObject SetItem;
    string tagnam;                     //アイテムのタグ名格納変数
    Vector2 Itmpos;                    //アイテム座標
    Vector2 nowpos;                    //プレイヤー座標
    float rot;                         //アイテム回転 
    public float Vcnt;                        //Vキーカウント変数
    float vectol;                      //アイテム貼り付け方向
    bool Setteing;                     //アイテム設置中フラグ
    public Item ItemScp;               //アイテムスクリプト変数
    float copyFlg;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        cb = GetComponent<Collider2D>();
        downspeed = 0;
        Vcnt = 0;
        vectol = 0;
        Setteing = false;
        SettingCanvas.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        //プレイヤー移動操作
        if (Setteing == false)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position + new Vector3(-0.45f, -0.55f), Vector2.right, 0.9f);
            if (hit.transform != null)
            {
                downspeed = 0;
                if (Input.GetButtonDown("Jump"))
                {
                    downspeed += 10.0f;
                    transform.Translate(Vector3.up * 0.01f);
                }
            }
            else
            {
                downspeed += -0.3f;
            }
            nowpos = rb.position;
            nowpos += new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, downspeed * Time.deltaTime);
            rb.MovePosition(nowpos);
            if (Input.GetKeyDown(KeyCode.LeftArrow)) vectol = -2;
            if (Input.GetKeyDown(KeyCode.RightArrow)) vectol = 2;


            //切り取り＆貼り付け操作
            if (Input.GetKeyDown(KeyCode.C) && Item.gameObject.GetComponent<Item>().SetCnt == 0)
            {
                if (SetItem != null) Destroy(SetItem);
                tagnam = Item.gameObject.tag;
                SetItem = Item;
                SetItem.gameObject.SetActive(false);
                ItemScp = SetItem.gameObject.GetComponent<Item>();
                Item = null;
                
            }
        }
        if (SetItem != null && Input.GetKeyDown(KeyCode.V) && SetItem.gameObject.GetComponent<Item>().SetCnt == 0)
        {
            SettingCanvas.gameObject.SetActive(true);
            Vcnt = 1;
            Setteing = true;
            SetItem.gameObject.SetActive(true);
            GameObject.Find(tagnam).transform.position = new Vector2(nowpos.x + vectol, nowpos.y);
            Itmpos = GameObject.Find(tagnam).transform.position;
        }
        if(Vcnt == 1)
        {
            //Vキーを押してる間アイテムの角度＆位置調整
            cb.isTrigger = true;
            Itmpos += new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
            GameObject.Find(tagnam).transform.position = Itmpos;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rot += 10;
                if (rot > 350) rot = 0;
            }
            SetItem.transform.eulerAngles = new Vector3(0, 0, rot);

            if (Input.GetKeyUp(KeyCode.V))  //Vキーを離したら調整終了
            {
                SettingCanvas.gameObject.SetActive(false);
                Setteing = false;
                cb.isTrigger = false;
                ItemScp.SetCnt = 1;
                SetItem = null;
                Vcnt = 0;
                rot = 0;
            }           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Obje" && collision.gameObject.tag != "trap")
        {
            Item = collision.gameObject;

            Debug.Log("Tuch");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!Input.GetKeyDown(KeyCode.C))
        {
            Item = null;
            Debug.Log("Nothing");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "trap") SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnBecameInvisible()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Respown");

    }
}