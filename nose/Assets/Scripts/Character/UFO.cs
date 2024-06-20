 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UFO : MonoBehaviour
{
    
    public int pollen_out_distance = 50;//花粉を発射する間隔
    public float speed = 6;//移動スピード
    public float dec_width = 3;//波の大きさ
    public float dec_time = 0.1f;//波のはやさ

    private bool move = false;//移動フラグ
    private bool contact = false;
    private int Scnt;//時間を数える
    private float dec = 0; //角度を入れる
    private bool vectorget_flag=false;
    private Vector2 vec;

    public GameObject pollen;//花粉
    Rigidbody2D RB;
    CircleCollider2D CC;
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        CC = GetComponent<CircleCollider2D>();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Nosemove nose;//呼ぶスクリプトにあだ名をつける
        GameObject obj = GameObject.Find("nose");//Circleというゲームオブジェクトを探す
        nose = obj.GetComponent<Nosemove>();//スクリプトを取得
        
        if (move == true)
        {
            
            if (vectorget_flag == false)
            {
                StartCoroutine(Stopcontroler());
                Vector2 Pvec = new Vector2(nose.player_vector.x, nose.player_vector.y);//プレイヤーの座標を保存
                vec = Pvec - new Vector2(this.transform.position.x, this.transform.position.y);//プレイヤーの位置から敵の位置を引く
                vec = vec.normalized;//正規化
                vectorget_flag = true;
            }
            dec +=dec_time;
            if (dec >= 360)
            {
                dec = dec_time;
            }
            RB.velocity = -vec*speed + new Vector2(Mathf.Sin(dec) * dec_width *　Mathf.Abs(vec.y), Mathf.Sin(dec) * dec_width * Mathf.Abs(vec.x));
            Debug.Log("こんな感じ:"+vec);//移動する+new Vector2(Mathf.Sin(dec) * dec_width, 0)
            Scnt = Scnt + 1;
            if (Scnt % pollen_out_distance == 0)
            {
                Instantiate(pollen, this.transform.position, Quaternion.identity);//花粉を置く
                Scnt = 0;
            }
        }
        if(move == false && contact == false)
        {
            
        }
    }
    private IEnumerator Stopcontroler()
    {
        yield return new WaitForSeconds(3);
        move = false;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&&contact==false)
        {
            contact = true;
            move = true;
        }
    }
}
