﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    [SerializeField] BoxCollider2D blastRadius = null;
    [SerializeField] BoxCollider2D secondaryBlastRadius = null;

    private void Start()
    {
        TurretSceneManager.Instance.OnPlayerAttack += HandlePlayerAttack;
    }

    private void OnDestroy()
    {
        if (TurretSceneManager.Instance != null)
        {
            TurretSceneManager.Instance.OnPlayerAttack -= HandlePlayerAttack;
        }
    }
    public void HandlePlayerAttack()
    {
        var a = new ContactFilter2D();
        string[] layers = { "Enemy", "EnemyProjectile" };
        a.SetLayerMask(LayerMask.GetMask(layers));
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(blastRadius, a, results);
        if (secondaryBlastRadius.enabled)
        {
            List<Collider2D> resultsSecondaryCamera = new List<Collider2D>();
            Physics2D.OverlapCollider(secondaryBlastRadius, a, resultsSecondaryCamera);
            results.AddRange(resultsSecondaryCamera);
        }

        if (results.Count > 0)
        {
            results.ForEach(o => {
                Destructible destructibleObject = o.GetComponent<Destructible>();
                if (destructibleObject != null) {
                    destructibleObject.TakeDamage(5);
                }
            });
            }
    }
}
