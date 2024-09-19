using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseScale : MonoBehaviour
{

    public SliderController Sdc;
    private bool ScaleUpFlag;

    // Start is called before the first frame update
    void Start()
    {
        Sdc = GameObject.Find("dash_slider").GetComponent<SliderController>();
        ScaleUpFlag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Sdc.noseScaleChange && !ScaleUpFlag)
        {
            StartCoroutine("ScaleUp");
            ScaleUpFlag = true;
            Debug.Log("ScaleUp");
        }
        else if (!Sdc.noseScaleChange && ScaleUpFlag)
        {
            StartCoroutine("ScaleDown");
            ScaleUpFlag = false;
            Debug.Log("ScaleDown");
        }
        else
        {
            Debug.Log("Scale ;" + Sdc.noseScaleChange+ "ScaleUp ;" + ScaleUpFlag);
        }
    }

    IEnumerator ScaleUp()
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            this.transform.localScale = new Vector2(0.53f * i + 0.53f, 0.53f * i + 0.53f);
            yield return new WaitForSeconds(0.01f);
        }
       
    }

    IEnumerator ScaleDown()
    {
        for (float i = 1; i >= 0; i -= 0.01f)
        {
            this.transform.localScale = new Vector2(0.53f * i + 0.53f, 0.53f * i + 0.53f);
            yield return new WaitForSeconds(0.01f);
        }
        
    }
}
