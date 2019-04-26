using System;
using UnityEngine;

[Serializable]
public class StageElementCreationTemplate
{
    public StageElementType ElementType;
    public GameObject Prefab;
    public Vector3 Position;
    public float DelaySeconds;
}