using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Public variables
    public float maxSpeed = 10.0f;
    public float jumpForce = 120.0f;
    public Transform groundCheck;
    public LayerMask defineGround;
    public string horizontalAxis, jumpAxis; // e.g. "Jump_Kirby"
    public float groundRadius = 0.6f;

    // Private variables
    private Rigidbody2D rBody;
    private SpriteRenderer sRend;
    private Animator animator;

    private bool isGrounded = false;

	// Use this for initialization
	void Start () {
        rBody = this.GetComponent<Rigidbody2D>();
        sRend = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
	}

    void Update()
    {
        if (Input.GetAxis(jumpAxis) > 0 && isGrounded)
        {
            animator.SetBool("Ground", isGrounded);
            rBody.AddForce(new Vector2(0, jumpForce));
        }
    }
	
	// Used for physics calculations
	void FixedUpdate () {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, defineGround);
        // Debug.Log("Grounded? " + isGrounded);
        animator.SetBool("Ground", isGrounded);

        animator.SetFloat("vSpeed", rBody.velocity.y);

        float moveHoriz = Input.GetAxis(horizontalAxis);

        // Pass horizontal velocity to animator (SPEED)
        animator.SetFloat("Speed", Mathf.Abs(moveHoriz));

        // Debug.Log("Move Horizontal: " + moveHoriz);
        rBody.velocity = new Vector2(moveHoriz * maxSpeed, rBody.velocity.y);

        if(moveHoriz > 0)
        {
            sRend.flipX = false;
        } else if (moveHoriz < 0)
        {
            sRend.flipX = true;
        }
	}
}
