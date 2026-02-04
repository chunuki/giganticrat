using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class Shooting : MonoBehaviour
{

    public InputAction attack;
    Animator animator;
    private ThirdPersonController movementScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementScript = GetComponent<ThirdPersonController>(); 
        animator = GetComponent<Animator>();
        attack = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        bool isShooting = animator.GetBool("isShooting");

        // if not currently shooting then shoot
        if (!isShooting && attack.WasPressedThisFrame()) 
        { 
            animator.SetBool("isShooting", true);
            if (movementScript != null) {
                movementScript.MoveSpeed = 0f;
                movementScript.SprintSpeed = 0f;
            }
        }
    }

    // animation event to make woman move again
    public void ResetMovementSpeed()
    {
        animator.SetBool("isShooting", false);
        movementScript.MoveSpeed = 2.0f;
        movementScript.SprintSpeed = 5.335f;
    }
}
