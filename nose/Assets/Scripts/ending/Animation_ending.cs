using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_ending : MonoBehaviour
{
    public static Animator anima_end;
    public static bool anima_start_end=false;
    /*
    RectTransform rectTransform;
    Vector2 pos;
    */
    AudioSource audiosorce;
    public AudioClip result;



    // Start is called before the first frame update
    void Start()
    {
        anima_end = gameObject.GetComponent<Animator>();
        anima_start_end = true;
        //pos = GetComponent<RectTransform>().anchoredPosition;
        audiosorce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DominoStart.end_start)
        {

        }       
    }

    public void OnAnimationEnd()
    {
        audiosorce.PlayOneShot(result);
    }
}
