using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_botton2 : MonoBehaviour
{
    public float growlevel = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        if (serial.conect == true)
        {
            float x = float.Parse(serial.x);
            float z = float.Parse(serial.z);
            if (x <= growlevel &&z <= growlevel ) //スペースキー押した場合
            {
                SceneManager.LoadScene("OpenCampus");//some_senseiシーンをロードする
                Debug.Log("space");

            }
        }
    }
}
