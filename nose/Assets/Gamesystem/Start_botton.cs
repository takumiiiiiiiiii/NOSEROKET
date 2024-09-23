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
    AudioSource audiosorce;
    public AudioClip gonose;
    public AudioClip gonoseVoice;
    public Animator anima;
    private float x_bef = 10000;
    private float z_bef = 10000;
    private bool anime_start = false;
    private float add_spd = -11.0f;  
    // Start is called before the first frame update
    void Start()
    {
        anima= gameObject.GetComponent<Animator>();
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        audiosorce = GetComponent<AudioSource>();
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
            if (x < Nosemove.growlevel && z < Nosemove.growlevel)
            {
                anima.SetBool("Charge", true);
            }
            if (x_bef < Nosemove.growlevel && z_bef < Nosemove.growlevel)
            {
                if (x >= Nosemove.growlevel || z >= Nosemove.growlevel)
                {
                    anima.SetBool("Charge",false);
                    audiosorce.PlayOneShot(gonose);
                    audiosorce.PlayOneShot(gonoseVoice);
                    anime_start = true;
                    MainSpriteRenderer.sprite = changeSprite;
                }
            }
            x_bef = x;
            z_bef = z;
        }
        if (Input.GetKeyDown(KeyCode.Space)) //スペースキーをおした場合
        {
            anima.SetBool("Charge", true);
        }
            if (Input.GetKeyUp(KeyCode.Space)) //スペースキーを離した場合
        {
            //SceneManager.LoadScene("SampleScene");//some_senseiシーンをロードする
            anima.SetBool("Charge", false);
            anime_start = true;
            MainSpriteRenderer.sprite = changeSprite;
            audiosorce.PlayOneShot(gonose);
            audiosorce.PlayOneShot(gonoseVoice);
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
