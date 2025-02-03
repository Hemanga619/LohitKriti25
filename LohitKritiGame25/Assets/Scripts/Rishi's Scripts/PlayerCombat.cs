using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator Animator;
    public Vector3 attackOffset;
    public float attackRange = 3f;
    public LayerMask bossMask;
    public int attackDamage = 5;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Animator.SetTrigger("Attack");
            Attack();
        }
    }

    void Attack()
    {

        //
        Collider2D colInfo = Physics2D.OverlapCircle(transform.position, attackRange, bossMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<BossHealth>().TakeDamage(attackDamage);
            Debug.Log("Attacking");
        }
        Debug.Log("1");
    }
}
