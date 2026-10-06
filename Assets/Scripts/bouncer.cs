using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using System;

public class bouncer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    public float speed = 5.0f;
    private Transform player;
    private bool isWaiting = false;
    static bool isRunning;
    static bool canMove;
    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canMove = true;
        isRunning = true;
        rb = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<player>().transform;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (!isWaiting)
            {
                // Checks if the player has jumped over the bouncer, meaning the bouncer must chase the player in the opposite direction
                if (player.position.y > transform.position.y && player.position.x - transform.position.x < 1)
                {
                    StartCoroutine(startWaiting());
                }
                else
                {
                    // If the player starts to the right of the bouncer, the bouncer will move right, and vise versa.
                    if (player.position.x > transform.position.x)
                    {
                        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
                    }
                    else
                    {
                        rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
                    }
                }
                if (Math.Abs(transform.position.x - player.position.x) <= 2 && Math.Abs(transform.position.y - player.position.y) <= 1.5)
                {
                    animator.SetBool("isClose", true);
                }
                else
                {
                    animator.SetBool("isClose", false);
                }
            }
        }
        else
        {
            return;
        }
    }
    // Starts the process of the small turn delay before chasing the player in the opposite direction.
    IEnumerator startWaiting()
    {
        if (isRunning)
        {
            isWaiting = true;
            yield return new WaitForSecondsRealtime(0.6f);
            if (player.position.x < transform.position.x)
            {
                StartCoroutine(chaseLeft());
            }
            else
            {
                StartCoroutine(chaseRight());
            }
        }
    }
    // Chases player left
    IEnumerator chaseLeft()
    {
        while (isRunning && canMove && (player.position.x < transform.position.x || player.position.y <= transform.position.y))
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
            sr.flipX = true;
            if (player.position.x - transform.position.x <= 2)
            {
                animator.SetBool("isClose", true);
            }
            else
            {
                animator.SetBool("isClose", false);
            }
                yield return null;
        }
        StartCoroutine(startWaiting());
    }
    // Chases player right
    IEnumerator chaseRight()
    {
        while (isRunning && canMove && (player.position.x > transform.position.x | player.position.y <= transform.position.y))
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
            sr.flipX = false;
            if (player.position.x - transform.position.x <= 2)
            {
                animator.SetBool("isClose", true);
            }
            else
            {
                animator.SetBool("isClose", false);
            }
            yield return null;
        }
        StartCoroutine(startWaiting());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Person"))
            {
                isRunning = false;
                if (collision.transform.position.x > transform.position.x)
                {
                    rb.linearVelocity = new Vector2(-5f, 0f);
                }
                else
                {
                    rb.linearVelocity = new Vector2(5f, 0f);
                }
                Invoke("Jump", 0.3f);
                Invoke("EnableMovement", 1f);
            }
    }
    // Ensures bouncer goes back to original movement script after jumping over person.
    void EnableMovement()
    {
        isRunning = true;
        isWaiting = false;
    }
    void Jump()
    {
        rb.linearVelocity = new Vector2(0f, 8f);
    }
}
