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
    public AudioClip[] result;

    public Ranking Rk;



    // Start is called before the first frame update
    void Start()
    {
        anima_end = gameObject.GetComponent<Animator>();
        anima_start_end = true;
        //pos = GetComponent<RectTransform>().anchoredPosition;
        audiosorce = GetComponent<AudioSource>();
        Rk = GameObject.Find("Ranking").GetComponent<Ranking>();
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
        Debug.Log("Ranking;" + Rk.ranknumber);
        if(Rk.ranknumber  == 1)
        {
            audiosorce.PlayOneShot(result[2]);
        }
        else if(Rk.ranknumber < 6 && Rk.ranknumber != -1)
        {
            audiosorce.PlayOneShot(result[1]);
        }
        else
        {
            audiosorce.PlayOneShot(result[0]);
        }
    }
}
