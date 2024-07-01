using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizePos : MonoBehaviour
{
    public bool startFlag = false;
    public float zoomSize = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) == true && startFlag == false)
        {
            startFlag = true;
            while(zoomSize < 2)
            {
                GetComponent<Camera>().orthographicSize = (zoomSize + 1)/10;
            }
        }
    }
}
