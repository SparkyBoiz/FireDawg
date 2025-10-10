using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs to Spawn")]
    public GameObject[] prefabs;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Spawn Settings")]
    public float spawnInterval = 3f;
    public bool autoSpawn = true;

    [Header("Scale Randomization")]
    public Vector2 xScaleRange = new Vector2(0.8f, 1.2f);

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

    public void SpawnRandom()
    {
        if (prefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("RandomSpawner: Missing prefabs or spawn points!");
            return;
        }

        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject instance = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

        float randomXScale = Random.Range(xScaleRange.x, xScaleRange.y);
        Vector3 newScale = instance.transform.localScale;
        newScale.x = randomXScale;
        instance.transform.localScale = newScale;
    }
}