using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nosemove : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D RB;
    public float speed;
    private Vector2 forword;
    void Start()
    {
        forword = transform.forward;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        Transform myTransform = this.transform;
        Vector3 worldAngle = myTransform.eulerAngles;
        Debug.Log(forword);
        transform.position += transform.rotation * new Vector2(0,speed);
        worldAngle.x = 10.0f;
        worldAngle.y = 0;
        worldAngle.z = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myTransform.Rotate(0, 0, 3.0f, Space.World);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            myTransform.Rotate(0, 0, -3.0f, Space.World);
        }
    }
}
