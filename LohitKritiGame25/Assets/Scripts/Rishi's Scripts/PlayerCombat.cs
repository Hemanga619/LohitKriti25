using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator Animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Attack();
        }
    }

    void Attack()
    {
        Animator.SetTrigger("Attack");
        // Detect enemies in range of attack
        // Damage Them
    }
}
