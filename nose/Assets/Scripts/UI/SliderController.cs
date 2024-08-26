using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public int pollenPoint;
    public int pollenReleaseRate;
    //public Color color;


    // Start is called before the first frame update
    void Start()
    {
        if(slider != null)
        {
            slider.value = 0;
            pollenPoint = 0;
            //color = gameObject.GetComponent<Image>().color;
        }
    }

    public void CollectObject()
    {
        pollenPoint++;
        UpdateSlider();
    }

    public bool EmittingObject()
    {
        if (pollenPoint < pollenReleaseRate)
        {
            return false;
        }
        pollenPoint -= pollenReleaseRate;
        UpdateSlider();

        return true;    
    }

    // Update is called once per frame
    private void UpdateSlider()
    {
        if(slider != null)
        {
            slider.value = pollenPoint;
            /*
            if(pollenPoint > 100)
            {
       
                this.image_pollenGauge.color = new Color32(80, 255, 0, 255);
            }
            else if(pollenPoint >= pollenReleaseRate)
            {
                this.image_pollenGauge.color = new Color32(255, 209, 0, 255);
            }
            else
            {
                image_pollenGauge.color = new Color32(255, 80, 0, 255);
            }
            */
        }
    }
}
