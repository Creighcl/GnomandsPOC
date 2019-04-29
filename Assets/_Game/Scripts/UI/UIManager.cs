using UnityEngine;
using System.Collections;
using System;

public class UIManager : Singleton<UIManager>
{
    void Start()
    {
        ClearUICanvasses();
        TurretSceneManager.Instance.OnLevelStart += HandleLevelStart;
        TurretSceneManager.Instance.OnPlayerVictory += HandlePlayerVictory;
        TurretSceneManager.Instance.OnPlayerDefeat += HandlePlayerDefeat;
    }

    void OnDestroy()
    {
        TurretSceneManager.Instance.OnLevelStart -= HandleLevelStart;
        TurretSceneManager.Instance.OnPlayerVictory -= HandlePlayerVictory;
        TurretSceneManager.Instance.OnPlayerDefeat -= HandlePlayerDefeat;
    }

    private void HandlePlayerDefeat()
    {
        SwitchToUIOverlayByName(UIOverlays.Defeat.ToString());
    }

    private void HandlePlayerVictory()
    {
        SwitchToUIOverlayByName(UIOverlays.Victory.ToString());
    }

    private void HandleLevelStart(Level level)
    {
        SwitchToUIOverlayByName(UIOverlays.Combat.ToString());
    }

    private void SwitchToUIOverlayByName(string uiOverlayName)
    {
        ClearUICanvasses();
        AddOverlayByName(uiOverlayName);
    }

    private void AddOverlayByName(string uiOverlayName)
    {
        GameObject overlay = ResourceLoader.GetUIOverlayByName(uiOverlayName);
        if (overlay != null)
        {
            Instantiate(overlay, Vector3.zero, Quaternion.identity, GameObject.Find("_UI")?.transform);
        }
    }

    private void ClearUICanvasses()
    {
        var canvasses = GameObject.FindGameObjectsWithTag(CustomGameObjectTags.UIOverlay.ToString());
        foreach (var canvas in canvasses)
        {
            Destroy(canvas);
        }
    }
}
