using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muteBackBGM : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip backBGM;
    private bool backBGMplayed = true;
    private Nosemove NM;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        NM = GameObject.Find("nose_player").GetComponent<Nosemove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NM.backBGMmute)
        {
            audioSource.Stop();
            backBGMplayed = false;
        }
        else if (!backBGMplayed && !NM.backBGMmute)
        {
            audioSource.PlayOneShot(backBGM);
            backBGMplayed = true;
        }
    }
}
