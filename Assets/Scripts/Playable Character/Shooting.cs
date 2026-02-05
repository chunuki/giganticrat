using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class Shooting : MonoBehaviour
{

    [SerializeField]
    private float attackSpeed = 1f;
    private float timeUntilNextAttack = 0f;
    public InputAction attack;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        attack = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        bool isShooting = animator.GetBool("isShooting");

        if (attack.WasPressedThisFrame()) 
        { 
            animator.SetBool("isShooting", true);

            timeUntilNextAttack = Time.time + attackSpeed; 
        }
    }

    // animation event to make woman move again
    public void ResetMovementSpeed()
    {
        System.Diagnostics.Debug.WriteLine("ResetMovementSpeed called");
        animator.SetBool("isShooting", false);
    }
}
