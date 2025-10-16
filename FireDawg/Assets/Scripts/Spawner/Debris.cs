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
    public Sprite groundSprite;

    private SpriteRenderer spriteRenderer;
    private bool hasChangedSprite = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            if (!hasChangedSprite && groundSprite != null)
            {
                spriteRenderer.sprite = groundSprite;
                hasChangedSprite = true;
            }

            Destroy(this);
            return;
        }

        if (collision.gameObject.CompareTag(playerTag))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
