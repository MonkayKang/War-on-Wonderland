using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public static int player1Score;
    public static int player2Score;
    public static int player3Score;
    public static int player4Score;

    // Start is called before the first frame update
    void Start()
    {
        player1Score = 0;
        player2Score = 0;
        player3Score = 0;
        player4Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Clone1"))
        {
            Destroy(collision.gameObject);
            player1Score += 1;
        }
        if (collision.gameObject.CompareTag("Clone2"))
        {
            Destroy(collision.gameObject);
            player2Score += 1;
        }
        if (collision.gameObject.CompareTag("Clone3"))
        {
            Destroy(collision.gameObject);
            player3Score += 1;
        }
        if (collision.gameObject.CompareTag("Clone4"))
        {
            Destroy(collision.gameObject);
            player4Score += 1;
        }
    }
}
