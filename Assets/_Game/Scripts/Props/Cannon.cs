using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Cannon : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        TurretSceneManager.Instance.OnPlayerAttack += HandlePlayerAttack;
    }

    private void OnDestroy()
    {
        if (TurretSceneManager.Instance != null) {
            TurretSceneManager.Instance.OnPlayerAttack -= HandlePlayerAttack;
        }
    }

    private void HandlePlayerAttack()
    {
        anim.Play("Explosion", -1, 0f);
    }
}
