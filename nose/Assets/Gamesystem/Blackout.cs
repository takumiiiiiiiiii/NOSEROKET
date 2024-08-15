using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blackout : MonoBehaviour
{
    private bool anime_start = false;
    private SpriteRenderer objRenderer;
    private float alpha;
    private float add_spd = -11.0f;
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
        if (Input.GetKeyDown(KeyCode.Space)) //スペースキー押した場合
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
                    SceneManager.LoadScene("tutorial");
                }
            }
        }
    }
}
