using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nosemove : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D RB;
    public float speed;//鼻の移動スピードを入力する
    public Vector2 player_vector;//プレイヤーの現在地を記録
    private Vector2 forword;
    private bool Nose_Dush=false;
    void Start()
    {
        forword = transform.forward;
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && Nose_Dush == false)
        {
            Debug.Log("fafafa");
            StartCoroutine(Dash());
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
        
        if (Input.GetKey(KeyCode.LeftArrow)&& Input.GetKey(KeyCode.Space) == false)
        {
            myTransform.Rotate(0, 0, 3.0f, Space.World);
        }
        if (Input.GetKey(KeyCode.RightArrow)&&Input.GetKey(KeyCode.Space) == false)
        {
            myTransform.Rotate(0, 0, -3.0f, Space.World);
        }
        if (Input.GetKey(KeyCode.Space)==false)
        {
            if (Nose_Dush == true)//
            {
                transform.position += transform.rotation * new Vector2(0, speed * 2);//ダッシュ
            }
            else
            {
                transform.position += transform.rotation * new Vector2(0, speed);//通常の移動
            }
        }else if(Nose_Dush == true)
        {
            transform.position += transform.rotation * new Vector2(0, speed * 2);//ダッシュ
        }
    }
    private IEnumerator Dash()
    {
        Nose_Dush = true;
        yield return new WaitForSeconds(1);//1秒後にダッシュ終わり
        Nose_Dush = false;
    }
}
