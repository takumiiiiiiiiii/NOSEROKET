using System.Collections;
using System.Collections.Generic;
using AIE2D;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    private IEnumerator Dash()
    {
        Nose_Dush = true;
        yield return new WaitForSeconds(1);//1秒後にダッシュ終わり
        Nose_Dush = false;
        Nose_Charge = false;
    }
}
