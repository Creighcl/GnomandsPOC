using UnityEngine;
using System.Collections;
using System;

public class StageManager : Singleton<StageManager>
{
    private const string ACTORS_STAGE_FIND_PATH = "_Stage/OnStage/Actors";
    public const string BACKGROUND_STAGE_FIND_PATH = "_Stage/Background";
    public delegate void GameObjectDelegate(GameObject gameObject);
    public GameObjectDelegate onActorAddedToStage;
    public GameObjectDelegate onElementAddedToStage;

    public void AddElementToStage(StageElementCreationTemplate template)
    {
        if (template.DelaySeconds == 0f)
        {
            createElement(template);
            return;
        }

        StartCoroutine(createElementAfterDelay(template));
    }

    IEnumerator createElementAfterDelay(StageElementCreationTemplate template)
    {
        yield return new WaitForSeconds(template.DelaySeconds);
        createElement(template);
    }

    void createElement(StageElementCreationTemplate template)
    {
        Transform parent = null;

        switch (template.ElementType)
        {
            case StageElementType.ONSTAGE_ACTOR:
                parent = GameObject.Find(ACTORS_STAGE_FIND_PATH)?.transform;
                break;
        }

        if (parent != null)
        {
            Instantiate(template.Prefab, template.Position, Quaternion.identity, parent);
            announceElementAddedToStage(template);
        }
    }

    void announceElementAddedToStage(StageElementCreationTemplate template)
    {
        onElementAddedToStage?.Invoke(template.Prefab);
 
        GameObjectDelegate eventChannel = null;
        switch (template.ElementType)
        {
            case StageElementType.ONSTAGE_ACTOR:
                eventChannel = onActorAddedToStage;
                break;
        }

        if (eventChannel != null)
        {
            eventChannel?.Invoke(template.Prefab);
        }
    }
}
