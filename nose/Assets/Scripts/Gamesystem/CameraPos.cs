using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    
    GameObject Nose;
    private bool cameramove = true;
    // Start is called before the first frame update
    void Start()
    {
        Nose = GameObject.Find("nose_player");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(cameramove);
        if (Nosemove.DoNotMove==false&&cameramove)
        {
            this.transform.position = new Vector3(Nose.transform.position.x, Nose.transform.position.y, -10);
        }
    }

    public void StartAnime()
    {

    }
}
