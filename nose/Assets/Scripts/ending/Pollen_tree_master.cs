using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Pollen_tree_master : MonoBehaviour
{
    [SerializeField] private CircleCollider2D CC;
    [SerializeField] private Rigidbody2D RB;
    public static int tree_cnt=0;
    public static int tree_cnt_end = 90;
    public static int tree_cnt_max = 100;
    public static float camera_xpos = 0.4f;
    public float between=1.0f;
    [SerializeField] public static float distance;
    public GameObject tree;//花粉
    // Start is called before the first frame update
    void Start()
    {
        distance = between;
        RB = GetComponent<Rigidbody2D>();
        CC = GetComponent<CircleCollider2D>();
        Instantiate(tree, this.transform.position+new Vector3(distance,0,0), Quaternion.identity);//花粉を置く
    }
    // Update is called once per frame
    void Update()
    {
        //RB.AddForce(new Vector2(10, 0));
    }
}
