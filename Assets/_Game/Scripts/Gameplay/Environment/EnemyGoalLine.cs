using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyGoalLine : MonoBehaviour
{
    BoxCollider2D _myBoxCollider;

    void Start()
    {
        _myBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Destroy(otherCollider.gameObject);
        TurretSceneManager.Instance.CastleModifyHitPoints(-5);
    }
}
