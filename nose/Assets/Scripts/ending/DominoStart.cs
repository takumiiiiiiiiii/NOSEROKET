using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoStart : MonoBehaviour
{
    private Vector2 thispos;
    public static bool end_start=false;
    private float x_before = 10;
    private float z_before = 10;
    [SerializeField] private Rigidbody2D RB;
    // Start is called before the first frame update
    void Start()
    {
        end_start = false;
        RB = GetComponent<Rigidbody2D>();
        RB.gravityScale = 0;
        thispos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
            
            if (x_before <  Nosemove.growlevel && z_before < Nosemove.growlevel)
            {
                if (x >= Nosemove.growlevel || z >= Nosemove.growlevel)
                {
                    RB.gravityScale = 100;
                    end_start = true;
                }
            }
            x_before = x;
            z_before = z;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            RB.gravityScale = 100;
            end_start = true;
        }
    }
}
