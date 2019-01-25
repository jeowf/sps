using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{

    private Rigidbody2D _rb;
    private CircleCollider2D _circle;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _circle = GetComponent<CircleCollider2D>();
    }

    public void Shoot(float horizontalForce)
    {
        _rb.AddForce(Vector2.right * horizontalForce);

        Destroy(gameObject, 1f);
    }

}
