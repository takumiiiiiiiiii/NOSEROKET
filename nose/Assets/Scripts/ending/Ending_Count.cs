using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class Ending_Count : MonoBehaviour
{
    public static int tree=0;
    public static int current_score;
    public Text scoreText;
    void Start()
    {
        tree = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = string.Format("{0}"+"本", tree);
    }

}

