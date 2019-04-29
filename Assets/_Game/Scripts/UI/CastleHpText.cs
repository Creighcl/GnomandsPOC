using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CastleHpText : MonoBehaviour
{
    Text _myTextComponent;

    void Start()
    {
        _myTextComponent = GetComponent<Text>();
        TurretSceneManager tsm = TurretSceneManager.Instance;
        tsm.OnCastleHealthChange += UpdateHp;
        UpdateHp(tsm.GetCastleHp(), tsm.GetCastleMaxHp());
    }

    void OnDestroy()
    {
        if (TurretSceneManager.Instance != null)
        {
            TurretSceneManager.Instance.OnCastleHealthChange -= UpdateHp;
        }
    }

    private void UpdateHp(int current, int max)
    {
        _myTextComponent.text = TurretSceneManager.Instance.GetCastleHpPercentString() + "%";
    }
}
