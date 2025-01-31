using UnityEngine;

public class PlayerMovementTopDown : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody2D playerRb;
    public Animator Animator;
    private bool m_FacingRight = true;
    private Vector2 m_Velocity = Vector2.zero;

    [Header("Player Elements")]
    private float playerMoveX;
    private float playerMoveY;
    [Range(0f, 50f)][SerializeField] private int playerSpeed;
    private bool isGrounded;
    private bool wasGrounded;
    [SerializeField] private Transform groundPos;
    [Range(0f, 1f)][SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [Range(0f, 2f)][SerializeField] private float playerCoyoteTime;
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;
    private float playerCoyoteTimeCounter;

    public void PlayerRun()
    {
        if (Mathf.Abs(playerMoveX) > Mathf.Abs(playerMoveY))
        {
            playerMoveY = 0;
        }
        else if (Mathf.Abs(playerMoveY) > Mathf.Abs(playerMoveX))
        {
            playerMoveX = 0;
        }
        else if (Mathf.Abs(playerMoveX) == 0 && Mathf.Abs(playerMoveY) == 0)
        {
            playerMoveX = 0;
            playerMoveY = 0;
            Animator.SetBool("IsMoving", false);
            Animator.SetBool("IsUp", false);
            Animator.SetBool("IsDown", false);
        }
        else
        {
            playerMoveY = 0;
        }

        Vector2 targetVelocity = new Vector2(playerMoveX * playerSpeed, playerMoveY * playerSpeed);
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
            Animator.SetFloat("Speed", Mathf.Abs(playerMoveX) + Mathf.Abs(playerMoveY));
            Animator.SetBool("IsMoving", playerMoveX != 0 || playerMoveY != 0);
            Animator.SetBool("IsUp", playerMoveY > 0);
            Animator.SetBool("IsDown", playerMoveY < 0);
        }
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
        playerMoveY = Input.GetAxisRaw("Vertical");
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundPos.position, groundCheckRadius, groundLayer);
        PlayerRun();
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
