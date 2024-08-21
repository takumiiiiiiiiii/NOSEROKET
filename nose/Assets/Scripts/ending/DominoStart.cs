using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoStart : MonoBehaviour
{
    private Vector2 thispos;
    public static bool end_start=false;
    private float growlevel = 0.3f;
    private float x_before = 0;
    private float z_before = 0;
    [SerializeField] private Rigidbody2D RB;

    AudioSource audiosorce;
    public AudioClip Domino_start;
    // Start is called before the first frame update
    void Start()
    {
        end_start = false;
        RB = GetComponent<Rigidbody2D>();
        RB.gravityScale = 0;
        thispos = this.transform.position;
        audiosorce = GetComponent<AudioSource>();
        
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
            
            if (x_before < growlevel && z_before < growlevel)
            {
                if (x >= growlevel || z >= growlevel)
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
            audiosorce.PlayOneShot(Domino_start);
            RB.gravityScale = 100;
            end_start = true;
        }


    }
}
