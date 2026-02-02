using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{

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
        if (attack.WasPressedThisFrame()) 
        { 
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
        
    }
}
