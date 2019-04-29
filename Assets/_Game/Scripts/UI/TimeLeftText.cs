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
        tsm.OnTimeLeftChange += UpdateTimeLeft;
        UpdateTimeLeft(tsm.GetTimeLeft());
    }

    private void OnDestroy()
    {
        if (TurretSceneManager.Instance != null)
        {
            TurretSceneManager.Instance.OnTimeLeftChange -= UpdateTimeLeft;
        }
    }

    private void UpdateTimeLeft(float unformattedTimeLeft)
    {
        _myTextComponent.text = TurretSceneManager.Instance.GetFormattedTimeLeft();
    }
}
