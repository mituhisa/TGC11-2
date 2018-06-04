using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    public GameObject prefab;
    public GameObject marker;
    float interval;
    float time;
    float height;
    float width;
    public bool senFlg;
    Vector3 position;
    Quaternion rota;

    // Use this for initialization
    void Start()
    {
        interval = 0.01f;
        time = 0;
        position = new Vector3(-3.8f, -2.5f, 0);
        rota = new Quaternion(0, 0, 0, 0);
        height = 0;
        width = 0;
        senFlg = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (senFlg == false)
        {
            if (position.y < 2.5f && height == 0)
            {
                width = 0;
                time += Time.deltaTime;
                if (time > interval)
                {
                    GameObject sen = Instantiate(prefab, position, rota) as GameObject;
                    position += new Vector3(0, 0.5f, 0);
                    sen.transform.parent = this.transform;
                    time = 0;
                }
            }
            else if (position.x < 3.8f && width == 0)
            {
                height = 1;
                rota = Quaternion.Euler(0, 0, 90);
                time += Time.deltaTime;
                if (time > interval)
                {
                    GameObject sen = Instantiate(prefab, position, rota) as GameObject;
                    sen.transform.position += new Vector3(0.2f, -0.35f, 0);
                    position += new Vector3(0.5f, 0, 0);
                    sen.transform.parent = this.transform;
                    time = 0;
                }
            }
            else if (position.y > -2.5f && height == 1)
            {
                width = 1;
                rota = Quaternion.Euler(0, 0, 0);
                time += Time.deltaTime;
                if (time > interval)
                {
                    GameObject sen = Instantiate(prefab, position, rota) as GameObject;
                    sen.transform.position += new Vector3(0, -0.5f, 0);
                    position += new Vector3(0, -0.5f, 0);
                    sen.transform.parent = this.transform;
                    time = 0;
                }
            }
            else if (position.x > -3.8f && width == 1)
            {
                rota = Quaternion.Euler(0, 0, 90);
                time += Time.deltaTime;
                if (time > interval)
                {
                    GameObject sen = Instantiate(prefab, position, rota) as GameObject;
                    sen.transform.position += new Vector3(-0.2f, -0.15f, 0);
                    position += new Vector3(-0.5f, 0, 0);
                    sen.transform.parent = this.transform;
                    time = 0;
                }
            }
            else
            {
                foreach (Transform n in gameObject.transform)
                {
                    GameObject.Destroy(n.gameObject);
                }
                height = 0;
                marker.SetActive(true);
                senFlg = true;
                rota = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
