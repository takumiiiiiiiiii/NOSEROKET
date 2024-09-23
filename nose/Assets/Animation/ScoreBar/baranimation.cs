using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baranimation : MonoBehaviour
{

    Animator anima;
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void animationStart()
    {
        anima.SetBool("ScoreWait", true);
    }
    public void animationEnd()
    {
        patinko.patinkotime = true;
    }
}