using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidBodies;
    Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        DeactivateRagdoll();
    }

    void Update()
    {

    }

    public void DeactivateRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        }

        animator.enabled = true;
    }

    public void ActivateRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
        }

        animator.enabled = true;
    }
}