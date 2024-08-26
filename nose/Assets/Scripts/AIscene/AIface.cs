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
    public Sprite changeChargeSprite1;
    public Sprite changeChargeSprite2;
    public Sprite changeChargeSprite3;
    [HideInInspector] public static bool DoNotMove = false;
    [HideInInspector] public static bool Nose_Dush = false;
    [HideInInspector] public static bool Nose_Charge = false;
    private float x_before = 10;
    private float z_before = 10;
    private float charge_time=0f;
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
                        MainSpriteRenderer.sprite = changeDashSprite;
                        Debug.Log("iff");
                        //transform.position += transform.rotation * new Vector2(0, speed * dash_speed);//ダッシュ
                    }
                    else if (x >= Nosemove.growlevel || z >= Nosemove.growlevel)
                    {

                        if (Nose_Dush == true)
                        {
                            
                        }
                        else
                        {
                            //transform.position += transform.rotation * new Vector2(0, speed);//通常の移動
                        }
                    }
                    if (x_before < Nosemove.growlevel && z_before < Nosemove.growlevel)
                    {
                        if (x >= Nosemove.growlevel || z >= Nosemove.growlevel)
                        {
                            StartCoroutine(Dash());
                        }
                    }
                    x_before = x;
                    z_before = z;
                }
            }
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
            else if (Input.GetKey(KeyCode.Space) && Nose_Dush == false)
            {
                MainSpriteRenderer.sprite = changeChargeSprite;
                charge_time += Time.deltaTime;
                Debug.Log(charge_time);
                if (charge_time < 0.25f)
                {
                    MainSpriteRenderer.sprite = changeChargeSprite;
                    Debug.Log("短く押された: " + charge_time+ " 秒");
                    // 短く押された場合の反応を記述
                }
                else if (charge_time >= 0.25f && charge_time < 0.5f)
                {
                    MainSpriteRenderer.sprite = changeChargeSprite1;
                    Debug.Log("中くらいの時間押された: " +charge_time + " 秒");
                    // 中くらいの時間押された場合の反応を記述
                }
                else if (charge_time >= 0.5f && charge_time < 0.75f)
                {
                    MainSpriteRenderer.sprite = changeChargeSprite2;
                    
                    // 長く押された場合の反応を記述
                }
                else
                {
                    MainSpriteRenderer.sprite = changeChargeSprite3;
                }
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
        
    }
    private IEnumerator Dash()
    {
        charge_time = 0f;
        Nose_Dush = true;
        yield return new WaitForSeconds(1);//1秒後にダッシュ終わり
        Nose_Dush = false;
        Nose_Charge = false;
    }
}
