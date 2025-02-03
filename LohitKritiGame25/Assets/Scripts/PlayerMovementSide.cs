using UnityEngine;

public class PlayerMovementSide : MonoBehaviour
{
    [Header("Player Components")]
    private Rigidbody2D playerRb;

    [Header("Player Elements")]
    private float playerMoveX;
    [Range(0f, 50f)][SerializeField] private int playerSpeed;
    private bool isGrounded;
    [SerializeField] private Transform groundPos;
    [Range(0f, 1f)][SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [Range(0f, 50f)][SerializeField] private int playerJumpSpeed;
    [Range(0f, 2f)][SerializeField] private float playerJumpBufferTime;
    private float playerJumpBufferTimeCounter;
    [Range(0f, 2f)][SerializeField] private float playerCoyoteTime;
    private float playerCoyoteTimeCounter;

    private void Awake()
    {
        isGrounded = false;
        playerJumpBufferTimeCounter = 0f;
        playerCoyoteTimeCounter = playerCoyoteTime;
        
        playerRb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        playerMoveX = Input.GetAxisRaw("Horizontal");

        if (Physics2D.OverlapCircle(groundPos.position, groundCheckRadius, groundLayer))
            isGrounded = true;
        else
            isGrounded= false;

        PlayerRun();
        PlayerJump();
    }

    private void PlayerRun()
    {
        playerRb.linearVelocityX = playerMoveX * playerSpeed;
    }
    private void PlayerJump()
    {
        if (isGrounded == true)
            playerCoyoteTimeCounter = playerCoyoteTime;
        else if (isGrounded == false && playerCoyoteTimeCounter > 0f)
            playerCoyoteTimeCounter -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
            playerJumpBufferTimeCounter = playerJumpBufferTime;

        if (playerJumpBufferTimeCounter > 0f && playerCoyoteTimeCounter > 0f)
        {
            playerJumpBufferTimeCounter = 0f;
            playerCoyoteTimeCounter = 0f;
            playerRb.linearVelocityY = playerJumpSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundPos.position, groundCheckRadius);
    }
}
