using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerapatinko : MonoBehaviour
{
    public Animator anima;
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
        anima.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public  void CameraPatinkoAnime(bool animationcontroler)
    {
        Animator anima;
        anima = GetComponent<Animator>();
        anima.enabled = animationcontroler;
        CameraPos camera = GetComponent<CameraPos>();
        camera.enabled = !animationcontroler;
        anima.SetBool("Paticamera", animationcontroler);
    }
    public void CameraPatinkoAnimeEnd()
    {
        CameraPos camera = GetComponent<CameraPos>();
        camera.enabled = true;
        Animator anima;
        anima = GetComponent<Animator>();
        anima.enabled = false;
    }
}
