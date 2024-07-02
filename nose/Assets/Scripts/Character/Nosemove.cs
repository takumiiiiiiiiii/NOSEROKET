using System.Collections;
using System.Collections.Generic;
using AIE2D;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Nosemove : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D RB;
    public float speed;//鼻の移動スピードを入力する
    public float dash_speed=2;//ダッシュ時のスピード
    public Vector2 player_vector;//プレイヤーの現在地を記録
    private Vector2 forword;
    [HideInInspector] public static bool DoNotMove=false;
    [HideInInspector] public static bool Nose_Dush=false;
    private AfterImageEffect2DPlayerBase _player = null;
    void Start()
    {
        forword = transform.forward;
        _player = gameObject.GetComponent<AfterImageEffect2DPlayerBase>();
        //_player.CreateAfterImage(gameObject.GetComponent<SpriteRenderer>());
    }
    // Update is called once per frame
    private void Update()
    {
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("Serial");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        if (serial.conect == true)
        {
            if (int.Parse(serial.z) == 1 && int.Parse(serial.x) == 1 && Nose_Dush == false)
            {
                Debug.Log("fafafa");
                StartCoroutine(Dash());

            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Space) && Nose_Dush == false)
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
    }
    void FixedUpdate()
    {
        player_vector = this.transform.position;
        Transform myTransform = this.transform;
        Vector3 worldAngle = myTransform.eulerAngles;
        Debug.Log(Nose_Dush);
        worldAngle.x = 10.0f;
        worldAngle.y = 0;
        worldAngle.z = 0;
        

        Debug.Log("DoNotMove:" + DoNotMove);
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("Serial");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        if (serial.conect == true)
        {
            if (DoNotMove == false)
            {
                if (int.Parse(serial.x)==1 && int.Parse(serial.z) == 0)
                {
                    myTransform.Rotate(0, 0, 3.0f, Space.World);
                }
                if (int.Parse(serial.z) == 1 && int.Parse(serial.x) == 0)
                {
                    myTransform.Rotate(0, 0, -3.0f, Space.World);
                }
                if (int.Parse(serial.z) == 1&& int.Parse(serial.x) == 1)
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
