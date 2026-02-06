using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Animator animator;
    public Akio akio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        akio = GetComponent<Akio>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if ((currentHealth <= 0.0f) && (akio.currentState != AkioState.InLove))
        {
            Die();
        }
     }

    public static event Action onAkioDied;
    void Die()
    {
        akio.agent.isStopped = true;
        animator.SetBool("IsDead", true);
        akio.UpdateState(AkioState.Dead);
        onAkioDied?.Invoke();
    }
}
