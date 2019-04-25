using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UI_TimeLeftText : MonoBehaviour
{
    Text _myTextComponent;

    void Start()
    {
        _myTextComponent = GetComponent<Text>();
        TurretSceneManager tsm = TurretSceneManager.instance;
        tsm.onTimeLeftChange += UpdateTimeLeft;
        UpdateTimeLeft(tsm.GetTimeLeft());
    }

    private void UpdateTimeLeft(float unformattedTimeLeft)
    {
        _myTextComponent.text = TurretSceneManager.instance.GetFormattedTimeLeft();
    }
}
