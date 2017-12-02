using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlatformerController : MonoBehaviour
{
    [Header("Settings")]
    public bool doubleJumpEnable = false;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private bool grounded = false;
    private bool doubleJumped = false;
    private bool canDoubleJump = false;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var h = Input.GetAxis("Horizontal");
	    var jump = Input.GetButtonDown("Jump");
        Move(h);
	    Jump(jump);
	}

    void Move(float h)
    {
        var move = new Vector2(h * Time.deltaTime * moveSpeed, 0);
        rb.transform.Translate(move);
    }

    void Jump(bool jump)
    {
        CheckGrounded();
        if (jump)
        {
            if (grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0,jumpForce));
                canDoubleJump = true;
            }
            else
            {
                if (!doubleJumpEnable) return;
                if (canDoubleJump && !doubleJumped)
                {
                    canDoubleJump = false;
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(new Vector2(0, jumpForce));
                }
            }
        }
    }

    void CheckGrounded()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, groundMask);
        if (grounded) doubleJumped = false;
    }
}
