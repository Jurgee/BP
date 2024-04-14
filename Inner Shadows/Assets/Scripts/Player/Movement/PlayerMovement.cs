using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask platformLayer;

    private Rigidbody2D body;
    private Animator animator;
    public bool grounded;
    public bool edge_1;
    public bool edge_2;
    private bool not_jump;
    public bool is_on_platform;
    public bool inWater;

    public float climbSpeed;
    public bool Aleft = false;
    public bool Dright = true;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        climbSpeed = 15f;

    }
    private void Update()
    {
        float horizontal_input = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontal_input * speed, body.velocity.y);

        //flip player left-right
        if(horizontal_input > 0.01f)
        {
            transform.localScale = Vector3.one;
            Dright = true;
            Aleft = false;
        }
        else if(horizontal_input < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Aleft = true;
            Dright = false;

        }


        if(Input.GetKey(KeyCode.Space))
        {
            if (IsGrounded()  || IsOnPlatform()) 
            {
                Jump();
            }
        }

        animator.SetBool("walk", horizontal_input != 0);
        animator.SetBool("grounded", not_jump);
        animator.SetFloat("yVelocity", body.velocity.y);

        //is falling
        if ((grounded || is_on_platform  || (edge_1 || edge_2)) && body.velocity.y < 0)
        {
            animator.SetTrigger("jump");
        }

        if (IsWalled() && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            body.velocity = new Vector2(body.velocity.x, climbSpeed);
        }


    }
    void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        not_jump = false;
        grounded = false;
        edge_1 = false;
        edge_2 = false;
        is_on_platform = false;
        animator.SetTrigger("jump");
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        body.freezeRotation = true; //stop rotating if collied

        if (collision.gameObject.tag == "Ground" )
        {
            not_jump = true;
            grounded = true;
            edge_1 = false;
            edge_2 = false;
            is_on_platform = false;
            inWater = false;
            

        }
        if(collision.gameObject.tag == "Edge_1")
        {
            not_jump = true;
            edge_1 = true;
            edge_2 = false;
            grounded = false;
            is_on_platform = false;

           
        }
        if (collision.gameObject.tag == "Edge_2")
        {
            not_jump = true;
            edge_1 = false;
            edge_2 = true;
            grounded = false;
            is_on_platform = false;

            
        }
        if (collision.gameObject.tag == "Platform")
        {
            not_jump = true;
            is_on_platform = true;
            grounded = false;
            edge_1 = false;
            edge_2 = false;
        }

       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ocean"))
        {
            not_jump = true;
            inWater = true;
        }
        if (other.gameObject.tag == "Edge_1")
        {
            not_jump = true;
            edge_1 = true;
            edge_2 = false;
            grounded = false;
            is_on_platform = false;


        }
        if (other.gameObject.tag == "Edge_2")
        {
            not_jump = true;
            edge_1 = false;
            edge_2 = true;
            grounded = false;
            is_on_platform = false;


        }
        
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

    }
    public bool IsOnPlatform()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, platformLayer);
    }
}
