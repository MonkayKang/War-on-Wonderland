using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0; // Pause the game at start
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ON()
    {
        Time.timeScale = 1;
    }
}
