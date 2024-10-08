using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Domino_camera : MonoBehaviour
{
    public static Transform target; // 移動先のターゲット位置
    public float speed = 5f; // 移動速度
    public static bool finaldomino = false;
    void Start()
    {
        finaldomino = false;
    }
    void Update()
    {
        Debug.Log(speed);
        // ターゲット位置に向かって移動
        
        if (target != null&&DominoStart.end_start)
        {
            //Debug.Log("transform:"+transform.position.x);
            //Debug.Log("target:" + target.position.x);
           
            Vector3 direction = (target.position - transform.position).normalized;
            if (transform.position.x < target.position.x)
            {
                speed++;
            }
            else
            {
                speed--;
            }
            if (transform.position.x >= target.position.x - 0.5 && Animation_ending.anima_start_end == true && finaldomino)
            {
                speed = 0;
                GoToTitle.ending_start = true;
                Animation_ending.anima_end.SetBool("EndGame", true);
            }
            transform.position += new Vector3(direction.x * speed * Time.deltaTime, 0);
        }
    }
}