using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;
public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float movementX;
    float movementY;
    [SerializeField] float speed = 5.0f;
    Rigidbody2D rb;
    bool isGrounded;
    int score = 0;
    [SerializeField] GameObject groundCheck;

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
        isGrounded = CheckIsGrounded();
        if (!Mathf.Approximately(movementX, 0f))
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = movementX < 0;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        movementX = v.x;
        movementY = v.y;
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, 400));
            source.PlayOneShot(jumpClip);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            score++;
            collision.gameObject.SetActive(false);
            gameManager.UpdateScore(score);
        }
    }

    bool CheckIsGrounded()
    {
        float groundCheckDistance = 0.5f;
        int groundLayer = 1 << 8;
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, groundCheckDistance, groundLayer);
        Debug.DrawRay(groundCheck.transform.position, Vector2.down * groundCheckDistance, Color.red, 0.2f);
        return hit.collider != null;
    }
}