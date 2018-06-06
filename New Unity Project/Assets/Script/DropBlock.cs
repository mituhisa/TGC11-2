using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBlock : MonoBehaviour {

    Vector3 BlockPos;
    Rigidbody2D Blockrb;
    bool tuchflg;
    float setcnt;
    // Use this for initialization
    void Start()
    {
        Blockrb = this.gameObject.GetComponent<Rigidbody2D>();
        tuchflg = true;
        setcnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (tuchflg == true)
        {
            if (this.gameObject.transform.position.y > -3.5f)
            {
                BlockPos = Blockrb.position;
                BlockPos += new Vector3(0, -0.05f, 0);
                Blockrb.MovePosition(BlockPos);
            }
            else
            {
                this.gameObject.transform.position = new Vector3(transform.position.x, -3.5f, 0);
            }
        }
        if (tuchflg == false)
        {
            if (setcnt == 1)
            {

            }
            else if (setcnt == 2)
            {
                BlockPos = Blockrb.position;
                BlockPos += new Vector3(0, -0.1f, 0);
                Blockrb.MovePosition(BlockPos);
                if (gameObject.transform.position.y < -5.5f)
                {
                    transform.position = new Vector3(transform.position.x, 5.5f, 0);
                    tuchflg = true;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        setcnt++;
        if (gameObject.transform.position.y <= -3.5f)
        {
            tuchflg = false;
        }
    }
}
