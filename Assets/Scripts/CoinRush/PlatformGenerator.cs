using UnityEngine;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Platform Prefabs")]
    public GameObject[] platformPrefabs;

    [Header("Obstacle / Coin Prefabs")]
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;

    [Header("Spawn Settings")]
    public Transform player;
    public int initialPlatforms = 5;
    public int safeStartPlatforms = 2; 
    public float platformLength = 10f;
    public float laneDistance = 2f;

    [Header("Spawn Chances")]
    [Range(0f, 1f)] public float obstacleChance = 0.3f;
    [Range(0f, 1f)] public float coinChance = 0.5f;

    private float zSpawn = 0f;
    private Queue<GameObject> activePlatforms = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < initialPlatforms; i++)
        {
            GameObject prefab = i < safeStartPlatforms 
                ? platformPrefabs[0] 
                : platformPrefabs[Random.Range(0, platformPrefabs.Length)];

            float spawnZ = i * platformLength;
            GameObject platform = Instantiate(prefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
            activePlatforms.Enqueue(platform);

            if (i >= safeStartPlatforms)
                SpawnObstaclesAndCoins(platform);

            zSpawn = spawnZ + platformLength;
        }
    }

    void Update()
    {
        if (player.position.z + (initialPlatforms * platformLength / 2) > zSpawn)
        {
            GameObject prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
            SpawnPlatform(prefab, false);
            DeleteOldPlatform();
        }
    }

    void SpawnPlatform(GameObject prefab, bool safe)
    {
        GameObject platform = Instantiate(prefab, new Vector3(0, 0, zSpawn), Quaternion.identity);
        activePlatforms.Enqueue(platform);

        if (!safe)
            SpawnObstaclesAndCoins(platform);

        zSpawn += platformLength;
    }

    void DeleteOldPlatform()
    {
        GameObject oldPlatform = activePlatforms.Dequeue();
        Destroy(oldPlatform);
    }

    void SpawnObstaclesAndCoins(GameObject platform)
    {
        float[] lanes = { -laneDistance, 0f, laneDistance };

        foreach (float lane in lanes)
        {   
            if (obstaclePrefabs.Length > 0 && Random.value < obstacleChance)
            {
                GameObject obsPrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
                Vector3 pos = platform.transform.position + new Vector3(lane, 0.5f, 0);
                Instantiate(obsPrefab, pos, Quaternion.identity, platform.transform);
            }
        }

        if (coinPrefab != null)
        {
            foreach (float lane in lanes)
            {
                if (Random.value < coinChance)
                {
                    Vector3 pos = platform.transform.position + new Vector3(lane, 1f, 0);
                    Instantiate(coinPrefab, pos, Quaternion.identity, platform.transform);
                }
            }
        }
    }
}
