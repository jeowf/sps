using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    #region Inspector

    public float movementSpeed = 340f;
    public float maxSpeed = 3f;
    public float groundCheckDistance = 0.2f;
    public float jumpForce = 1f;

    public GameObject bullet;
    public float bulletSpeed = 1f;

    public int maxHP = 100;

    public Text hpText;

    #endregion

    #region Private Members

    private SpriteRenderer _sr;
    private Animator _anim;
    private CapsuleCollider2D _capsule;
    private Rigidbody2D _rb;
    private AudioSource _audio;

    private float _horizontal;
    private float _vertical;

    private bool _flip = false;
    private bool _grounded = false;
    private bool _jump = false;
    private bool _canShoot = true;

    private int _currentHP;
    #endregion

    #region Monobehaviour Methods
    void Awake()
    {

        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _capsule = GetComponent<CapsuleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();

        _currentHP = maxHP;
        hpText.text = _currentHP.ToString();

    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");


        if (_horizontal < 0 && !_flip)
        {
            _sr.flipX = true;
            _flip = true;
        }
        else if (_horizontal > 0 && _flip)
        {
            _sr.flipX = false;
            _flip = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _anim.SetBool("Shooting", true);

            StartCoroutine(Shoot(0.2f));

            
        }
        else
        {
            _anim.SetBool("Shooting", false);

        }

        Vector3 groundCheck = transform.position;
        groundCheck.y -= groundCheckDistance;
        Debug.DrawLine(transform.position, groundCheck, Color.red);
        
        _grounded = Physics2D.Linecast(transform.position, groundCheck, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetKeyDown(KeyCode.W) && _grounded)
        {
            _jump = true;
        }

    
    }

    void FixedUpdate()
    {

        if (_horizontal * _rb.velocity.x < maxSpeed)
            _rb.AddForce(Vector2.right * _horizontal * movementSpeed);

        if (Mathf.Abs(_rb.velocity.x) > maxSpeed)
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * maxSpeed, _rb.velocity.y);

        if (Mathf.Abs(_horizontal) == 0)
        {
            Vector3 stopHorizontal = _rb.velocity;
            stopHorizontal.x = 0;
            _rb.velocity = stopHorizontal;
        }

        if (_grounded && !_jump)
        {
            Vector3 stopVertical = _rb.velocity;
            stopVertical.y = 0;
            _rb.velocity = stopVertical;
        } else if (_jump)
        {
            _rb.AddForce(new Vector2(0f, jumpForce));
            _jump = false;
        }


        _anim.SetFloat("Speed", Mathf.Abs(_horizontal));
        _anim.SetFloat("HorizontalVelocity", Mathf.Abs( _rb.velocity.x));
        _anim.SetFloat("VerticalVelocity", _rb.velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _currentHP -= 1;
            hpText.text = _currentHP.ToString();
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.DestroyEnemy();

            if (_currentHP == 0)
            {
                SceneManager.LoadScene("Gameover");
            }
        }
    }

    #endregion

    #region Private Methods

    private IEnumerator Shoot(float waitTime)
    {
        if (_canShoot)
        {
            _canShoot = false;

            GameObject blt = GameObject.Instantiate(bullet) as GameObject;
            blt.transform.position = transform.position;
            Projectile projectile = blt.GetComponent<Projectile>();
            int direction = _flip ? -1 : 1;
            projectile.Shoot(direction * bulletSpeed);

            _audio.Play(0);

            yield return new WaitForSeconds(waitTime);

            

            _canShoot = true;

        }
    }

    #endregion

    #region Public Methods

    public float GetPlayerVelocity()
    {
        return _rb.velocity.x;
    }

    #endregion

}
