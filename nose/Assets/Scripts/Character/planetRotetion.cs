using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetRotetion : MonoBehaviour
{
    [SerializeField, Header("回転速度")]
    private float rotetionSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;
        myTransform.Rotate(0.0f, 0.0f, rotetionSpeed);
    }
}
