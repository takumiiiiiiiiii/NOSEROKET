using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class reroed : MonoBehaviour
{
    public static bool ending_start=false;
    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        ending_start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneName);//some_senseiシーンをロードする
        }
    }

}
