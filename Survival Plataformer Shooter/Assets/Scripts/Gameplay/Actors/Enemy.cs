using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public int life = 3;

    public float maxSpeed = 2.5f;
    public float movementSpeed = 200f;

    public GameObject explosionEffect;

    private GameObject _player;
    private bool _flip = false;

    private SpriteRenderer _sr;
    private Animator _anim;
    private CapsuleCollider2D _capsule;
    private Rigidbody2D _rb;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _capsule = GetComponent<CapsuleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {

        int mirror = _flip ? 1 : -1;

        if (_rb.velocity.x * mirror < maxSpeed)
            _rb.AddForce(Vector2.right * mirror * movementSpeed);

        if (Mathf.Abs(_rb.velocity.x) > maxSpeed)
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * maxSpeed, _rb.velocity.y);

       

        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float player_x = _player.transform.position.x;
        float this_x = transform.position.x;



        if (player_x - this_x > 0 && !_flip)
        {
            _sr.flipX = true;
            _flip = true;

        } else if (player_x - this_x < 0 && _flip)
        {
            _sr.flipX = false;
            _flip = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            life -= 1;

            if (life == 0)
                DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        GameObject explosion = GameObject.Instantiate(explosionEffect) as GameObject;
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }

}
