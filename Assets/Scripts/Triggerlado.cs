using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public MovingPlatform platform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            platform.ActivatePlatform();
        }
    }
}