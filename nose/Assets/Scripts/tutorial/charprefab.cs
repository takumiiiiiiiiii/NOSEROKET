using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charprefab : MonoBehaviour
{

    void Start()
    {
        transform.parent = GameObject.Find("Canvas").transform;
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
