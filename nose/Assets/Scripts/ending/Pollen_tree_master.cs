using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Pollen_tree_master : MonoBehaviour
{
    [SerializeField] private CircleCollider2D CC;
    [SerializeField] private Rigidbody2D RB;

    public static bool end = true;
    public static int tree_cnt=0;
    public static int tree_cnt_end = 90;//杉の木の倒す数
    public static int tree_cnt_max = 1000;//杉の木の最大数
    public static float camera_xpos = 0.4f;
    public float between=1.0f;
    [SerializeField] public static float distance;
    public GameObject tree;//花粉
    [SerializeField] private bool debug=false;//デバッグよう
    // Start is called before the first frame update
    void Start()
    {

        end = true;
        tree_cnt = 0;
        tree_cnt_end = Score.current_score/100;
        tree_cnt_max = 1000;
        if (debug)
        {
            tree_cnt_end = 100;
        }
        distance = between;
        camera_xpos = 0.4f;
        RB = GetComponent<Rigidbody2D>();
        CC = GetComponent<CircleCollider2D>();
        Instantiate(tree, this.transform.position+new Vector3(distance,0,0), Quaternion.identity);//木を置く
    }
    // Update is called once per frame
    void Update()
    {
 
        //RB.AddForce(new Vector2(10, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
