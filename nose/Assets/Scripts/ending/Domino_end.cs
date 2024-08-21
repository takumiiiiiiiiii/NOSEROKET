using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domino_end : MonoBehaviour
{
    AudioSource audiosorce;
    // Start is called before the first frame update
    void Start()
    {
        audiosorce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GoToTitle.ending_start)
        {

            audiosorce.Stop();
        }
    }
}
