using UnityEngine;
using UnityEngine.SceneManagement;

public class Debris : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneToLoad = "NextScene";

    [Header("Layer Settings")]
    public LayerMask groundLayer;
    public string playerTag = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            Destroy(this);
            return;
        }

        if (collision.gameObject.CompareTag(playerTag))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}