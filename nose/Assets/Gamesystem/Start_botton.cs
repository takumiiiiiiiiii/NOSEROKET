using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Drawing;

public class Start_botton : MonoBehaviour
{
    SpriteRenderer MainSpriteRenderer;
    public Sprite changeSprite;

    private float x_before = 10;
    private float z_before = 10;
    private bool anime_start = false;
    private float add_spd = -11.0f;  
    // Start is called before the first frame update
    void Start()
    {
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Nosemove.growlevel);
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        float x, z;
        if (float.TryParse(serial.x, out x) && float.TryParse(serial.z, out z))
        {

            if (x_before < Nosemove.growlevel && z_before < Nosemove.growlevel)
            {
                if (x >= Nosemove.growlevel || z >= Nosemove.growlevel)
                {
                    anime_start = true;
                    MainSpriteRenderer.sprite = changeSprite;
                }
            }
            x_before = x;
            z_before = z;
        }

        if (Input.GetKeyDown(KeyCode.Space)) //スペースキー押した場合
        {
            //SceneManager.LoadScene("SampleScene");//some_senseiシーンをロードする
            anime_start = true;
            MainSpriteRenderer.sprite = changeSprite;
            Debug.Log("space");
        }
    }

    void FixedUpdate()
    {
        AnimeMoveNose();
    }

        void AnimeMoveNose()
    {
        if (anime_start == true)
        {
            add_spd += 0.1f;
            transform.position += new Vector3(0, 10 + add_spd, 0) * Time.deltaTime;
        }
    }
}
