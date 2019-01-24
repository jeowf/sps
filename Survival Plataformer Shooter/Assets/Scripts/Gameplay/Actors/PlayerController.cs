using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Inspector

    public float movementSpeed = 340f;
    public float maxSpeed = 3f;
    public float groundCheckDistance = 0.2f;
    public float jumpForce = 1f;

    #endregion

    #region Private Members

    private SpriteRenderer sr;
    private Animator anim;
    private CapsuleCollider2D capsule;
    private Rigidbody2D rb;

    private float _horizontal;
    private float _vertical;

    private bool flip = false;
    private bool grounded = false;
    private bool jump = false;
    

    #endregion

    #region Monobehaviour Methods
    void Awake()
    {

        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
                
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");


        if (_horizontal < 0 && !flip)
        {
            sr.flipX = true;
            flip = true;
        }
        else if (_horizontal > 0 && flip)
        {
            sr.flipX = false;
            flip = false;
        }

        if (Input.GetKey(KeyCode.Space))
            anim.SetBool("Shooting", true);
        else
            anim.SetBool("Shooting", false);

        Vector3 groundCheck = transform.position;
        groundCheck.y -= groundCheckDistance;
        Debug.DrawLine(transform.position, groundCheck, Color.red);
        
        grounded = Physics2D.Linecast(transform.position, groundCheck, 1 << LayerMask.NameToLayer("Ground"));

        Debug.Log(grounded);

        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            jump = true;
        }

    
    }

    void FixedUpdate()
    {

        if (_horizontal * rb.velocity.x < maxSpeed)
            rb.AddForce(Vector2.right * _horizontal * movementSpeed);

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);

        if (Mathf.Abs(_horizontal) == 0)
        {
            Vector3 stopHorizontal = rb.velocity;
            stopHorizontal.x = 0;
            rb.velocity = stopHorizontal;
        }

        if (grounded && !jump)
        {
            Vector3 stopVertical = rb.velocity;
            stopVertical.y = 0;
            rb.velocity = stopVertical;
        } else if (jump)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }


        anim.SetFloat("Speed", Mathf.Abs(_horizontal));
        anim.SetFloat("HorizontalVelocity", Mathf.Abs( rb.velocity.x));
        anim.SetFloat("VerticalVelocity", rb.velocity.y);

    }

    #endregion



}
