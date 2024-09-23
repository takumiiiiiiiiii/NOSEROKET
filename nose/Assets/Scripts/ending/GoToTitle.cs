using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToTitle : MonoBehaviour
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
        if (ending_start == true)
        {
            StartCoroutine(Gotitle());
        }
    }
    private IEnumerator Gotitle()
    {
        
        yield return new WaitForSeconds(14);//1秒後にダッシュ終わり
        Serial seria;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        seria = objc.GetComponent<Serial>();//スクリプトを取得
        //seria.serial.Close();
        SceneManager.LoadScene(SceneName);//some_senseiシーンをロードする

    }
}
