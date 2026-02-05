using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HotGuy : MonoBehaviour
{

    [Header("References")]
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform playerTransform;
    public Animator animator;

    [Header("Scripts")]
    public RandomMovement randomMovement;
    public Boid boid;

    [Header("Layers")]
    public LayerMask deathLayerMask;
    public LayerMask playerLayerMask;
    public LayerMask hotGuyLayerMask;

    [Header("Neutral Settings")]
    public float range = 10f; //radius of sphere
    public float waitTime = 3f;

    [Header("In Love Settings")]

    [Header("Angry Settings")]
    public float visionRange = 10f;

    private bool isDeathVisible;
    private bool isPlayerVisible;
    public bool isDead = false;
    public bool isInLove = false;
    public bool isWaiting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        boid = GetComponent<Boid>();
        randomMovement = GetComponent<RandomMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDeathVisible && !isInLove)
        {
            randomMovement.Move();
        }

        else if (isInLove)
        {
            Debug.Log("I am in Love!");
            boid.Follow();
        }

        animator.SetBool("IsMoving", agent.velocity.magnitude > 0.01f);
    }


    // functions for Detection
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    private void DetectDeath()
    {
        isDeathVisible = Physics.CheckSphere(transform.position, visionRange, deathLayerMask);
    }

    private void DetectPlayer()
    {
        isDeathVisible = Physics.CheckSphere(transform.position, visionRange, playerLayerMask);
    }

    // functions for Following
    // routine for Follow

}
