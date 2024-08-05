using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackout : MonoBehaviour
{
    private bool anime_start = false;
    private SpriteRenderer objRenderer;
    // Start is called before the first frame update
    void Start()
    {
        objRenderer = GetComponent<SpriteRenderer>();
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
            transform.position += new Vector3(0, 10, 0) * Time.deltaTime;
            if (transform.position.y > 75)
            {
                float alpha = Mathf.PingPong(Time.time, 1.0f);
                SetTransparency(alpha);

                if (alpha >= 1)
                {
                    anime_start = false;
                }
            }
        }
    }
}
