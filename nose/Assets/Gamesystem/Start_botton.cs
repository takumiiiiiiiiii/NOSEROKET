using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_botton : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //スペースキー押した場合
        {
            SceneManager.LoadScene("Matome_Scene");//some_senseiシーンをロードする
            Debug.Log("space");

        }
    }
}
