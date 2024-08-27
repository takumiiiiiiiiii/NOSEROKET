using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blackout : MonoBehaviour
{
    private float x_before = 10;
    private float z_before = 10;
    private bool anime_start = false;
    private SpriteRenderer objRenderer;
    private float alpha;
    private float add_spd = -11.0f;
    public string SeneName="tutorial";
    // Start is called before the first frame update
    void Start()
    {
        objRenderer = GetComponent<SpriteRenderer>();
        alpha = 0.0f;
        SetTransparency(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
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
                }
            }
            x_before = x;
            z_before = z;
        }
        if (Input.GetKeyUp(KeyCode.Space)) //スペースキー押した場合
        {
            //SceneManager.LoadScene("SampleScene");//some_senseiシーンをロードする
            anime_start = true;
            Debug.Log("space");

        }
        //Bout();
    }
    void FixedUpdate()
    {
        Debug.Log("mっ耳耳み");
        Bout();
    }
        public void SetTransparency(float alpha)
    {
        UnityEngine.Color color = objRenderer.material.color;
        color.a = alpha;
        objRenderer.material.color = color;
    }

    void Bout()
    {
        if (anime_start == true)
        {
            add_spd += 0.1f;
            transform.position += new Vector3(0, 10 + add_spd, 0) * Time.deltaTime;
            if (transform.position.y > 50)
            {
 
                alpha += 0.01f;
                SetTransparency(alpha);

                if (alpha > 1)
                {

                    SceneManager.LoadScene(SeneName);

                }
            }
        }
    }
}
