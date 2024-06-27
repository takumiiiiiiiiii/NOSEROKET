using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public int pollenPoint;
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

    // Update is called once per frame
    private void UpdateSlider()
    {
        if(slider != null)
        {
            slider.value = pollenPoint;
        }
    }
}
