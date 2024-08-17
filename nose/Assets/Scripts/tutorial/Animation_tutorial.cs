using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//鼻のスクリプトを一時的に止めておく
public class Nose_wait : MonoBehaviour
{

    public GameObject targetObject;  // 対象のオブジェクト（Inspectorでアサイン）
    public float wait_time=1.0f;
    private bool start_nose=false;

    void Start()
    {
        // 対象のオブジェクトにアタッチされたスクリプトを取得
        StartCoroutine(Dilay());
    }
    void Update()
    {
        Nosemove targetScript = targetObject.GetComponent<Nosemove>();
        if (targetScript != null)
        {
            if (start_nose)
            {
                targetScript.enabled = true;
            }
            else
            {
                targetScript.enabled = false;
            }
            // スクリプトを無効にする
           
        }
        else
        {
            Debug.LogWarning("スクリプトが見つかりません！");
        }
    }
    private IEnumerator Dilay()//鼻を一時的にフリーズさせる
    {
        yield return new WaitForSeconds(wait_time);
        start_nose = true;
    }
}
