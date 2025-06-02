using System.Collections.Generic;
using UnityEngine;

public class Spawn100 : MonoBehaviour
{
    public GameObject cubePrefab;

    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-2, -2);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(2, 2);

    private List<GameObject> spawnedCubes = new List<GameObject>();
    private bool hasSpawned = false; // Add this flag

    void Update()
    {
        if (!hasSpawned)
        {
            SpawnCube();
            hasSpawned = true; // Set flag so it only spawns once
        }
    }

    private void SpawnCube()
    {
        // Spawn 100 cubes at random positions
        for (int i = 0; i < 100; i++)
        {
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float z = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector3 position = new Vector3(x, 3.0f, z);
            GameObject newCube = Instantiate(cubePrefab, position, Quaternion.identity);
            spawnedCubes.Add(newCube);
        }
    }
}