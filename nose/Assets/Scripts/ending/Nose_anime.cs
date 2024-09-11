using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nose_anime : MonoBehaviour
{
    public Animator anima;
    private float x_bef = 10000f, z_bef = 10000f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("xbfore" + x_bef);
        anima = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anima.SetBool("Left_anima", false);
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        float x, z;
        if (serial.conect && float.TryParse(serial.x, out x) && float.TryParse(serial.z, out z))
        {
            if (x < Nosemove.growlevel && z < Nosemove.growlevel)
            {
                Debug.Log("fafafa");
                anima.SetBool("Big_anima",true);
            }
            if (x_bef < Nosemove.growlevel && z_bef < Nosemove.growlevel)
            {
                if (x >= Nosemove.growlevel || z >= Nosemove.growlevel)
                {
                    anima.SetBool("Puff_anima", true);
                }
            }
            x_bef = x;
            z_bef = z;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anima.SetBool("Big_anima", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anima.SetBool("Puff_anima", true);
        }
    }
}
