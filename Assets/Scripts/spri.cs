using UnityEngine;

public class spri : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float jumpForce = 6f;
    private float velocity = 0;
    public float gravity = 20f;
    private float jumpHeight = 0;
    bool isJumping = false;
    public Transform playerSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.linearVelocity = new Vector2(0, speed);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.linearVelocity = new Vector2(0, -speed);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            velocity = jumpForce;
        }
        if (isJumping)
        {
            playerSprite.localPosition = new Vector3(playerSprite.localPosition.x, jumpHeight, playerSprite.localPosition.z);
            velocity -= gravity * Time.deltaTime;
            jumpHeight += velocity * Time.deltaTime;
            if (jumpHeight <= 0)
            {
                jumpHeight = 0;
                velocity = 0;
                isJumping = false;
            }
        }
    }
}
