using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    public float fallDelay = 0.2f;

    private Rigidbody2D rb;
    private bool activated = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Plataforma começa parada
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void ActivateTrap()
    {
        if (!activated)
        {
            activated = true;
            Invoke("Fall", fallDelay);
        }
    }

    void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}