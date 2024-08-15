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
