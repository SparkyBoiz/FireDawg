using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private float lockedX;

    private void Start()
    {
        lockedX = transform.position.x;
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = lockedX;
        transform.position = pos;
    }
}