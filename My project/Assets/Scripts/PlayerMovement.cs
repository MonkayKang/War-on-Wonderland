using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedBoostDuration = 10f;
    public float boostedSpeed = 25f;
    public float normalSpeed = 15f;

    public float speed = 5f;
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator; // Reference to the Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Initialize Animator
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw(horizontalAxis);
        float moveY = Input.GetAxisRaw(verticalAxis);
        Vector2 movement = new Vector2(moveX, moveY).normalized;
        rb.velocity = movement * speed;

        // Flip sprite based on movement direction
        if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Set animation parameter
        animator.SetBool("isMoving", movement.magnitude > 0);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SpeedUp"))
        {
            StartCoroutine(SpeedBoostCoroutine());
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        speed = boostedSpeed;
        yield return new WaitForSeconds(speedBoostDuration);
        speed = normalSpeed;
    }
}

