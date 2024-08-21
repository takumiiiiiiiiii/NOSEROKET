using System.Collections;
using System.Collections.Generic;
using AIE2D;
using UnityEngine;

public class AIface : MonoBehaviour
{
    SpriteRenderer MainSpriteRenderer;
    public Sprite changeNormalSprite;
    public Sprite changeRightSprite;
    public Sprite changeLeftSprite;
    public Sprite changeDashSprite;
    public Sprite changeChargeSprite;
    [HideInInspector] public static bool DoNotMove = false;
    [HideInInspector] public static bool Nose_Dush = false;
    [HideInInspector] public static bool Nose_Charge = false;
    // Start is called before the first frame update
    void Start()
    {
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        if (serial.connect_char == true)
        {

        }
        else
        {
            //アニメーション
            if (Nose_Dush == true)
            {
                MainSpriteRenderer.sprite = changeDashSprite;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.Space) == false && Nose_Charge == false)
            {
                MainSpriteRenderer.sprite = changeLeftSprite;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.Space) == false && Nose_Charge == false)
            {
                MainSpriteRenderer.sprite = changeRightSprite;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && Nose_Dush == false)
            {
                MainSpriteRenderer.sprite = changeChargeSprite;
                Nose_Charge = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space) && Nose_Dush == false)
            {
                StartCoroutine(Dash());
            }
            else if (Nose_Dush == false && Nose_Charge == false)
            {
                MainSpriteRenderer.sprite = changeNormalSprite;
            }
        }

    }
    void FixedUpdate()
    {
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        if (serial.connect_char == true)
        {
            float x, z;
            if (float.TryParse(serial.x, out x) && float.TryParse(serial.z, out z))//文字を数字に直しつつ変なデータがきたら弾く
            {
                MainSpriteRenderer.sprite = changeNormalSprite;
                Debug.Log("X:" + x);
                if (DoNotMove == false)
                {
                    if (z < Nosemove.growlevel && x >= Nosemove.growlevel)
                    {
                        MainSpriteRenderer.sprite = changeLeftSprite;
                    }
                    if (x < Nosemove.growlevel && z >= Nosemove.growlevel)
                    {
                        MainSpriteRenderer.sprite = changeRightSprite;
                    }

                    if (x >= Nosemove.growlevel && z >= Nosemove.growlevel && Nose_Dush == true)
                    {
                        Debug.Log("iff");
                        //transform.position += transform.rotation * new Vector2(0, speed * dash_speed);//ダッシュ
                    }
                    else if (x >= Nosemove.growlevel || z >= Nosemove.growlevel)
                    {

                        if (Nose_Dush == true)
                        {
                            //transform.position += transform.rotation * new Vector2(0, speed * dash_speed);//ダッシュ
                        }
                        else
                        {
                            //transform.position += transform.rotation * new Vector2(0, speed);//通常の移動
                        }
                    }
                }
            }
        }
    }
    private IEnumerator Dash()
    {
        Nose_Dush = true;
        yield return new WaitForSeconds(1);//1秒後にダッシュ終わり
        Nose_Dush = false;
        Nose_Charge = false;
    }
}
