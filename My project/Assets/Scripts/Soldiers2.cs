using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiers2 : MonoBehaviour
{
    public Transform player; // Assign the player 
    public float followSpeed = 3f;

    public string Name1 = "Name1";
    public string Name2 = "Name2";
    public string Name3 = "Name3";
    private int hitcount = 0;

    public float stopDistance = 1f; // Distance to stop near the player

    private GameObject soldierPrefab; // Assign this in the Inspector
    private void Start()
    {
        soldierPrefab = gameObject;
    }
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > stopDistance)
        {
            transform.position = Vector2.Lerp(transform.position, player.position, followSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Name1) || collision.gameObject.CompareTag(Name2) || collision.gameObject.CompareTag(Name3))
        {
            hitcount++;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (hitcount < 2)
        {
            if (collision.gameObject.CompareTag(Name1) || collision.gameObject.CompareTag(Name2) || collision.gameObject.CompareTag(Name3))
            {
                followSpeed = .0f;
                StartCoroutine(DestroyOrClone(collision.gameObject, 0.1f));
            }
        }
        if (hitcount >= 2)
        {
            if (collision.gameObject.CompareTag(Name1) || collision.gameObject.CompareTag(Name2) || collision.gameObject.CompareTag(Name3))
            {
                followSpeed = .0f;
                StartCoroutine(DestroyOrClone2(collision.gameObject, 0.1f));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StopAllCoroutines();
        hitcount = 0;
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
            else if (chance >= 0.8f)
            {
                Destroy(target);
            }
        }
    }

    private IEnumerator DestroyOrClone2(GameObject target, float delay)
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

        }
    }

    private void CloneSoldier(GameObject original)
    {
        Instantiate(soldierPrefab, original.transform.position, Quaternion.identity);
    }
    // ADD A SMALL BOX INFRONT AND THEN FLIP SPRITES
}
