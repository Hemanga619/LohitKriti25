using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D characterController;

    public Animator Animator;

    public float runSpeed = 40f;

    private float horizontalMove = 0f;

    private bool jump = false;

    private bool crouch = false;

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
            Debug.Log($"Parameter {i}: Name = {parameter.name}, Type = {parameter.type}");

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
        characterController.OnLandEvent.AddListener(OnLanding);
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Animator != null)
        {
            Animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        else
        {
            Debug.LogWarning("Animator is not assigned.");
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
            Animator.SetBool("Jumping", true);
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
    public void OnLanding()
    {
        if (Animator != null)
        {
            Animator.SetBool("Jumping", false);
        }
    }

    void FixedUpdate()
    {
        if (characterController != null)
        {
            characterController.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            Debug.Log($"Movement applied: {horizontalMove}, Jump: {jump}, Crouch: {crouch}");
        }
        else
        {
            Debug.LogError("CharacterController2D is not assigned!");
        }

        // Reset jump after it's been processed
        jump = false;
    }

}