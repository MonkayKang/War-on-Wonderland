using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiers1 : MonoBehaviour
{

    public Transform player; // Assign the player 
    public float followSpeed = 3f;

    public string Name1 = "Clone1";
    public string Name2 = "Clone2";
    public string Name3 = "Clone4";

    public float stopDistance = 1f; // Distance to stop near the player

    private GameObject soldierPrefab; // Assign this in the Inspector
    private BoxCollider2D boxCollider;

    public AudioClip clip;
    public AudioClip clip2;
    public AudioSource source;

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
            source.PlayOneShot(clip);
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
                source.PlayOneShot(clip2);
                Destroy(target);
            }
            else if (chance >= 0.6f)
            {
                source.PlayOneShot(clip2);
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
                source.PlayOneShot(clip2);
                Destroy(target);
            }
            else if (chance >= 0.4f)
            {
                source.PlayOneShot(clip2);
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
        Instantiate(soldierPrefab, original.transform.position, Quaternion.identity);
    }
}
