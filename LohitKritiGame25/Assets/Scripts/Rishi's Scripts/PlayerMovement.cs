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
        // Compute target velocity
        Vector2 targetVelocity = new Vector2(playerMoveX * playerSpeed, playerRb.linearVelocity.y);

        // Smoothly transition to the target velocity
        playerRb.linearVelocity = Vector2.SmoothDamp(playerRb.linearVelocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // Flip player if moving in the opposite direction
        if (playerMoveX > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (playerMoveX < 0 && m_FacingRight)
        {
            Flip();
        }

        // Update animator's Speed parameter
        if (Animator != null)
        {
            Animator.SetFloat("Speed", Mathf.Abs(playerMoveX));
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

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            playerJumpBufferTimeCounter = playerJumpBufferTime;
        }

        if (playerJumpBufferTimeCounter > 0f && playerCoyoteTimeCounter > 0f)
        {
            playerJumpBufferTimeCounter = 0f;
            playerCoyoteTimeCounter = 0f;

            // Apply jump force
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, playerJumpSpeed);

            // Trigger the jump animation
            if (Animator != null)
            {
                Animator.SetBool("Jumping", true);
            }
        }

        // Reset buffer counter over time
        playerJumpBufferTimeCounter -= Time.deltaTime;
    }

    void Start()
    {
        // Initialize Animator
        Animator = GetComponent<Animator>();

        if (Animator == null)
        {
            Debug.LogError("No Animator component found on this GameObject.");
            return;
        }

        // Debug animator parameters
        int parameterCount = Animator.parameterCount;
        bool speedExists = false;
        bool jumpingExists = false;

        for (int i = 0; i < parameterCount; i++)
        {
            AnimatorControllerParameter parameter = Animator.GetParameter(i);
            //Debug.Log($"Parameter {i}: Name = {parameter.name}, Type = {parameter.type}");

            if (parameter.name == "Speed")
            {
                speedExists = true;
            }
            if (parameter.name == "Jumping")
            {
                jumpingExists = true;
            }
        }

        if (!speedExists)
        {
            Debug.LogError("Animator parameter 'Speed' does not exist.");
        }
        if (!jumpingExists)
        {
            Debug.LogError("Animator parameter 'Jumping' does not exist.");
        }
    }

    private void FixedUpdate()
    {
        playerMoveX = Input.GetAxisRaw("Horizontal");

        // Check if grounded
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundPos.position, groundCheckRadius, groundLayer);

        // Handle running and jumping
        PlayerRun();
        PlayerJump();

        // Handle landing detection
        if (!wasGrounded && isGrounded)
        {
            OnLanding();
        }
    }

    public void OnLanding()
    {
        if (Animator != null)
        {
            Animator.SetBool("Jumping", false);
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing
        m_FacingRight = !m_FacingRight;

        // Flip the player's local scale
        Vector3 flipped = transform.localScale;
        flipped.x *= -1f;
        transform.localScale = flipped;
    }

    private void OnDrawGizmos()
    {
        // Draw the ground check radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundPos.position, groundCheckRadius);
    }
}
