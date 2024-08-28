using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sencerFix : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Nosemove.growlevel += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Nosemove.growlevel -= 0.1f;
        }
        scoreText.text = string.Format("{0}", Nosemove.growlevel);
    }
}
