using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dump : MonoBehaviour
{
    [SerializeField] Wave wave;

    void Start()
    {
        StartCoroutine(InASec());
    }

    IEnumerator InASec()
    {
        yield return new WaitForSeconds(1);
        if (wave != null)
        {
            wave.Start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
