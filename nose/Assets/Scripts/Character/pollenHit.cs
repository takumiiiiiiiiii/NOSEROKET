using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class pollenHit : MonoBehaviour
{
    [SerializeField] private CircleCollider2D Cc;
    [SerializeField] private Rigidbody2D Rb;

    public Score Sc;


    // Start is called before the first frame update
    void Start()
    {
        Sc = GameObject.Find("Text (TMP) (1)").GetComponent<Score>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Sc.score += 100;
            Destroy(this.gameObject);
        }
    }
}
