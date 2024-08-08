using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionString : MonoBehaviour
{
    // Start is called before the first frame update
    private float str_x;
    private float str_y;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        str_x = Random.Range(-0.05f, 0.05f);
        str_y = Random.Range(3.1f, 3.2f);
        
        transform.position = new Vector3(str_x, str_y, 0);
    }
}
