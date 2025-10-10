using UnityEngine;

public class Collectible_NPC : MonoBehaviour
{
    [Header("Collectible Settings")]
    public string playerTag = "Player";

    [Header("Follow Offset Range (Randomized on Pickup)")]
    public Vector3 minOffset = new Vector3(-1f, 0.5f, 0f);

    public Vector3 maxOffset = new Vector3(1f, 1.5f, 0f);

    private bool collected = false;
    private Transform playerTransform;
    private Collider2D col;
    private Rigidbody2D rb;
    private Vector3 followOffset;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!collected && other.CompareTag(playerTag))
        {
            Collect(other.transform);
        }
    }

    private void Collect(Transform player)
    {
        collected = true;
        playerTransform = player;

        followOffset = new Vector3(
            Random.Range(minOffset.x, maxOffset.x),
            Random.Range(minOffset.y, maxOffset.y),
            Random.Range(minOffset.z, maxOffset.z)
        );

        if (col != null)
            col.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;
        }

        transform.SetParent(playerTransform);

        transform.localPosition = followOffset;
    }

    private void Update()
    {
        if (collected && playerTransform != null)
        {
            transform.position = playerTransform.position + followOffset;
        }
    }
}

