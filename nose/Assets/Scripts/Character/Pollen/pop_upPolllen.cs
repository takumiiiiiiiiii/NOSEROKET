using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class pop_upPollen : MonoBehaviour
{
    //Scoreを表示
    private int instant = 0;
    //Cameraへ移動する
    public Camera targetCamera;
    //
    public float pollenYpos, pollenXpos;//花粉が集まる座標を調整
    [SerializeField] private GameObject pollen_point;
    [SerializeField] private GameObject audio_getpollen;
    [SerializeField] private CircleCollider2D CC;
    [SerializeField] private Rigidbody2D RB;

    public float speed;
    public float pop_min = 100f, pop_max = 200f;
    private Vector2 vec;//鼻のベクトルを入れる
    public Score Sc;
    public SliderController Sdc;
    // Start is called before the first frame update
    void Start()
    {
        // カメラを探す
        GameObject cameraObject = GameObject.Find("MainCamera");
        // カメラが見つかった場合のみコンポーネントを取得する
        if (cameraObject != null)
        {
            targetCamera = cameraObject.GetComponent<Camera>();
            // targetCameraでさらに処理を続ける
        }
        //targetCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        //カメラのいちを取得
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
        Sc = GameObject.Find("Text (TMP) (1)").GetComponent<Score>();
        Sdc = GameObject.Find("dash_slider").GetComponent<SliderController>();
        RB = GetComponent<Rigidbody2D>();
        CC = GetComponent<CircleCollider2D>();
        RB.AddForce(new Vector2(Random.Range(pop_min, pop_max), Random.Range(pop_min, pop_max)));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //変換
        Vector3 screenPoint = new Vector3(targetCamera.pixelWidth, 0, 0);//カメラの座標位置を取得
        Vector3 worldPos = targetCamera.ScreenToWorldPoint(screenPoint);//カメラの座標をワールドに変換
        // ゲームオブジェクトをその位置に移動
        Vector2 Pvec = new Vector2(worldPos.x, worldPos.y)+new Vector2(pollenXpos,pollenYpos);//UIの座標を保存
        vec = Pvec - new Vector2(this.transform.position.x, this.transform.position.y);//プレイヤーの位置から敵の位置を引く
        vec = vec.normalized * speed;//正規化
        if (instant==1)
        {
            Instantiate(pollen_point, this.transform.position, Quaternion.identity);
            instant++;
        }
        StartCoroutine(Homing());
        RB.AddForce(vec * speed);
        //RB.velocity = vec * speed;
        if (this.transform.position.y < worldPos.y+2f)
        {
            Instantiate(audio_getpollen, this.transform.position, Quaternion.identity);
            if (Sc != null)
            {
                Sc.score += 100;
                Sdc.CollectObject();
                UIchake.schake = true;
            }
            Destroy(this.gameObject);
        }
    }
    private IEnumerator Homing()
    {
        yield return new WaitForSeconds(0.1f);
        instant++;
        RB.velocity = vec * speed;
    }
}
