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
        Debug.Log(_rb2d.velocity.y + " " + _movementSpeed);
        if (_rb2d.velocity.x < (_movementDirection.x * _movementSpeed) || _rb2d.velocity.y > (_movementDirection.y * _movementSpeed))
        {
            _rb2d.AddForce(_movementDirection * (_movementSpeed * 25) * Time.deltaTime, ForceMode2D.Force);
        } else
        {
            Debug.Log("THROTTLE");
        }
    }
}
