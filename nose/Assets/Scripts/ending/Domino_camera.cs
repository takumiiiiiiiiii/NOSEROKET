using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Domino_camera : MonoBehaviour
{
    public static Transform target; // 移動先のターゲット位置
    public float speed = 5f; // 移動速度

    void Start()
    {

    }
    void Update()
    {
        // ターゲット位置に向かって移動
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}