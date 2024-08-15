using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Walls : MonoBehaviour
{
    Rigidbody2D RB;
    BoxCollider2D BC;
    private bool dilaybo=false;//鼻を弾き飛ばした後連続で弾き飛ばさないようにする。
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        BC = GetComponent<BoxCollider2D>();
        RB.gravityScale = 0;
        BC.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        Transform parentTransform = transform.parent;
        if (parentTransform == null)
        {
            RB.gravityScale = 1.3f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Nosemove nose;//呼ぶスクリプトにあだ名をつける
        GameObject obj = GameObject.Find("nose_player");//Circleというゲームオブジェクトを探す
        nose = obj.GetComponent<Nosemove>();//スクリプトを取得
        if (collision.gameObject.tag == "Player"&&dilaybo==false)
        {
            StartCoroutine(Dilay());
            Nosemove.myTransform.Rotate(0, 0, 180f, Space.World);
            
        }
    }
    private IEnumerator Dilay()//鼻を一時的にフリーズさせる
    {
        dilaybo = true;
        yield return new WaitForSeconds(1);
        dilaybo = false;
    }
}
