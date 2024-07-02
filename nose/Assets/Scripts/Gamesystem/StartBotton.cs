using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBotton : MonoBehaviour
{
    public static bool start = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //スペースキー押した場合
        {
            //SceneManager.LoadScene("Matome_Scene");//some_senseiシーンをロードする
            start = true;
            Debug.Log("space");

        }
    }
}
