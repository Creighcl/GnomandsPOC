using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButtonClick : MonoBehaviour
{
    public void PlayerAttack()
    {
        TurretSceneManager.Instance.DoPlayerAttack();
    }
}
