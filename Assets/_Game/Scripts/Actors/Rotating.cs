using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField] float spinStrength = 1f;

    void Update()
    {
        transform.Rotate(0f, 0f, spinStrength * Time.deltaTime);
    }
}
