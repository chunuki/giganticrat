using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //important

//if you use this code you are contractually obligated to like the YT video
public class RandomMovement : MonoBehaviour //don't forget to change the script name if you haven't
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
    public Animator animator;

    [Header("Patrol Settings")]
    public float range = 10f; //radius of sphere
    public float waitTime = 3f;

    bool isWaiting = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (!isWaiting && ReachedDestination())
        {
            StartCoroutine(Patrol());
        }

        animator.SetBool("IsMoving", agent.velocity.magnitude > 0.01f);

    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
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

    IEnumerator Patrol()
    {
        isWaiting = true;

        agent.isStopped = true; // to handle NavMesh

        animator.SetBool("IsWaiting", true); // to handle Animator

        Debug.Log("Destination Reached! Waiting...");

        yield return new WaitForSeconds(waitTime); // waits for a few seconds

        Debug.Log("Done Waiting. Going to New Destination");

        isWaiting = false;

        agent.isStopped = false;

        animator.SetBool("IsWaiting", false); // to handle Animator

        Vector3 point;
        if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            agent.SetDestination(point);
        }

    }

    public void OnFootstep()
    {
    }
}
