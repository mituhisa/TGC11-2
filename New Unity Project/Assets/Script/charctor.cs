using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class charctor : MonoBehaviour {

    public enum Mode
    {
        Kick,
        Run,
        Stay,
        Eiei,
        Copy,
    };

    Animator animator;
    Rigidbody2D rb;
    Vector3 startpos;
    Vector3 nowpos;
    Vector3 scale;
    Vector3 CreatePoint;
    public GameObject Block;
    public GameObject title;
    public GameObject panel;
    public GameObject menu;
    public float waitTime;
    float createTime;
    public static bool SceneChangeFlg = false;
    public Mode mode;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startpos = gameObject.transform.position;
        scale = gameObject.transform.localScale;
        CreatePoint = new Vector3(9.5f, gameObject.transform.position.y, 0);
        scale.x = -1;
        transform.localScale = scale;
        gameObject.transform.position = new Vector3(-11.5f, -0.8f, 0);
        mode = Mode.Kick;
        waitTime = 0;

        if(SceneManager.GetActiveScene().name == "Home")
        {
            title.SetActive(true);
            panel.SetActive(false);
            menu.SetActive(true);
            scale.x = -1;
            transform.localScale = scale;
            gameObject.transform.position = new Vector3(-10.5f, startpos.y, 0);
            animator.SetTrigger("move");
            mode = Mode.Stay;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name != "Home")
        { 
            SceneManager.LoadScene("Home");
        }
        switch (mode)
        {
            case Mode.Kick:
                waitTime += Time.deltaTime;
                if(waitTime > 2.0f)
                {
                    animator.SetTrigger("kick");
                    rb.AddForce(transform.right * 50.0f);
                }
                if(transform.position.x > 70.0f)
                {
                    scale.x = 1;
                    transform.localScale = scale;
                    rb.velocity = Vector2.zero;
                    gameObject.transform.position = new Vector3(10.5f, -2.36f, 0);
                    waitTime = 0;
                    GameObject.Find("Controller").GetComponent<Title>().mode = Title.Mode.Title;
                    mode = Mode.Run;

                }

                break;
            case Mode.Run:
                animator.SetTrigger("move");
                //左方向に走る
                if (nowpos.x > -9.5f && scale.x == 1)
                {
                       nowpos = rb.position;
                       nowpos += new Vector3(-0.05f, 0, 0);
                       rb.MovePosition(nowpos);
                }
                //右方向に走る
                else if (nowpos.x < startpos.x)
                {
                    title.SetActive(true);
                    nowpos = rb.position;
                    nowpos += new Vector3(0.05f, 0, 0);
                    rb.MovePosition(nowpos);
                }
                else
                {
                    panel.transform.position += new Vector3(0.05f, -0.04f, 0);
                    gameObject.transform.position = new Vector3(-9.5f, gameObject.transform.position.y, 0);
                    waitTime += Time.deltaTime;
                    if (waitTime > 4.0f) SceneManager.LoadScene("Home");
                }

                if(transform.position.x > 2.0f && scale.x == -1) panel.transform.position += new Vector3(0.02f, -0.02f, 0);
                //方向転換
                if (nowpos.x < -9.5f)
                {
                       scale.x = -1;
                       transform.localScale = scale;
                }
                //ブロック生成
                if (Mathf.FloorToInt(transform.position.x * 10.0f) / 10.0f < CreatePoint.x && scale.x == 1)
                {
                       GameObject newblock = Instantiate(Block, CreatePoint + new Vector3(-1.0f * scale.x, -1.14f, 0), Quaternion.identity);
                       CreatePoint += new Vector3(-1.0f, 0, 0);
                }
                break;
            case Mode.Stay:
                waitTime += Time.deltaTime;
                if (waitTime > 4.0f)
                {
                    menu.SetActive(true);
                    GameObject.Find("Controller").GetComponent<Title>().mode = Title.Mode.Menu;
                    //４秒後にひょっこり
                    if (gameObject.transform.position.x < -4.5f)
                    {
                        nowpos = rb.position;
                        nowpos += new Vector3(0.1f, 0, 0);
                        rb.MovePosition(nowpos);
                    }
                    else
                    {
                        animator.SetTrigger("stay");
                    }
                }

                if(waitTime > 8.0f)
                {
                    //６秒毎に正拳突き
                    mode = Mode.Eiei;
                    waitTime = 0;
                    animator.SetTrigger("punch");
                }
                break;
            case Mode.Eiei:
                waitTime += Time.deltaTime;
                if(waitTime > 3.0f)
                {
                    waitTime = 0;
                    animator.SetTrigger("stay");
                    mode = Mode.Stay;
                }
                break;
            case Mode.Copy:
                if(GameObject.Find("Line").GetComponent<Line>().senFlg == false)
                {
                    animator.SetTrigger("copy");
                }
                else
                {
                    menu.SetActive(false);
                    title.SetActive(false);
                    animator.SetTrigger("stay");
                }
                break;
        }
    
    }
}
