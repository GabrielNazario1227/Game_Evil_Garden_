using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public TrapPlatform platform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            platform.ActivateTrap();
        }
    }
}