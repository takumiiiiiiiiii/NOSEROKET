using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Drawing;

public class Start_botton : MonoBehaviour
{

    private bool anime_start = false;
    // Start is called before the first frame update
    void Start()
    {

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
        AnimeMoveNose();
    }

    void AnimeMoveNose()
    {
        if (anime_start == true)
        {
            transform.position += new Vector3(0, 10, 0) * Time.deltaTime;
        }
    }
}
