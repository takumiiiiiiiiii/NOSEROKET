using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public int pollenPoint;
    public int pollenReleaseRate;
    // Start is called before the first frame update
    void Start()
    {
        if(slider != null)
        {
            slider.value = 0;
            pollenPoint = 0;
        }
    }

    public void CollectObject()
    {
        pollenPoint++;
        UpdateSlider();
    }

    public void EmittingObject()
    {
        pollenPoint -= pollenReleaseRate;
        UpdateSlider();
    }

    // Update is called once per frame
    private void UpdateSlider()
    {
        if(slider != null)
        {

            slider.value = pollenPoint;
        }
    }
}
