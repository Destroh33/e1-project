using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float movementX;
    float movementY;
    [SerializeField] float speed = 5.0f;
    Rigidbody2D rb;
    bool isGrounded;
    int score = 0;

    [SerializeField] GameManager gameManager;

    Animator animator;
    SpriteRenderer spriteRenderer;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip jumpClip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public int GetScore()
    {
        return score;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementX * speed, rb.linearVelocity.y);
        if (!Mathf.Approximately(movementX, 0f))
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = movementX < 0;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
            
        if (isGrounded && movementY > 0)
        {
            rb.AddForce(new Vector2(0, 250));
            source.PlayOneShot(jumpClip);
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        movementX = v.x;
        movementY = v.y;
        Debug.Log("Movement X = " + movementX);
        Debug.Log("Movement Y = " + movementY);
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
        if (collision.gameObject.CompareTag("Collectible"))
        {
            score++;
            collision.gameObject.SetActive(false);
            gameManager.UpdateScore(score);
        }
    }
}