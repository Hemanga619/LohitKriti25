using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public int maxHealth = 500;
    public float currentHealth;
    public HealthBar healthBar;

    public GameObject deathEffect;

    public bool isInvulnerable = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= maxHealth/2)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}