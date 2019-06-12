using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class P2MovementController : MonoBehaviour
{
    //Variables
    //move + jump
    public float movementSpeed;
    public float jumpForce;
    public float moveInput;

    private bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    //Animation
    private Rigidbody2D rb;
    public Animator animator;
    public UnityEvent onLandEvent;

    //attack
    private bool attack1;
    private bool attack2;
    private bool attack3;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public float damage;
    public float health;
    private float knockBackStr = 1;
    //knockback
    public Rigidbody2D enemyRB;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (onLandEvent == null)
            onLandEvent = new UnityEvent();
    }

    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        //knockback
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //check if player touching ground 
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }
        if (Input.GetKeyDown(KeyCode.I) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            animator.SetBool("isJumping", true);
        }
        else if (Input.GetKeyDown(KeyCode.I) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;


        }

        handleInput1();

    }

    //Methods
    void FixedUpdate()
    {

        moveInput = Input.GetAxisRaw("Horizontal_P2");

        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveInput));

        }

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        bool wasGrounded = isGrounded;
        isGrounded = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, checkRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                    onLandEvent.Invoke();
            }
        }

        resetValues1();

    }



    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

  
    private void handleInput1()
    {
        if (Input.GetButtonDown("Fire1_P2"))
        {
            attack1 = true;
        }
        if (attack1 == true)
        {
            animator.SetTrigger("Attack");
            rb.velocity = Vector2.zero;

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<P1MovementController>().TakeDamage(damage);

            }
        }
    }


    private void resetValues1()
    {
        if (Input.GetButtonUp("Fire1_P2"))
        {
            attack1 = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);

    }

    public void TakeDamage(float damage)
    {

        health += damage;
        rb.velocity = new Vector2(moveInput * health * knockBackStr, health * knockBackStr);

        Debug.Log("damage taken !");
    }



}
