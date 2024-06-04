using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_botton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //スペースキー押した場合
        {
            SceneManager.LoadScene("SampleScene");//some_senseiシーンをロードする
            Debug.Log("space");
        }
    }
}
