using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiers3 : MonoBehaviour
{
    private static int cloneCount = 0;
    public int maxClones = 3;

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
            StopAllCoroutines();
            StartCoroutine(DestroyOrClone(collision.gameObject, .2f));
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Name1) || collision.gameObject.CompareTag(Name2) || collision.gameObject.CompareTag(Name3))
        {
            followSpeed = 0.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Name1) || collision.gameObject.CompareTag(Name2) || collision.gameObject.CompareTag(Name3))
        {
            StopAllCoroutines();
            StartCoroutine(DestroyOrClone2(collision.gameObject, .2f));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        followSpeed = 2f;
    }

    private IEnumerator DestroyOrClone(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (target != null)
        {
            float chance = Random.value; // Random float between 0 and 1

            if (chance <= 0.1f)
            {
                CloneSoldier(target);
                Destroy(target);
            }
            else if (chance >= 0.6f)
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

            if (chance <= 0.20f)
            {
                CloneSoldier(target);
                Destroy(target);
            }
            else if (chance >= 0.4f)
            {
                Destroy(target);
            }
            else
            {
                followSpeed = -30f;
            }
        }
    }

    private void CloneSoldier(GameObject original)
    {
        if (cloneCount >= maxClones) return; // Prevent excessive cloning

        Instantiate(soldierPrefab, original.transform.position, Quaternion.identity);
        cloneCount++;
    }
}
