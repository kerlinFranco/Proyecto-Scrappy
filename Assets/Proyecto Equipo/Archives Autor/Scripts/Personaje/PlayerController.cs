using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public float jumpForce;
    private bool doubleJump;

    public Rigidbody2D theRB;

    private Animator anim;
    private SpriteRenderer theSR;


    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    private bool isGrounded;
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
        
    }

    
    void Update()
    {
        theRB.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), theRB.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if (isGrounded)
        {
            doubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);

            }else 
            {
                if(doubleJump)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    doubleJump = false;

                }
            }
            
        }
        if (theRB.velocity.x < 0)
        {
            theSR.flipX= true;

        }else if (theRB.velocity.x > 0)
        {
            theSR.flipX= false;
        }
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
      

    }
}
