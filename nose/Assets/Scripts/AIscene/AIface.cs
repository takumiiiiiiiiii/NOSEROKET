using System.Collections;
using System.Collections.Generic;
using AIE2D;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AIface : MonoBehaviour
{
    public Rigidbody2D RB;
    public Animator anima;
    public float dash_speed = 2;//ダッシュ時のスピード
    SpriteRenderer MainSpriteRenderer;
    public Sprite changeNormalSprite;
    public Sprite changeRightSprite;
    public Sprite changeLeftSprite;
    public Sprite changeDashSprite;
    public Sprite changeChargeSprite;


    public float growlevel = 0.3f;
    private Vector2 forword;

    [HideInInspector] public static bool DoNotMove = false;
    [HideInInspector] public static bool Nose_Dush = false;
    [HideInInspector] public static Transform myTransform;
    [HideInInspector] private float x_before = 10f, z_before = 10f;
    // Start is called before the first frame update
    void Start()
    {
        anima = gameObject.GetComponent<Animator>();
        forword = transform.forward;
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        //serial = objc.GetComponent<Serial>();//スクリプトを取得
        anima.SetBool("Left_anima", false);
        anima.SetBool("Right_anima", false);
        /*
        if (serial.connect_char == true)
        {
            float x, z;
            if (float.TryParse(serial.x, out x) && float.TryParse(serial.z, out z))
            {

                if (x_before < growlevel && z_before < growlevel && Nose_Dush == false)
                {
                    if (x >= growlevel || z >= growlevel)
                    {
                        
                    }
                }
                if (Nose_Dush == true)
                {
                    
                }
                else
                {
                    
                }
                x_before = x;
                z_before = z;
                if (x < growlevel && z >= growlevel)
                {

                    anima.SetBool("Right_anima", true);
                }
                if (z < growlevel && x >= growlevel)
                {
                    anima.SetBool("Left_anima", true);
                }
            }

        }
        else*/
        {
            //アニメーション
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.Space) == false)
            {
                anima.SetBool("Left_anima", true);
                MainSpriteRenderer.sprite = changeLeftSprite;
            }
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.Space) == false)
            {
                anima.SetBool("Right_anima", true);
                MainSpriteRenderer.sprite = changeRightSprite;
            }
            if (Input.GetKeyUp(KeyCode.Space) && Nose_Dush == false)
            {
                
                StartCoroutine(Dash());
            }
            if (Nose_Dush == true)
            {
                MainSpriteRenderer.sprite = changeDashSprite;
            }
            else if(Nose_Dush == false)
            {
                MainSpriteRenderer.sprite = changeChargeSprite;
            }
            else
            {
                MainSpriteRenderer.sprite = changeNormalSprite;
            }
        }

    }
    private IEnumerator Dash()
    {
        Nose_Dush = true;
        yield return new WaitForSeconds(1);//1秒後にダッシュ終わり
        Nose_Dush = false;
    }
}
