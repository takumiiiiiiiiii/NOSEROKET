using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Planet : MonoBehaviour
{
    public float speed=40;//吹っ飛んで行くスピード
    public int OUT_POLLEN=20;//落とす花粉の数
    [SerializeField] private GameObject pollen;//花粉
    [SerializeField] private GameObject HitEffect;//ヒットエフェクト

    private bool Hit = false;//ダッシュ中の鼻にあたったかを判定
    private Vector2 vec;//鼻のベクトルを入れる
    private bool vector_get = true;//当たった瞬間のベクトルを取得する
    Rigidbody2D RB;
    CircleCollider2D CC;
    SliderController Sdc;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        CC = GetComponent<CircleCollider2D>();
        Sdc = GameObject.Find("dash_slider").GetComponent<SliderController>();
        RB.gravityScale = 0;
        RB.mass = 0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Nosemove nose;//呼ぶスクリプトにあだ名をつける
        GameObject obj = GameObject.Find("nose_player");//Circleというゲームオブジェクトを探す
        nose = obj.GetComponent<Nosemove>();//スクリプトを取得
        if (Hit == true) {
            CC.isTrigger = true;
            if (vector_get == true)
            {
                RB.constraints = RigidbodyConstraints2D.None;
                this.gameObject.transform.DetachChildren();
                for (int i = 0; i < OUT_POLLEN; i++)
                {
                    Instantiate(pollen, this.transform.position, Quaternion.identity);//花粉を置く
                }
                Debug.Log("飛ぶ");
                Vector2 Pvec = new Vector2(nose.player_vector.x, nose.player_vector.y);//プレイヤーの座標を保存
                vec = Pvec - new Vector2(this.transform.position.x, this.transform.position.y);//プレイヤーの位置から敵の位置を引く
                vec = vec.normalized;//正規化
                vector_get = false;

            }
            RB.velocity = -vec * speed;
            if(this.transform.position.x < -140)
            {
                CC.isTrigger = false; 
            }
        }
    }
    private IEnumerator Stopc_MoveNose()//鼻を一時的にフリーズさせる
    {
        if(!Sdc.feverFlag)
        {
            Nosemove.DoNotMove = true;
            yield return new WaitForSeconds(1);
            Nosemove.DoNotMove = false;
        }
 
    }
    private IEnumerator HitStop()//鼻を一時的にフリーズさせる
    {
        GameObject game = GameObject.Find("MainCamera");
        if (game != null)
        {
            Camerashake camerachake = game.GetComponent<Camerashake>();
            camerachake.StartShake(0.6f, 0.6f, 0.6f);
        }
        yield return new WaitForSeconds(0.1f);
        Hit = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       

        Nosemove nose;//呼ぶスクリプトにあだ名をつける
        GameObject obj = GameObject.Find("nose_player");//Circleというゲームオブジェクトを探す
        nose = obj.GetComponent<Nosemove>();//スクリプトを取得
        if (collision.gameObject.tag == "Player" && Nosemove.Nose_Dush == true)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (HitEffect != null)
                {
                    Instantiate(HitEffect, (Vector3)point.point, Quaternion.identity);//花粉を置く
                }
            }
            //GameObject game = GameObject.Find("MainCamera");
            //if (game != null)
            //{
            //    Camerashake camerachake = game.GetComponent<Camerashake>();
            //    camerachake.StartShake(0.6f, 0.6f, 0.6f);
            //}
            StartCoroutine(HitStop());
            if (!Nosemove.maxcharge)
            {
                StartCoroutine(Stopc_MoveNose());
            }
            //Hit = true;
        }
    }
}
