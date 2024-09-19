using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoStart : MonoBehaviour
{
    private Vector2 thispos;
    public static bool end_start=false;
    private float x_bef = 10000;
    private float z_bef = 10000;
    [SerializeField] private Rigidbody2D RB;

    AudioSource audiosorce;
    public AudioClip Domino_start;
    // Start is called before the first frame update
    void Start()
    {
        x_bef = 1000;
        z_bef = 1000;
        end_start = false;
        RB = GetComponent<Rigidbody2D>();
        RB.gravityScale = 0;
        thispos = this.transform.position;
        audiosorce = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("x_before" + x_bef);
        //Debug.Log(end_start);
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        float x, z;
        if (!end_start)
        {
            this.transform.position = thispos;
        }

        if (serial.conect&&float.TryParse(serial.x, out x) && float.TryParse(serial.z, out z))
        {
            if (x_bef < Nosemove.growlevel && z_bef < Nosemove.growlevel)
            {
                if (x >= Nosemove.growlevel || z >= Nosemove.growlevel)
                {
                    Debug.Log("x_before" + x_bef + "x" + x);
                    RB.gravityScale = 100;
                    end_start = true;
                }
            }
            x_bef = x;
            z_bef = z;
            //if (x_before <  Nosemove.growlevel && z_before < Nosemove.growlevel)
            //{
            //    if (x >= Nosemove.growlevel || z >= Nosemove.growlevel)
            //    {
            //        RB.gravityScale = 100;
            //        end_start = true;
            //    }
            //}
            //x_before = x;
            //z_before = z;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            audiosorce.PlayOneShot(Domino_start);
            RB.gravityScale = 100;
            end_start = true;
        }
    }
}
