using System.Collections;
using AIE2D;
using UnityEngine;

public class Nosemove : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D RB;
    public Animator anima;
    public float speed;//鼻の移動スピードを入力する
    public float dash_speed=2;//ダッシュ時のスピード
    public Vector2 player_vector;//プレイヤーの現在地を記録
    public float growlevel=0.3f;
    private Vector2 forword;
    
    [HideInInspector] public static bool DoNotMove=false;
    [HideInInspector] public static bool Nose_Dush=false;
    [HideInInspector] public static Transform myTransform;
    [HideInInspector] private float x_before,z_before;

    private AfterImageEffect2DPlayerBase _player = null;
    void Start()
    {
        anima = gameObject.GetComponent<Animator>();
        forword = transform.forward;
        _player = gameObject.GetComponent<AfterImageEffect2DPlayerBase>();
        //_player.CreateAfterImage(gameObject.GetComponent<SpriteRenderer>());
    }
    // Update is called once per frame
    private void Update()
    {
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        anima.SetBool("Left_anima", false);
        anima.SetBool("Right_anima", false);
        if (serial.connect_char == true && serial.x != null && serial.z != null)
        {
            Debug.Log("notcorrect" + serial.x);
            bool isBase = (serial.x is string);
            if (isBase == true)
            {
                float x = float.Parse(serial.x);
                float z = float.Parse(serial.z);
                if (x_before < growlevel && z_before < growlevel && Nose_Dush == false)
                {
                    if (x >= growlevel || z >= growlevel)
                    {
                        Debug.Log("fafafa");
                        StartCoroutine(Dash());
                    }
                }
                if (Nose_Dush == true)
                {
                    _player.SetActive(true);
                }
                else
                {
                    _player.SetActive(false);
                }
                x_before = x;
                z_before = z;
            }
        }
        else
        {
            //アニメーション
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.Space) == false)
            {
                anima.SetBool("Left_anima", true);
            }
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.Space) == false)
            {
                anima.SetBool("Right_anima", true);
            }
            if (Input.GetKeyUp(KeyCode.Space) && Nose_Dush == false)
            {
                Debug.Log("fafafa");
                StartCoroutine(Dash());
            }
            if (Nose_Dush == true)
            {
                _player.SetActive(true);
            }
            else
            {
                _player.SetActive(false);
            }
        }
        
    }
    void FixedUpdate()
    {
        player_vector = this.transform.position;
        myTransform = this.transform;
        Vector3 worldAngle = myTransform.eulerAngles;
        Debug.Log(Nose_Dush);
        worldAngle.x = 10.0f;
        worldAngle.y = 0;
        worldAngle.z = 0;
        Debug.Log("DoNotMove:" + DoNotMove);
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        
        if (serial.connect_char == true&&serial.x != null && serial.z != null)
        {
            bool isBase = (serial.x is string);
            if (isBase == true)
            {
                float x = float.Parse(serial.x);
                float z = float.Parse(serial.z);
                if (DoNotMove == false)
                {
                    if (x < growlevel && z >= growlevel)
                    {
                        myTransform.Rotate(0, 0, 3.0f, Space.World);
                    }
                    if (z < growlevel && x >= growlevel)
                    {
                        myTransform.Rotate(0, 0, -3.0f, Space.World);
                    }
                    if (x >= growlevel && z >= growlevel && Nose_Dush == true)
                    {
                        Debug.Log("iff");
                        transform.position += transform.rotation * new Vector2(0, speed * dash_speed);//ダッシュ
                    }
                    else if (x >= growlevel || z >= growlevel)
                    {

                        if (Nose_Dush == true)
                        {
                            transform.position += transform.rotation * new Vector2(0, speed * dash_speed);//ダッシュ
                        }
                        else
                        {
                            transform.position += transform.rotation * new Vector2(0, speed);//通常の移動
                        }
                    }
                }
            }
        }
        else
        {
            if (DoNotMove == false)
            {
                if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.Space) == false)
                {
                    
                    myTransform.Rotate(0, 0, 3.0f, Space.World);
                }
                if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.Space) == false)
                {
                    
                    myTransform.Rotate(0, 0, -3.0f, Space.World);
                }
                if (Input.GetKey(KeyCode.Space) == false)
                {
                    if (Nose_Dush == true)//
                    {
                        transform.position += transform.rotation * new Vector2(0, speed * dash_speed);//ダッシュ
                    }
                    else
                    {
                        transform.position += transform.rotation * new Vector2(0, speed);//通常の移動
                    }
                }
                else if (Nose_Dush == true)
                {
                    transform.position += transform.rotation * new Vector2(0, speed * dash_speed);//ダッシュ
                }
            }
        }
    }
    private IEnumerator Dash()
    {
        Nose_Dush = true;
        yield return new WaitForSeconds(1);//1秒後にダッシュ終わり
        Nose_Dush = false;
    }
}
