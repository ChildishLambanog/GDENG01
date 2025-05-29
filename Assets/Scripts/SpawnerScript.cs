using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject capsulePrefab;
    public GameObject cubePrefab;

    private GameObject lastCapsule;
    private int cornerIndex = 0;

    [SerializeField] private float spawnInterval = 1.3f; //seconds
    private float spawnTimer = 0.0f;

    // Cube spawn settings
    [SerializeField] private float cubeSpawnInterval = 2.3f; //seconds
    private float cubeSpawnTimer = 0.0f;
    [SerializeField] private int cubesPerSpawn = 3;
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-2, -2);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(2, 2);

    private List<GameObject> spawnedCubes = new List<GameObject>();

    private Vector3[] corners = new Vector3[]
    {
        new Vector3(-2, 1.1f, -2),
        new Vector3(-2, 1.1f, 2),
        new Vector3(2, 1.1f, 2),
        new Vector3(2, 1.1f, -2)
    };

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnCapsule();
            spawnTimer = 0f;
        }

        cubeSpawnTimer += Time.deltaTime;
        if (cubeSpawnTimer >= cubeSpawnInterval)
        {
            SpawnCube();
            cubeSpawnTimer = 0f;
        }
    }

    private void SpawnCapsule()
    {
        if (lastCapsule != null) //Destroys the previous spawned capsule if it exists
        {
            Destroy(lastCapsule);
        }

        lastCapsule = Instantiate(capsulePrefab, corners[cornerIndex], Quaternion.identity);  //Spawns new capsule at the current corner
        
        cornerIndex = (cornerIndex + 1) % corners.Length; //Move to the next corner for the next spawn
    }
    private void SpawnCube()
    {
        foreach (var cube in spawnedCubes) //Destroy previously spawned cubes
        {
            if (cube != null)
            {
                Destroy(cube);
            }
                
        }
        spawnedCubes.Clear();

        // Spawn new cubes at random positions
        for (int i = 0; i < cubesPerSpawn; i++)
        {
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float z = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector3 position = new Vector3(x, 3.0f, z); // y = 1.1f to match capsule height
            GameObject newCube = Instantiate(cubePrefab, position, Quaternion.identity);
            spawnedCubes.Add(newCube);
        }
    }
}
