using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Wall : MonoBehaviour
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

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Dilay());
            Nosemove.myTransform.Rotate(0, 0, 180f, Space.World);
        }
    }
    private IEnumerator Dilay()//鼻を一時的にフリーズさせる
    {
        dilaybo = true;
        yield return new WaitForSeconds(0.1f);
        dilaybo = false;
    }
}
