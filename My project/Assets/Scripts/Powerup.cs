using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public bool isClone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isClone)
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }

}
