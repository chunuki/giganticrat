using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    public Health health;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit(float damage)
    {
        health.TakeDamage(damage);
        Debug.Log("hit or miss huh");
    }
}
