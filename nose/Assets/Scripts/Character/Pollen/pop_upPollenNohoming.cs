using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class pop_upPollenNohoming : MonoBehaviour
{
    //Scoreへ移動する
    private bool GoScore=false;
    public Camera targetCamera;
    //
    [SerializeField] private GameObject pollen_point;
    [SerializeField] private CircleCollider2D CC;
    [SerializeField] private Rigidbody2D RB;
    public float speed;
    public float pop_min=100f,pop_max=200f;
    private Vector2 vec;//鼻のベクトルを入れる
    public Score Sc;
    public SliderController Sdc;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(pollen_point, this.transform.position, Quaternion.identity);
        targetCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        //カメラのいちを取得
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
        Sc = GameObject.Find("Text (TMP) (1)").GetComponent<Score>();
        Sdc = GameObject.Find("dash_slider").GetComponent<SliderController>();
        RB = GetComponent<Rigidbody2D>();
        CC = GetComponent<CircleCollider2D>();
        RB.AddForce(new Vector2(Random.Range(pop_min, pop_max), Random.Range(pop_min,pop_max)));
        Instantiate(pollen_point, this.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 screenPoint = new Vector3(targetCamera.pixelWidth, 0, 0);
        Vector3 worldPos = targetCamera.ScreenToWorldPoint(screenPoint);

        // ゲームオブジェクトをその位置に移動
        //transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
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
    private IEnumerator Homing()
    {
        yield return new WaitForSeconds(0.1f);
        RB.velocity = vec * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Sc != null)
        {
          
            GoScore = true;
           
            
            //Destroy(this.gameObject);
        }
    }


}
