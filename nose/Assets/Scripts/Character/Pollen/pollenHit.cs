using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class pollenHit : MonoBehaviour
{
    [SerializeField] private GameObject pollen_point;
    [SerializeField] private CircleCollider2D Cc;
    [SerializeField] private Rigidbody2D Rb;

    public Score Sc;
    public SliderController Sdc;


    // Start is called before the first frame update
    void Start()
    {
        Sc = GameObject.Find("Text (TMP) (1)").GetComponent<Score>();
        Sdc = GameObject.Find("dash_slider").GetComponent<SliderController>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"&&Sc!=null)
        {
            Instantiate(pollen_point, this.transform.position, Quaternion.identity);
            Sc.score += 100;
            Sdc.CollectObject();
            Destroy(this.gameObject);
            
        }
    }
}
