using UnityEngine;
using System.Collections;

public class ResourceLoader
{
    public const string UI_OVERLAY_RESOURCE_PATH = "Prefabs/UI/Overlays";
    public const string AUDIO_CLIP_RESOURCE_PATH = "Sounds";
    public const string LEVEL_RESOURCE_PATH = "Levels";
    public const string STAGE_RESOURCE_PATH = "Prefabs/Backgrounds";

    static public Level GetLevelByName(string levelName)
    {
        Object obj = Resources.Load(LEVEL_RESOURCE_PATH + "/" + levelName);
        return (Level) obj;
    }

    static public AudioClip GetAudioClipByName(string clipName)
    {
        return (AudioClip)Resources.Load(AUDIO_CLIP_RESOURCE_PATH + "/" + clipName);
    }
    
    static public GameObject GetUIOverlayByName(string name)
    {
        return (GameObject)Resources.Load(UI_OVERLAY_RESOURCE_PATH + "/" + name);
    }

    static public GameObject GetBackgroundByName(string name)
    {
        return (GameObject)Resources.Load(STAGE_RESOURCE_PATH+"/"+name);
    }
}
