using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeLeftText : MonoBehaviour
{
    Text _myTextComponent;

    void Start()
    {
        _myTextComponent = GetComponent<Text>();
        TurretSceneManager tsm = TurretSceneManager.Instance;
        tsm.onTimeLeftChange += UpdateTimeLeft;
        UpdateTimeLeft(tsm.GetTimeLeft());
    }

    private void UpdateTimeLeft(float unformattedTimeLeft)
    {
        _myTextComponent.text = TurretSceneManager.Instance.GetFormattedTimeLeft();
    }
}
