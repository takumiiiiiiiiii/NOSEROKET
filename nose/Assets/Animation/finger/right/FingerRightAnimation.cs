using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerRightAnimation : MonoBehaviour
{
    public Animator anima;
    [HideInInspector] public static bool DoNotMove = false;
    [HideInInspector] public static bool Nose_Dush = false;
    [HideInInspector] public static bool Nose_Charge = false;
    private float x_before = 10;
    private float z_before = 10;
    private float charge_time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        anima= gameObject.GetComponent<Animator>();
        DoNotMove = false;
        Nose_Dush = false;
        Nose_Charge = false;
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
                Debug.Log("X:" + x);
                anima.SetBool("FingerIN", false);
                if (DoNotMove == false)
                {
                    if (z < Nosemove.growlevel && x >= Nosemove.growlevel && Nose_Dush == false && Nose_Charge == false)//左に指を入れる
                    {
                        anima.SetBool("FingerIN", true);
                    }
                    if (z < Nosemove.growlevel && x < Nosemove.growlevel)
                    {
                        charge_time += Time.deltaTime;
                        Debug.Log(charge_time);
                        if (charge_time < 0.25f)
                        {
                            Debug.Log("短く押された: " + charge_time + " 秒");
                            // 短く押された場合の反応を記述
                        }
                        else if (charge_time >= 0.25f && charge_time < 0.5f)
                        {
                            Debug.Log("中くらいの時間押された: " + charge_time + " 秒");
                            // 中くらいの時間押された場合の反応を記述
                        }
                        else if (charge_time >= 0.5f && charge_time < 0.75f)
                        {

                            // 長く押された場合の反応を記述
                        }
                        else
                        {
                        }
                        Nose_Charge = true;
                    }
                    if (x_before < Nosemove.growlevel && z_before < Nosemove.growlevel)
                    {

                        if (x >= Nosemove.growlevel || z >= Nosemove.growlevel)
                        {
                            StartCoroutine(Dash());
                        }
                    }
                    if (Nose_Dush == true)
                    {
                    }
                    x_before = x;
                    z_before = z;
                }
            }
        }
        else
        {
            anima.SetBool("FingerIN", false);
            //アニメーション
            if (Nose_Dush == true)
            {
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.Space) == false && Nose_Charge == false)
            {
                anima.SetBool("FingerIN", true);
            }
            else if (Input.GetKey(KeyCode.Space) && Nose_Dush == false)
            {
                charge_time += Time.deltaTime;
                Debug.Log(charge_time);
                if (charge_time < 0.25f)
                {
                    Debug.Log("短く押された: " + charge_time + " 秒");
                    // 短く押された場合の反応を記述
                }
                else if (charge_time >= 0.25f && charge_time < 0.5f)
                {
                    Debug.Log("中くらいの時間押された: " + charge_time + " 秒");
                    // 中くらいの時間押された場合の反応を記述
                }
                else if (charge_time >= 0.5f && charge_time < 0.75f)
                {

                    // 長く押された場合の反応を記述
                }
                else
                {
                }
                Nose_Charge = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space) && Nose_Dush == false)
            {
                StartCoroutine(Dash());
            }
            else if (Nose_Dush == false && Nose_Charge == false)
            {
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
