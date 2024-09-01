using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Burst.CompilerServices;


public class pollenHit : MonoBehaviour
{
    //Scoreを表示
    private bool instant = false;
    //Cameraへ移動する
    public Camera targetCamera;
    //
    public float speed;
    [SerializeField] private GameObject pollen_point;
    [SerializeField] private CircleCollider2D Cc;
    [SerializeField] private Rigidbody2D RB;
    private Vector2 vec;//鼻のベクトルを入れる
    public Score Sc;
    public SliderController Sdc;


    // Start is called before the first frame update
    void Start()
    {
        targetCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        //カメラのいちを取得
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
        Sc = GameObject.Find("Text (TMP) (1)").GetComponent<Score>();
        Sdc = GameObject.Find("dash_slider").GetComponent<SliderController>();
        RB = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (instant)
        {
            Vector3 screenPoint = new Vector3(targetCamera.pixelWidth, 0, 0);//カメラの座標位置を取得
            Vector3 worldPos = targetCamera.ScreenToWorldPoint(screenPoint);//カメラの座標をワールドに変換
                                                                            // ゲームオブジェクトをその位置に移動
            Vector2 Pvec = new Vector2(worldPos.x, worldPos.y);//UIの座標を保存
            vec = Pvec - new Vector2(this.transform.position.x, this.transform.position.y);//プレイヤーの位置から敵の位置を引く
            vec = vec.normalized * speed;//正規化

            StartCoroutine(Homing());
            RB.AddForce(vec * speed);

            //RB.velocity = vec * speed;
            if (this.transform.position.y < worldPos.y)
            {
                Sc.score += 100;
                Sdc.CollectObject();
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"&&Sc!=null)
        {
            instant = true;
            Instantiate(pollen_point, this.transform.position, Quaternion.identity);
            Sc.score += 100;
            Sdc.CollectObject();
           
            
        }
    }
    private IEnumerator Homing()
    {
        yield return new WaitForSeconds(0.1f);
 
        RB.velocity = vec * speed;
    }
}
