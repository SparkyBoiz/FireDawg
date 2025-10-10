using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs to Spawn")]
    public GameObject[] prefabs; // Possible prefabs to spawn

    [Header("Spawn Points")]
    public Transform[] spawnPoints; // Set spawn locations

    [Header("Spawn Settings")]
    public float spawnInterval = 3f; // Time between spawns
    public bool autoSpawn = true;    // Should it spawn automatically?

    [Header("Scale Randomization")]
    [Tooltip("Minimum and maximum X scale for spawned prefabs.")]
    public Vector2 xScaleRange = new Vector2(0.8f, 1.2f); // Default range

    private float timer;

    private void Start()
    {
        timer = spawnInterval;
    }

    private void Update()
    {
        if (!autoSpawn) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnRandom();
            timer = spawnInterval;
        }
    }

    /// <summary>
    /// Spawns a random prefab at a random spawn point and randomizes its X scale.
    /// </summary>
    public void SpawnRandom()
    {
        if (prefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("RandomSpawner: Missing prefabs or spawn points!");
            return;
        }

        // Pick random prefab and spawn point
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate prefab
        GameObject instance = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

        // Randomize X scale within range
        float randomXScale = Random.Range(xScaleRange.x, xScaleRange.y);
        Vector3 newScale = instance.transform.localScale;
        newScale.x = randomXScale;
        instance.transform.localScale = newScale;
    }
}