using UnityEngine;

public class PlayerMovementSide : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody2D playerRb;
    public Animator Animator;
    private bool m_FacingRight = true;
    private Vector2 m_Velocity = Vector2.zero;

    [Header("Player Elements")]
    private float playerMoveX;
    [Range(0f, 50f)][SerializeField] private int playerSpeed;
    private bool isGrounded;
    private bool wasGrounded;
    [SerializeField] private Transform groundPos;
    [Range(0f, 1f)][SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [Range(0f, 50f)][SerializeField] private int playerJumpSpeed;
    [Range(0f, 2f)][SerializeField] private float playerJumpBufferTime;
    private float playerJumpBufferTimeCounter;
    [Range(0f, 2f)][SerializeField] private float playerCoyoteTime;
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;
    private float playerCoyoteTimeCounter;

    private void Awake()
    {
        isGrounded = false;
        wasGrounded = false;
        playerJumpBufferTimeCounter = 0f;
        playerCoyoteTimeCounter = playerCoyoteTime;
        playerRb = GetComponent<Rigidbody2D>();
    }

    public void PlayerRun()
    {
        Vector2 targetVelocity = new Vector2(playerMoveX * playerSpeed, playerRb.linearVelocity.y);
        playerRb.linearVelocity = Vector2.SmoothDamp(playerRb.linearVelocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (playerMoveX > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (playerMoveX < 0 && m_FacingRight)
        {
            Flip();
        }

        if (Animator != null)
        {
            Animator.SetFloat("Speed", Mathf.Abs(playerMoveX));
            Animator.SetBool("IsMoving", playerMoveX != 0);
        }
    }

    private void PlayerJump()
    {
        if (isGrounded)
        {
            playerCoyoteTimeCounter = playerCoyoteTime;
        }
        else
        {
            playerCoyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerJumpBufferTimeCounter = playerJumpBufferTime;
        }

        if (playerJumpBufferTimeCounter > 0f && playerCoyoteTimeCounter > 0f)
        {
            playerJumpBufferTimeCounter = 0f;
            playerCoyoteTimeCounter = 0f;
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, playerJumpSpeed);
            Animator.SetBool("Jumping", true);
        }

        playerJumpBufferTimeCounter -= Time.deltaTime;
    }

    void Start()
    {
        Animator = GetComponent<Animator>();
        if (Animator == null)
        {
            Debug.LogError("No Animator component found on this GameObject.");
            return;
        }
    }

    private void FixedUpdate()
    {
        playerMoveX = Input.GetAxisRaw("Horizontal");
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundPos.position, groundCheckRadius, groundLayer);
        PlayerRun();
        PlayerJump();

        if (!wasGrounded && isGrounded)
        {
            OnLanding();
        }
    }

    public void OnLanding()
    {
        Animator.SetBool("Jumping", false);
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 flipped = transform.localScale;
        flipped.x *= -1f;
        transform.localScale = flipped;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundPos.position, groundCheckRadius);
    }
}
