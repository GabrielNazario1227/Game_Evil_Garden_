using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;

    private Rigidbody2D rb;
    private bool hasFallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Começa parado
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasFallen)
        {
            hasFallen = true;
            Invoke("Fall", fallDelay);
        }
    }

    void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}