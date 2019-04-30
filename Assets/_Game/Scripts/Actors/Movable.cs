using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movable : MonoBehaviour
{
    [SerializeField] float _movementSpeed = .4f;
    [SerializeField] Vector2 _movementDirection = Vector2.down;
    Rigidbody2D _rb2d;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();   
    }

    void Update()
    {
        _rb2d.AddForce(_movementDirection * 50f * Time.deltaTime, ForceMode2D.Force);
        _rb2d.velocity = Vector3.ClampMagnitude(_rb2d.velocity, _movementSpeed);
    }
}
