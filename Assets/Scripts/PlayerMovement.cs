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
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("IsJumping", Mathf.Abs(rb.linearVelocity.y) > 0.1f);

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

        if (isDead) return;

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

        if (transform.position.y < deathY)
        {
            Die();
        }

        rb.linearVelocity = new Vector2(
            moveInput * speed,
            rb.linearVelocity.y
        );

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

        if (collision.CompareTag("Door1"))
        {
            NextLevel();
        }

        if (collision.CompareTag("Door2"))
        {
            NextLevel2();
        }

        if (collision.CompareTag("Door3"))
        {
            Win();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("Player morreu!");

        // Ativa animação de morte
        anim.SetBool("IsDead", true);

        if (deathCounter != null)
        {
            deathCounter.AddDeath();
        }

        audioSource.Stop();

        audioSource.PlayOneShot(deathSound);

        // Para movimento
        rb.linearVelocity = Vector2.zero;

        // opcional: impede o player de andar durante a morte
        rb.bodyType = RigidbodyType2D.Static;

        Invoke("RestartLevel", 1.2f);
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

    void NextLevel()
    {
        Debug.Log("Próxima fase!");

        SceneManager.LoadScene("Level2");
    }

    void NextLevel2()
    {
        Debug.Log("Próxima fase!");

        SceneManager.LoadScene("level3");
    }
}