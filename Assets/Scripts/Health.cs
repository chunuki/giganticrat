using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    Ragdoll ragdoll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

        ragdoll = GetComponent<Ragdoll>();
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        for each(var rigidBody in rigidBodies){
            rigidbody.gameObject.AddComponent<BulletTarget>();
        }
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

    private void Die()
    {
        animator.SetBool("IsDead", true);
    }
}
