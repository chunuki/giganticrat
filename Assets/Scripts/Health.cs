using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0.0f)
        {
            Die();
        }
     }

    void Die()
    {
        animator.SetBool("IsDead", true);
    }
}
