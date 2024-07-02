using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizePos : MonoBehaviour
{
     Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = this.gameObject.GetComponent<Camera>();

        if (!cam.orthographic)
        {
            cam.orthographic = true;  // 必要に応じてオーソグラフィックモードに切り替える
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StartBotton.start == true)
        {
            cam.orthographicSize -= 1.0f;
        }
    }
}
