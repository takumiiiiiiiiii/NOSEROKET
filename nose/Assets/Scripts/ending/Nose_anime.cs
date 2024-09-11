using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nose_anime : MonoBehaviour
{
    public Animator anima;
    [HideInInspector] private float x_before = 10000f, z_before = 10000f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("xbfore" + x_before);
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
                anima.SetBool("Big_anima",true);
            }
            if (x_before < Nosemove.growlevel && z_before < Nosemove.growlevel)
            {
                if (x >= Nosemove.growlevel || z >= Nosemove.growlevel)
                {
                    anima.SetBool("Puff_anima", true);
                }
            }
            x_before = x;
            z_before = z;
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
