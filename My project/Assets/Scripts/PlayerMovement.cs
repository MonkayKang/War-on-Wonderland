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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with the power-up
        if (collision.gameObject.CompareTag("SpeedUp"))
        {
            StartCoroutine(SpeedBoostCoroutine());
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        // Boost speed
        speed = boostedSpeed;

        // Wait for the specified duration (10 seconds)
        yield return new WaitForSeconds(speedBoostDuration);

        // Reset speed to normal after the boost duration
        speed = normalSpeed;
    }
}
