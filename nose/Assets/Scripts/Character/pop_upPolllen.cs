using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class pop_upPolllen : MonoBehaviour
{

    [SerializeField] private CircleCollider2D CC;
    [SerializeField] private Rigidbody2D RB;
    public float speed;
    public float pop_min=100f,pop_max=200f;
    private Vector2 vec;//鼻のベクトルを入れる

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        CC = GetComponent<CircleCollider2D>();
        RB.AddForce(new Vector2(Random.Range(pop_min, pop_max), Random.Range(pop_min,pop_max)));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Nosemove nose;//呼ぶスクリプトにあだ名をつける
        GameObject obj = GameObject.Find("nose_player");//Circleというゲームオブジェクトを探す
        nose = obj.GetComponent<Nosemove>();//スクリプトを取得
        Vector2 Pvec = new Vector2(nose.player_vector.x, nose.player_vector.y);//プレイヤーの座標を保存
        vec = Pvec - new Vector2(this.transform.position.x, this.transform.position.y);//プレイヤーの位置から敵の位置を引く
        vec = vec.normalized*speed;//正規化
        StartCoroutine(Homing());
        RB.AddForce(vec*speed);
        //RB.velocity = vec * speed;
    }
    private IEnumerator Homing()
    {
        
        yield return new WaitForSeconds(0.5f);
        RB.velocity = vec * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

}
