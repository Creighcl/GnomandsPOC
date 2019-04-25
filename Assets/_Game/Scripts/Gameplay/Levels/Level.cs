using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    public List<Wave> waves;
    public float waveIntervalSeconds;

    public void SayHello()
    {
        Debug.Log("HI!");
    }
}
