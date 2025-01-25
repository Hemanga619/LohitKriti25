using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    

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
        //Play an attack Animation
        // Detect enemies in range of attack
        // Damage Them
    }
}
