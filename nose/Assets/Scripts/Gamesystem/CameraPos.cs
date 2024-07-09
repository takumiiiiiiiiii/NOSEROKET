using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    GameObject Nose;
    // Start is called before the first frame update
    void Start()
    {
        Nose = GameObject.Find("nose_player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Nosemove.DoNotMove == false)
        {
            this.transform.position = new Vector3(Nose.transform.position.x, Nose.transform.position.y, -10);
        }
    }
}
