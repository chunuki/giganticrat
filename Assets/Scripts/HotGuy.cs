using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //important

public class HotGuy : MonoBehaviour
{

    [Header("References")]
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform playerTransform;
    public Transform centrePoint; //centre of the area the agent wants to move around in
    public Animator animator;

    [Header("Scripts")]
    public RandomMovement randomMovement;

    [Header("Layers")]
    public LayerMask deathLayerMask;
    public LayerMask playerLayerMask;

    [Header("Neutral Settings")]
    public float range = 10f; //radius of sphere
    public float waitTime = 3f;

    [Header("In Love Settings")]
    public float stoppingDistance = 1.0f;

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
            Follow();
        }

        animator.SetBool("IsMoving", agent.velocity.magnitude > 0.01f);
    }


    // functions for Patrol
    // find a random point within the NavMesh
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    bool ReachedDestination()
    {
        return !agent.pathPending &&
                agent.remainingDistance <= agent.stoppingDistance &&
                (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

    // routine for Patrol
    IEnumerator Patrol()
    {
        // update booleans
        isWaiting = true;
        agent.isStopped = true; // to handle NavMesh
        animator.SetBool("IsWaiting", true); // to handle Animator

        Debug.Log("Destination Reached! Waiting...");

        yield return new WaitForSeconds(waitTime); // waits for a few seconds

        Debug.Log("Done Waiting. Going to New Destination");

        // update booleans
        isWaiting = false;
        agent.isStopped = false;
        animator.SetBool("IsWaiting", false); // to handle Animator

        bool pointOK = false;
        Vector3 point;
        while (!pointOK)
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                pointOK = true;
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
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

    // functions for Following
    // routine for Follow
    private void Follow()
    {
        if (playerTransform != null)
        {
            agent.SetDestination(playerTransform.position);

            if (agent.remainingDistance <= stoppingDistance)
            {
                agent.isStopped = true;
            }
            else agent.isStopped = false;
        }

    }
    
}
