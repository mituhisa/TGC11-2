using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{

    public float speed = 1.0f;
    public string SelectScene;
    private float time;
    private float TuchFlg;
    private SpriteRenderer image;

    // Use this for initialization
    void Start()
    {
        image = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        image.color = GetAlphaColor(image.color);
        if(TuchFlg == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SelectScene);
            }
        }
    }

    Color GetAlphaColor(Color color)
    {
        if (TuchFlg == 1)
        {
            time += Time.deltaTime * 5.0f * speed;
            color.a = Mathf.Sin(time) * 0.5f + 0.5f;
        }
        else
        {
            color.a = 255;
        }
        return color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TuchFlg = 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TuchFlg = 0;
    }
}
