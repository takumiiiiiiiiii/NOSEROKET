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
        yield return new WaitForSeconds(10);//1秒後にダッシュ終わり
        SceneManager.LoadScene(SceneName);//some_senseiシーンをロードする

    }
}
