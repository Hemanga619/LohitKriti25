using UnityEngine;

public class PlayerMovementTop : MonoBehaviour
{
    [Header("Player Components")]
    private Rigidbody2D playerRb;

    [Header("Player Elements")]
    private float playerMoveX;
    private float playerMoveY;
    [SerializeField] private float playerSpeed;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        playerMoveX = Input.GetAxisRaw("Horizontal");
        playerMoveY = Input.GetAxisRaw("Vertical");

        PlayerRun();
    }

    private void PlayerRun()
    {
        playerRb.linearVelocity = new Vector3(playerMoveX * playerSpeed, playerMoveY * playerSpeed);
    }
}