using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_ending : MonoBehaviour
{
    public static Animator anima_end;
    public static bool anima_start_end=false;
    // Start is called before the first frame update
    void Start()
    {
        anima_end = gameObject.GetComponent<Animator>();
        anima_start_end = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (DominoStart.end_start)
        {
            
        }
    }
}
