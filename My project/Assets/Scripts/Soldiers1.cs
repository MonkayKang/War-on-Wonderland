using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiers1 : MonoBehaviour
{
    public Transform player; // Assign the player 
    public float followSpeed = 3f;

    public string Name1 = "Name1";
    public string Name2 = "Name2";
    public string Name3 = "Name3";

    public float stopDistance = 1f; // Distance to stop near the player

    private GameObject soldierPrefab; // Assign this in the Inspector
    private BoxCollider2D boxCollider;

    private void Start()
    {
        soldierPrefab = gameObject;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > stopDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.Lerp(transform.position, player.position, followSpeed * Time.deltaTime);

            // Flip object and hitbox based on movement direction
            if (direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Name1) || collision.gameObject.CompareTag(Name2) || collision.gameObject.CompareTag(Name3))
        {
            StartCoroutine(DestroyOrClone(collision.gameObject, 0.1f)); ;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Name1) || collision.gameObject.CompareTag(Name2) || collision.gameObject.CompareTag(Name3))
        {
            followSpeed = 0.2f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Name1) || collision.gameObject.CompareTag(Name2) || collision.gameObject.CompareTag(Name3))
        {
            Instantiate(gameObject);
        }
    }

    private IEnumerator DestroyOrClone(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (target != null)
        {
            float chance = Random.value; // Random float between 0 and 1

            if (chance <= 0.2f)
            {
                CloneSoldier(target);
                Destroy(target);
            }
            else if (chance >= 0.8f)
            {
                Destroy(target);
            }
            else
            {
                followSpeed = -30f;
            }
        }
    }

    private IEnumerator DestroyOrClone2(GameObject target, float delay) // If Surrounded
    {
        yield return new WaitForSeconds(delay);

        if (target != null)
        {
            float chance = Random.value; // Random float between 0 and 1

            if (chance <= 0.05f)
            {
                CloneSoldier(target);
            }
            else if (chance >= 0.9f)
            {
                Destroy(target);
            }
            else
            {
                StopAllCoroutines();
            }
        }
    }

    private void CloneSoldier(GameObject original)
    {
        Instantiate(soldierPrefab, original.transform.position, Quaternion.identity);
    }
}
