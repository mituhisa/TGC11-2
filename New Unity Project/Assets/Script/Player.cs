using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    [SerializeField] private GameObject PlayerCamera;
    [SerializeField] private GameObject MapCamera;

    public float speed = 8f;           //移動速度
    public float movebleRange = 23f;   //可動範囲
    public float Vcnt;                 //Vキーカウント変数
    float vectol;                      //アイテム貼り付け方向
    float rot;                         //アイテム回転 
    float downspeed;                   //落下速度 
    float copyFlg;
    float motionTime;

    public Transform SettingCanvas;    //テキスト用変数
    public GameObject hevenstime;
    public GameObject Pouse;
    public Item ItemScp;               //アイテムスクリプト変数

    Animator animator;
    Rigidbody2D rb;                    //プレイヤー移動用変数
    RaycastHit2D hit;
    Collider2D cb;                     //isTrigger用変数
    GameObject Item;                   //アイテム情報格納変数
    GameObject SetItem;

    Vector3 scale;
    Vector2 Itmpos;                    //アイテム座標
    Vector2 nowpos;                    //プレイヤー座標

    bool Setteing;                     //アイテム設置中フラグ
    bool PouseFlg;
    public string NextStage;
    string tagnam;                     //アイテムのタグ名格納変数
    
  

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        cb = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        downspeed = 0;
        Vcnt = 0;
        vectol = 0;
        motionTime = 0;
        scale = transform.localScale;
        scale.x = -1;
        transform.localScale = scale;
        Setteing = false;
        PouseFlg = false;
        SettingCanvas.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        hit = Physics2D.Raycast(transform.position + new Vector3(-0.225f, -0.65f), Vector2.right, 0.45f);
        //プレイヤー移動操作
        if (Setteing == false && PouseFlg == false)
        {
            //地面当たり判定
            if (hit.transform != null)  //地面の上にいるなら
            {
                downspeed = 0;
                animator.SetBool("stay", true);
                if (Input.GetButtonDown("Jump"))
                {
                    downspeed += 11.0f;
                    transform.Translate(Vector3.up * 0.01f);
                }
            }
            else   //空中にいるなら
            {
                animator.SetBool("stay", false);
                animator.SetBool("move", false);
                downspeed += -0.3f;
            }

            animator.SetFloat("downspeed", downspeed);
            float gamepadX = Input.GetAxis("Horizontal");

            //右移動
            if (Input.GetKey(KeyCode.RightArrow) || gamepadX == 1)
            {
                scale.x = -1;
                transform.localScale = scale;
                animator.SetBool("move",true);
                animator.SetBool("copy", false);
                if (Input.GetButtonDown("Jump"))
                {
                    animator.SetBool("move", false);
                }
            }
            //左移動
            else if (Input.GetKey(KeyCode.LeftArrow) || gamepadX == -1)
            {
                scale.x = 1;
                transform.localScale = scale;
                animator.SetBool("move",true);
                animator.SetBool("copy", false);
                if (Input.GetButtonDown("Jump"))
                {
                    animator.SetBool("move", false);
                }
            }
            //立ち止まっている
            else
            {
                animator.SetBool("move",false);
            }
            //移動反映処理
            nowpos = rb.position;
            nowpos += new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, downspeed * Time.deltaTime);
            rb.MovePosition(nowpos);
            //横移動制限
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -9, 40), transform.position.y);
            //リスポーン
            if (transform.position.y < -9.0f) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            

            if (Input.GetKeyDown(KeyCode.LeftArrow)) vectol = -2;
            if (Input.GetKeyDown(KeyCode.RightArrow)) vectol = 2;

            if(Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("Copy") && gamepadX == 0)
            {
                animator.SetBool("copy", true);
            }
            else if(Input.GetKeyUp(KeyCode.C) || Input.GetButtonUp("Copy"))
            {
                animator.SetBool("copy", false);
            }

            //切り取り＆貼り付け操作
            if (Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("Copy") && Item.gameObject.GetComponent<Item>().SetCnt == 0)
            {
                if (SetItem != null) Destroy(SetItem);
                tagnam = Item.gameObject.tag;
                SetItem = Item;
                SetItem.gameObject.SetActive(false);
                ItemScp = SetItem.gameObject.GetComponent<Item>();
                Item = null;
            }

            if (Input.GetButtonDown("Pouse"))
            {
                PouseFlg = true;
                Pouse.SetActive(true);
                PlayerCamera.SetActive(!PlayerCamera.activeSelf);
                MapCamera.SetActive(!MapCamera.activeSelf);
            }

        }

        if (PouseFlg == false)
        {
            if (SetItem != null && Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Paste") && SetItem.gameObject.GetComponent<Item>().SetCnt == 0)
            {
                animator.SetBool("copy", true);
                if (hit.transform != null)
                {
                    SettingCanvas.gameObject.SetActive(true);
                }
                else
                {
                    hevenstime.gameObject.SetActive(true);
                }
                Vcnt = 1;
                Setteing = true;
                SetItem.gameObject.SetActive(true);
                GameObject.Find(tagnam).transform.position = new Vector2(nowpos.x + vectol, nowpos.y);
                Itmpos = GameObject.Find(tagnam).transform.position;
            }
            if (Vcnt == 1)
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

                if (Input.GetKeyUp(KeyCode.V) || Input.GetButtonUp("Paste"))  //Vキーを離したら調整終了
                {
                    animator.SetBool("copy", false);
                    SettingCanvas.gameObject.SetActive(false);
                    hevenstime.gameObject.SetActive(false);
                    Setteing = false;
                    cb.isTrigger = false;
                    ItemScp.SetCnt = 1;
                    SetItem = null;
                    Vcnt = 0;
                    rot = 0;
                }
            }
        }

        if(PouseFlg == true)
        {
            GameObject yazirusi = GameObject.Find("yazirusi") ;
            yazirusi.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            float gamepadY = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.UpArrow) || gamepadY == 1)
            {
                if (yazirusi.transform.position.y < 5)
                {
                    yazirusi.transform.position = new Vector3(yazirusi.transform.position.x, 5, 0);
                }
            }
            if(Input.GetKeyDown(KeyCode.DownArrow) || gamepadY == -1)
            {
                if(yazirusi.transform.position.y > 2)
                {
                    yazirusi.transform.position = new Vector3(yazirusi.transform.position.x, 2, 0);
                }
            }
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Copy"))
            {
                if (yazirusi.transform.position.y == 5)
                {
                    PouseFlg = false;
                    Pouse.SetActive(false);
                    yazirusi.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    PlayerCamera.SetActive(!PlayerCamera.activeSelf);
                    MapCamera.SetActive(!MapCamera.activeSelf);
                }
                    if (yazirusi.transform.position.y == 2) SceneManager.LoadScene("Home");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Obje" && collision.gameObject.tag != "trap" && collision.gameObject.tag != "Finish")
        {
            Item = collision.gameObject;

            Debug.Log("Tuch");
        }

        if (collision.gameObject.tag == "Finish") SceneManager.LoadScene(NextStage);
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

    //private void OnBecameInvisible()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //    Debug.Log("Respown");

    //}
}