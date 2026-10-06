using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class player: MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float jumpForce = 6f;
    public Vector2 boxSize;
    // Determines the distance the box is casted down to detect if the player is on the ground or not. This can be changed in the editor.
    public float castDistance;
    // Determines which layers are considered the ground for the player. 
    public LayerMask groundLayer;
    private SpriteRenderer sr;
    private Transform bouncer;
    public Logic logic;
    static bool canMove;
    private Transform person;
    static bool isKnocked;
    [SerializeField] private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canMove = true;
        isKnocked = false;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        person = GameObject.FindGameObjectWithTag("Person").transform;
        bouncer = GameObject.FindGameObjectWithTag("Bouncer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnocked)
        {
            if (!canMove)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }
            float input = Input.GetAxisRaw("Horizontal");
            // Detects whether the player is moving horizontally, and if the player is a running animation is played. If not, an idle animation is played.
            if (input != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
            // Movement script for moving left, right and jumping.
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
                sr.flipX = false;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
                sr.flipX = true;
            }
            else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
            }
            if (Input.GetKeyDown(KeyCode.W) && isGrounded() || Input.GetKeyDown(KeyCode.UpArrow) && isGrounded())
            {
                rb.linearVelocity = new Vector2(speed, jumpForce);
                animator.SetBool("isJumping", true);
                Invoke("NotJumping", 1f);
            }
        }
    }
    void NotJumping()
    {
        animator.SetBool("isJumping", false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Person"))
        {
            isKnocked = true;
            if (collision.transform.position.x > transform.position.x)
            {
                rb.linearVelocity = new Vector2(-8f, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(8f, rb.linearVelocity.y);
            }
            Invoke("EnableMovement", 0.7f);
        }
        if (collision.CompareTag("Door"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bouncer"))
        {
            logic.gameOver();
            canMove = false;
        }
    }
    // Ensures player can move again after being knocked back by bouncing off of a person.
    void EnableMovement()
    {
        isKnocked = false;
    }
    public bool isGrounded() {
        // Casts a box to check if the player is on the ground or not. If the box collides with the ground, the player is grounded, and if not, the player is not grounded.
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void OnDrawGizmos()
    {
        // Draws a box to check if the player is grounded.
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
