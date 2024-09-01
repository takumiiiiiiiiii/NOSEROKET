using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen_Score : MonoBehaviour
{
    [SerializeField] private GameObject pollen_point;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(pollen_point, this.transform.position, Quaternion.identity);//花粉を置く
    }
}
