using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RightKyori : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI leftText;
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
        leftText.text = string.Format("Right:"+"{0}", serial.z);
    }
}
