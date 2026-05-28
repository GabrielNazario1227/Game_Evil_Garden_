using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public DeathCounter deathCounter;

    public float speed = 5f;
    public float jumpForce = 10f;

    public AudioClip stepSound;
    public AudioClip deathSound;

    public float deathY = -0.49f;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audioSource;

    private bool isGrounded;
    private bool isDead = false;

    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Animações
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("IsJumping", Mathf.Abs(rb.linearVelocity.y) > 0.1f);

        // Virar personagem
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        // Fechar jogo com ESC
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }

        // Não deixa controlar após morrer
        if (isDead) return;

        // Som de passos
        if (Mathf.Abs(moveInput) > 0.1f && isGrounded)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = stepSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip == stepSound)
            {
                audioSource.Stop();
            }
        }

        // Morte ao cair
        if (transform.position.y < deathY)
        {
            Die();
        }

        // Movimento
        rb.linearVelocity = new Vector2(
            moveInput * speed,
            rb.linearVelocity.y
        );

        // Pulo
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
        }

        
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            Die();
        }

        if (collision.CompareTag("Door"))
        {
            Win();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("Player morreu!");

        if (deathCounter != null)
        {
            deathCounter.AddDeath();
        }

        // Para passos
        audioSource.Stop();

        // Toca som de morte
        audioSource.PlayOneShot(deathSound);

        // Desativa movimento
        rb.linearVelocity = Vector2.zero;

        // Espera o som tocar
        Invoke("RestartLevel", 0.5f);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    void Win()
    {
        Debug.Log("Você venceu!");

        SceneManager.LoadScene("WinScene");
    }
}