using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AkioState
{ 
    Neutral,
    InLove,
    Dead
}

public class Akio : MonoBehaviour
{
    public AkioState currentState { get; private set; }

    [Header("References")]
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform playerTransform;
    public Animator animator;
    public GameObject heartFx;

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
    public bool isWaiting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        boid = GetComponent<Boid>();
        randomMovement = GetComponent<RandomMovement>();
        heartFx = transform.Find("Heart Loop")?.gameObject;
        heartFx.SetActive(false);

        currentState = AkioState.Neutral;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case AkioState.Neutral:
                if (!isDeathVisible)
                {
                    randomMovement.Move();
                }
                break;
            case AkioState.InLove:
                boid.Follow();
                break;
            case AkioState.Dead:
                break;
        }

        animator.SetBool("IsMoving", agent.velocity.magnitude > 0.01f);
    }

    // State Machine
    public void UpdateState(AkioState newState)
    {
        if ((currentState != newState) && (!CanTransition(currentState, newState)))
            return;

        EnterState(newState);
        currentState = newState;
        Debug.Log("State Updated!" + currentState);
    }

    // State Machine Rulez
    bool CanTransition(AkioState currentState, AkioState newState)
    {
        switch (currentState)
        {
            case AkioState.Neutral:
                return true;
            case AkioState.InLove:
                return newState != AkioState.Dead;
            case AkioState.Dead:
                return newState != AkioState.InLove;
        }

        return false;
    }

    // State Machine Transitions (i.e. One-Time effects)
    void EnterState(AkioState newState)
    {
        switch (newState)
        {
            case AkioState.InLove:
                SetLayerRecursively(gameObject, LayerMask.NameToLayer("Hot Guy"));
                heartFx.SetActive(true);
                break;
        }
    }

    // Change Layers
    void SetLayerRecursively(GameObject obj, int newLayerIndex)
    {
        obj.layer = newLayerIndex;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayerIndex);
        }
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

}
