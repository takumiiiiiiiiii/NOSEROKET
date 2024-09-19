using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muteplayBGM : MonoBehaviour
{
    public AudioSource audioSource;
    private bool backBGMmuted = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void muteFlag(bool BGMmute)
    {
        if (BGMmute && !backBGMmuted)
        {
            audioSource.volume = 0.0f;
            backBGMmuted = true;
        }
        else
        {
            audioSource.volume = 1.0f;
            backBGMmuted = false;
        }
    }
}
