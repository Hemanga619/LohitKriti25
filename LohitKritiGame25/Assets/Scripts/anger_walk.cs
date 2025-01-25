using UnityEngine;

public class anger_walk : StateMachineBehaviour
{
    public float Speed = 2.5f;
    private Transform Player;
    private Rigidbody2D rb;
    private Boss boss;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();

        if (Player == null)
            Debug.LogError("Player is null. Ensure the Player object has the correct tag.");
        if (rb == null)
            Debug.LogError("Rigidbody2D is null. Ensure the GameObject has a Rigidbody2D component.");
        if (boss == null)
            Debug.LogError("Boss is null. Ensure the GameObject has a Boss component.");
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (boss == null || Player == null || rb == null)
        {
            Debug.LogError("One of the required components is null.");
            return;
        }

        boss.LookAtPlayer();

        Vector2 target = new Vector2(Player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, Speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
