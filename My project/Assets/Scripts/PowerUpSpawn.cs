using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Assign objects in the inspector
    public float spawnInterval = 30f; // Time between spawns
    public LayerMask obstacleLayer;
    public float checkRadius = 1f; // Radius to check for collisions


    public static int hasWinner;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(spawnInterval); // Wait 30 seconds before first spawn

        while (true)
        {
            SpawnObjects();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObjects()
    {
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogWarning("No objects assigned to spawn!");
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            Vector2 spawnPosition = GetValidSpawnPosition();
            GameObject randomObject = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            Instantiate(randomObject, spawnPosition, Quaternion.identity);
        }
    }

    Vector2 GetValidSpawnPosition()
    {
        Vector2 spawnPosition;
        int attempts = 10; // Avoid infinite loops

        do
        {
            spawnPosition = new Vector2(Random.Range(-48f, 48f), Random.Range(-27f, 27f));
            attempts--;
        }
        while (Physics2D.OverlapCircle(spawnPosition, checkRadius, obstacleLayer) && attempts > 0);

        return spawnPosition;
    }
}