using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D characterController;
    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            crouch = true;
        } 
        else if (Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            crouch = false;
        }
    }

    void FixedUpdate()
    {
        characterController.Move(horizontalMove * Time.fixedDeltaTime,  crouch, jump);
        jump = false;
    }
}
