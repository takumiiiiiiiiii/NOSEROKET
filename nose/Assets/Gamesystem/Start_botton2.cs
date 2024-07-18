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
        float x, z;
        if (float.TryParse(serial.x, out x) && float.TryParse(serial.z, out z))//文字を数字に直しつつ変なデータがきたら弾く
        {
            if (x <= growlevel &&z <= growlevel ) //スペースキー押した場合
            {
                serial.serial.Close();
                SceneManager.LoadScene("OpenCampus");//some_senseiシーンをロードする
                Debug.Log("space");

            }
        }
    }
}
