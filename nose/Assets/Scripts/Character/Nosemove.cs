using System.Collections;
using AIE2D;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class Nosemove : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D RB;
    public Animator anima;
    public float speed;//鼻の移動スピードを入力する
    public float dash_speed=2;//ダッシュ時のスピード
    public Vector2 player_vector;//プレイヤーの現在地を記録
    public  static float growlevel=30f;
    private Vector2 forword;
    AudioSource audiosorce;
    AudioSource audiosorce2;
    public AudioClip charge;
    public AudioClip dash;
    public AudioClip right_left_move;

    private bool superDush = false;

    public AudioClip superDashBGM;

    private bool audioCharge_played;
    private bool audioRight_played;
    private bool audioLeft_played;

    public AudioClip[] Damage;
    public SliderController Sdc;

    public muteplayBGM Mbb;

    [HideInInspector] public bool backBGMmute = false;

    //チャージ関連
    private float charge_time=0f;//チャージ時間を入れる
    public static bool maxcharge = false;//最大チャージ

    [SerializeField] private GameObject audio_charge;

    [HideInInspector] public static bool DoNotMove=false;
    [HideInInspector] public static bool Nose_Dush=false;
    [HideInInspector] public static Transform myTransform;
    [HideInInspector] private float x_before=10000f,z_before=10000f;
    

    private AfterImageEffect2DPlayerBase _player = null;
    

    private 
    void Start()
    {
        growlevel = 49;
        maxcharge = false;
        DoNotMove = false;
        Nose_Dush = false;
        audioCharge_played = false;
        audioRight_played = false;
        audioLeft_played = false;
        anima = gameObject.GetComponent<Animator>();
        forword = transform.forward;
        _player = gameObject.GetComponent<AfterImageEffect2DPlayerBase>();
        audiosorce = GetComponent<AudioSource>();
        audiosorce2 = GetComponent<AudioSource>();

        Sdc = GameObject.Find("dash_slider").GetComponent<SliderController>();
        Mbb = GameObject.Find("BGM").GetComponent<muteplayBGM>();

        //_player.CreateAfterImage(gameObject.GetComponent<SpriteRenderer>());
    }
    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("チャージ状態"+maxcharge);
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
                //x = x - 25f;
                //チャージ時間に応じた処理
                if (z < growlevel && x < growlevel)
                {
                    anima.SetBool("charge", true);
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
                if (z < growlevel && x < growlevel && !audioCharge_played)
                {
                    audiosorce2.PlayOneShot(charge);
                    audiosorce2.PlayOneShot(charge);
                    audioCharge_played = true;
                }
                if ((x >= growlevel && z >= growlevel) && Nose_Dush == false)
                {
                    audiosorce2.Stop();
                    audioCharge_played = false;
                }

                //花粉が溜まってないと動かない
                if (x_before < growlevel && z_before < growlevel && Nose_Dush == false)
                {

                    //if (Sdc.EmittingObject())
                        //StartCoroutine(Dash());
                        
                    if (x >= growlevel || z >= growlevel)//
                    {
                        anima.SetBool("charge", true);
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
                    anima.SetBool("Left_anima", true);
                }
                if (z < growlevel && x >= growlevel)
                {
                    anima.SetBool("Right_anima", true);
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
                audioLeft_played = false;
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

            if (Input.GetKey(KeyCode.Space) && Nose_Dush == false && !audioCharge_played)//音
            {
                audiosorce2.PlayOneShot(charge);
                audiosorce2.PlayOneShot(charge);
                audioCharge_played = true;
            }
            if (Input.GetKeyUp(KeyCode.Space) && Nose_Dush == false)
            {
                audiosorce2.Stop();
                audioCharge_played = false;
            }

            if (Input.GetKey(KeyCode.Space) && Nose_Dush == false)
            {

                anima.SetBool("charge", true);                
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

                if (Sdc.pollenPoint >= 100)
                {     
                    anima.SetBool("charge", true);
                    //
                }
                StartCoroutine(Dash());
                //if(Sdc.EmittingObject())

                
                    //StartCoroutine(Dash());
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
        //Debug.Log(Nose_Dush);
        worldAngle.x = 10.0f;
        worldAngle.y = 0;
        worldAngle.z = 0;
        //Debug.Log("DoNotMove:" + DoNotMove);
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        Debug.Log("センサーの接続:"+serial.connect_char);
        if (serial.connect_char == true)
        {
            float x, z;
            if (float.TryParse(serial.x, out x) && float.TryParse(serial.z, out z))//文字を数字に直しつつ変なデータがきたら弾く
            {
                //x = x - 25;
                //Debug.Log("X:" + x);
                if (DoNotMove == false)
                {
                    if (z < growlevel && x >= growlevel)//右移動
                    {
                        myTransform.Rotate(0, 0, -3.0f, Space.World);//左移動

                    }

                    if (x < growlevel && z >= growlevel)
                    {
                        myTransform.Rotate(0, 0, 3.0f, Space.World);
                    }

                    if ((z < growlevel && x >= growlevel))//右の鼻の穴をさす音
                    {
                        if (!audioRight_played)
                        {
                            audiosorce.PlayOneShot(right_left_move);
                            audioRight_played = true;
                        }
                    }
                    else
                    {
                        audioRight_played = false;
                    }
                    if ((x < growlevel && z >= growlevel))//左の鼻の穴をさす音
                    {
                        if (!audioLeft_played)
                        {
                            audiosorce.PlayOneShot(right_left_move);
                            audioLeft_played = true;
                        }
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
        if (!superDush)
        {
            anima.SetBool("charge", false);
            Nose_Dush = true;
            audiosorce.PlayOneShot(dash);
        }
        yield return new WaitForSeconds(1);//1秒後にダッシュ終わり
        if (!superDush)
        {
            charge_time = 0f;
            Nose_Dush = false;
            maxcharge = false;
        }
    }
    public void SuperDashStart()
    {
        StartCoroutine(SuperDash());
    }
    private IEnumerator SuperDash()
    {

        superDush = true;
        backBGMmute = true;
        anima.SetBool("charge", true);
        Mbb.muteFlag(backBGMmute);
        Nose_Dush = true;
        audiosorce.PlayOneShot(superDashBGM);
        audiosorce.PlayOneShot(dash);
        Sdc.feverFlag = true;
        Sdc.FeverTime();
        Sdc.pollenPoint -= 100;
        yield return new WaitForSeconds(8);//1秒後にダッシュ終わり
        superDush = false;
        anima.SetBool("charge", false);
        
        anima.SetBool("charge", false);
        Sdc.feverFlag = false;
        Sdc.FeverTime();
        charge_time = 0f;
        backBGMmute = false;
        Mbb.muteFlag(backBGMmute);
        Nose_Dush = false;
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
