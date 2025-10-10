using UnityEngine;
using UnityEngine.SceneManagement;

public class Debris : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneToLoad;

    public string playerTag = "Player";

    [Header("Layer Settings")]
    public LayerMask groundLayer;

    [Header("Collider Settings")]
    public bool useTrigger = true;

    private bool touchingGround = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsInLayerMask(collision.gameObject.layer, groundLayer))
        {
            touchingGround = true;
        }

        if (!useTrigger && !touchingGround && collision.gameObject.CompareTag(playerTag))
        {
            LoadScene();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
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
        if (useTrigger && touchingGround && other.CompareTag(playerTag))
        {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
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
