using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Wave : ScriptableObject
{
    public List<StageElementCreationTemplate> elementsToCreate;

    public void Start()
    {
        foreach(StageElementCreationTemplate element in elementsToCreate)
        {
            StageManager.Instance.AddElementToStage(element);
        }
    }
}
