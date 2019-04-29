using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chuck_detect : MonoBehaviour
{
    
    void Start()
    {
        Debug.Log(GetComponent<SpriteRenderer>().bounds.size.x.ToString("0.000"));
        Debug.Log((GetComponent<SpriteRenderer>().bounds.center.x * 2) + " MAP WIDTH");
    }

    
    void Update()
    {
        
    }
}
