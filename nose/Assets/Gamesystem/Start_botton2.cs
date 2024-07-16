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
        Serial seria;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        seria = objc.GetComponent<Serial>();//スクリプトを取得
        if (seria.connect_char == true&&seria.x.Length < 6 && seria.z.Length < 6)
        {
            
            float x = float.Parse(seria.x);
            float z = float.Parse(seria.z);
            if (x <= growlevel &&z <= growlevel ) //スペースキー押した場合
            {
                seria.serial.Close();
                SceneManager.LoadScene("OpenCampus");//some_senseiシーンをロードする
                Debug.Log("space");

            }
        }
    }
}
