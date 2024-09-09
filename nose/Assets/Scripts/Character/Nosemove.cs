using System.Collections;
using AIE2D;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class Nosemove : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D RB;
    public CircleCollider2D CC2D;
    public Animator anima;
    public float speed;//鼻の移動スピードを入力する
    public float dash_speed=2;//ダッシュ時のスピード
    public Vector2 player_vector;//プレイヤーの現在地を記録
    public  static float growlevel=0.6f;
    private Vector2 forword;
    AudioSource audiosorce;
    public AudioClip dash;
    public AudioClip right_left_move;

    private bool a_RightLeft_played;
    private bool audioRight_played;
    private bool audioLeft_played;

    public AudioClip[] Damage;
    public SliderController Sdc;

    

    //チャージ関連
    private float charge_time=0f;//チャージ時間を入れる
    public static bool maxcharge = false;//最大チャージ

    [HideInInspector] public static bool DoNotMove=false;
    [HideInInspector] public static bool Nose_Dush=false;
    [HideInInspector] public static Transform myTransform;
    [HideInInspector] private float x_before=10f,z_before=10f;
    

    private AfterImageEffect2DPlayerBase _player = null;
    

    private 
    void Start()
    {
        maxcharge = false;
        DoNotMove = false;
        Nose_Dush = false;
        a_RightLeft_played = false;
        audioRight_played = false;
        audioLeft_played = false;
        anima = gameObject.GetComponent<Animator>();
        forword = transform.forward;
        _player = gameObject.GetComponent<AfterImageEffect2DPlayerBase>();
        audiosorce = GetComponent<AudioSource>();
        
        Sdc = GameObject.Find("dash_slider").GetComponent<SliderController>();
        CC2D = gameObject.AddComponent<CircleCollider2D>();
        CC2D.radius = 0.0001f;
        CC2D.isTrigger = false;
        CC2D.offset = new Vector2(0, 1);

        //_player.CreateAfterImage(gameObject.GetComponent<SpriteRenderer>());
    }
    // Update is called once per frame
    private void Update()
    {
        Debug.Log("チャージ状態"+maxcharge);
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        anima.SetBool("Left_anima", false);
        anima.SetBool("Right_anima", false);
        if (serial.connect_char == true)
        {
            float x, z;
            if (float.TryParse(serial.x, out x)&&float.TryParse(serial.z, out z))
            {
                //チャージ時間に応じた処理
                if (z < growlevel && x < growlevel)
                {
                    charge_time += Time.deltaTime;
                    Debug.Log(charge_time);
                    if (charge_time < 0.25f)
                    {

                        maxcharge = false;
                        // 短く押された場合の反応を記述
                    }
                    else if (charge_time >= 0.25f && charge_time < 0.5f)
                    {
                        maxcharge = false;
                        // 中くらいの時間押された場合の反応を記述
                    }
                    else if (charge_time >= 0.5f && charge_time < 0.75f)
                    {
                        maxcharge = true;
                        // 長く押された場合の反応を記述
                    }
                }
                //花粉が溜まってないと動かない
                if (x_before < growlevel && z_before < growlevel && Nose_Dush == false)
                {
                    //if (Sdc.EmittingObject())
                        StartCoroutine(Dash());
                        
                    if (x >= growlevel || z >= growlevel)//
                    {
                        StartCoroutine(Dash());
                    }
                }
                //鼻のアニメーション
                if (Nose_Dush == true)
                {
                    _player.SetActive(true);
                }
                else
                {
                    _player.SetActive(false);
                }
                //
                x_before = x;
                z_before = z;
                if (x < growlevel && z >= growlevel)
                {
                   
                    anima.SetBool("Right_anima", true);
                }
                if (z < growlevel && x >= growlevel)
                {
                    anima.SetBool("Left_anima", true);
                    
                }
            }
           
        }
        else
        {
            //アニメーション
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.Space) == false)
            {
                anima.SetBool("Right_anima", true);                
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKey(KeyCode.Space) == false && !audioLeft_played)//左の鼻の穴をさす音
            {
                audiosorce.PlayOneShot(right_left_move);
                audioLeft_played = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKey(KeyCode.Space) == false)
            {
                audioLeft_played = false; ;
            }

            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.Space) == false)
            {
                anima.SetBool("Left_anima", true);                
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKey(KeyCode.Space) == false && !audioRight_played)//右の鼻の穴をさす音
            {
                audiosorce.PlayOneShot(right_left_move);
                audioRight_played = true;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow) && Input.GetKey(KeyCode.Space) == false)
            {
                audioRight_played = false; ;
            }

            if (Input.GetKey(KeyCode.Space) && Nose_Dush == false)
            {
                charge_time += Time.deltaTime;
                Debug.Log(charge_time);
                if (charge_time < 0.25f)
                {
                    maxcharge = false;
                    // 短く押された場合の反応を記述
                }
                else if (charge_time >= 0.25f && charge_time < 0.5f)
                {
                    maxcharge = false;
                    // 中くらいの時間押された場合の反応を記述
                }
                else if (charge_time >= 0.5f && charge_time < 0.75f)
                {
                    maxcharge = true;
                    // 長く押された場合の反応を記述
                }
            }

            
            if (Input.GetKeyUp(KeyCode.Space) && Nose_Dush == false)
            {
                //if(Sdc.EmittingObject())
                if (Sdc.pollenPoint >= 100)
                {                   
                    StartCoroutine(SuperDash());
                }
                else
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
        
        if (serial.connect_char == true)
        {
            float x, z;
            if (float.TryParse(serial.x, out x) && float.TryParse(serial.z, out z))//文字を数字に直しつつ変なデータがきたら弾く
            {
                Debug.Log("X:" + x);
                if (DoNotMove == false)
                {
                    if (z < growlevel && x >= growlevel)//右移動
                    {
                        myTransform.Rotate(0, 0, 3.0f, Space.World);
                    }

                    if (x < growlevel && z >= growlevel)
                    {                      
                        myTransform.Rotate(0, 0, -3.0f, Space.World);//左移動
                    }

                    if ((z < growlevel && x >= growlevel) && !audioRight_played)//右の鼻の穴をさす音
                    {
                        audiosorce.PlayOneShot(right_left_move);
                        audioRight_played = true;
                    }
                    else
                    {
                        audioRight_played = false;
                    }
                    if ((x < growlevel && z >= growlevel) && !audioLeft_played)//左の鼻の穴をさす音
                    {
                        audiosorce.PlayOneShot(right_left_move);
                        audioLeft_played = true;
                    }
                    else
                    {
                        audioLeft_played = false;
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
        audiosorce.PlayOneShot(dash);
        yield return new WaitForSeconds(1);//1秒後にダッシュ終わり
        charge_time = 0f;
        Nose_Dush = false;
        maxcharge = false;
        
    }

    private IEnumerator SuperDash()
    {
        Nose_Dush = true;
        anima.SetBool("Charge", true);
        CC2D.radius = 3.0f;
        audiosorce.PlayOneShot(dash);
        Sdc.feverFlag = true;
        Sdc.FeverTime();
        yield return new WaitForSeconds(8);//1秒後にダッシュ終わり
        Sdc.pollenPoint -= 100;
        Sdc.feverFlag = false;
        Sdc.FeverTime();
        charge_time = 0f;
        Nose_Dush = false;
        CC2D.radius = 0.0001f;
        anima.SetBool("Charge", false);
        maxcharge = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Nosemove nose;//呼ぶスクリプトにあだ名をつける
        //GameObject obj = GameObject.Find("nose_player");//Circleというゲームオブジェクトを探す
        //nose = obj.GetComponent<Nosemove>();//スクリプトを取得
        if (collision.gameObject.tag == "Planet" && Nose_Dush == true)
        {
            audiosorce.PlayOneShot(Damage[Random.Range(0, Damage.Length)]);
        }
    }

    
}
