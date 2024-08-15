using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionString : MonoBehaviour
{
    // Start is called before the first frame update
    private float str_x;
    private float str_y;

    public float min_pos_x;
    public float max_pos_x;
    public float min_pos_y;
    public float max_pos_y;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        str_x = Random.Range(min_pos_x, max_pos_x);
        str_y = Random.Range(min_pos_y, max_pos_y);
        
        transform.position = new Vector3(str_x, str_y, 0);
    }
}
