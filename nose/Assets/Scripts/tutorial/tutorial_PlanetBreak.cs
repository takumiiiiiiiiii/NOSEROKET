using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class tutorial_PlanetBreak : MonoBehaviour
{
    private Vector2 thispos;//最初の座標を保存
    private bool move=false;
    public GameObject black_out, text;

    public float speed = 40;//吹っ飛んで行くスピード
    public int OUT_POLLEN = 20;//落とす花粉の数
    public string scene_name = "kakehi_open";
    [SerializeField] private GameObject pollen;//花粉

    private bool Hit = false;//ダッシュ中の鼻にあたったかを判定
    private Vector2 vec;//鼻のベクトルを入れる
    private bool vector_get = true;//当たった瞬間のベクトルを取得する
    Rigidbody2D RB;
    CircleCollider2D CC;
    // Start is called before the first frame update
    void Start()
    {
        thispos = this.transform.position;
        RB = GetComponent<Rigidbody2D>();
        CC = GetComponent<CircleCollider2D>();
        RB.gravityScale = 0;
        RB.mass = 0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        Nosemove nose;//呼ぶスクリプトにあだ名をつける
        GameObject obj = GameObject.Find("nose_player");//Circleというゲームオブジェクトを探す
        nose = obj.GetComponent<Nosemove>();//スクリプトを取得
        if (Hit == true)
        {
            move = true;
            if (vector_get == true)
            {
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
            StartCoroutine(End_tutorial());
            RB.velocity = -vec * speed;
        }
        if (!move)
        {
            this.transform.position = thispos;
        }
    }
    private IEnumerator Stopc_MoveNose()//鼻を一時的にフリーズさせる
    {

        Nosemove.DoNotMove = true;
        yield return new WaitForSeconds(1);
        Nosemove.DoNotMove = false;
    }
    private IEnumerator End_tutorial()//シーンを移動
    {
        Hit = false;
        Instantiate(black_out);//画面暗転
        Instantiate(text);//文字を表示
        yield return new WaitForSeconds(3);
        Serial seria;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        seria = objc.GetComponent<Serial>();//スクリプトを取得
        //seria.serial.Close();

        SceneManager.LoadScene(scene_name);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Nosemove nose;//呼ぶスクリプトにあだ名をつける
        GameObject obj = GameObject.Find("nose_player");//Circleというゲームオブジェクトを探す
        nose = obj.GetComponent<Nosemove>();//スクリプトを取得
        if (collision.gameObject.tag == "Player" && Nosemove.Nose_Dush == true)
        {
            StartCoroutine(Stopc_MoveNose());
            Hit = true;
        }
    }
}
