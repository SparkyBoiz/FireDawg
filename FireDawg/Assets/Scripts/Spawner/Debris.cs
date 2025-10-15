using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Debris : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneToLoad = "NextScene";

    [Header("Layer Settings")]
    public LayerMask groundLayer;
    public string playerTag = "Player";

    [Header("Sprite Settings")]
    public Sprite groundSprite; // Sprite to switch to on ground contact

    private SpriteRenderer spriteRenderer;
    private bool hasChangedSprite = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // --- If debris hits the ground ---
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            if (!hasChangedSprite && groundSprite != null)
            {
                spriteRenderer.sprite = groundSprite;
                hasChangedSprite = true;
            }

            // ✅ Remove this script after changing the sprite
            Destroy(this);
            return;
        }

        // --- If debris hits the player ---
        if (collision.gameObject.CompareTag(playerTag))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
