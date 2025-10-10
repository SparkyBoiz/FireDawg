using UnityEngine;
using UnityEngine.SceneManagement;

public class Debris : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("The name of the scene to load (must be added in Build Settings).")]
    public string sceneToLoad;

    [Tooltip("Require player to have this tag.")]
    public string playerTag = "Player";

    [Header("Layer Settings")]
    [Tooltip("The layer considered 'ground'.")]
    public LayerMask groundLayer;

    [Header("Collider Settings")]
    [Tooltip("Set to true if this object has a trigger collider.")]
    public bool useTrigger = true;

    private bool touchingGround = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for ground contact
        if (IsInLayerMask(collision.gameObject.layer, groundLayer))
        {
            touchingGround = true;
        }

        // Handle player collision if not using trigger
        if (!useTrigger && !touchingGround && collision.gameObject.CompareTag(playerTag))
        {
            LoadScene();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // When leaving ground
        if (IsInLayerMask(collision.gameObject.layer, groundLayer))
        {
            touchingGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (useTrigger && !touchingGround && other.CompareTag(playerTag))
        {
            LoadScene();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Optional safety: if touching ground, ignore scene load even while overlapping player
        if (useTrigger && touchingGround && other.CompareTag(playerTag))
        {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Optional reset
    }

    private void LoadScene()
    {
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogWarning("SceneChanger: No scene name assigned!");
            return;
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    private bool IsInLayerMask(int layer, LayerMask mask)
    {
        return ((1 << layer) & mask) != 0;
    }
}
