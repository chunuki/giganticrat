using UnityEngine;

public class Boid : MonoBehaviour
{
    public LayerMask hotGuyLayerMask;
    public float attractionWeight = 1.0f;
    public float separationWeight = 1.0f;
    public float alignmentWeight = 1.0f;
    public float cohesionWeight = 1.0f;
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform playerTransform;

    public float bumpRange = 1.0f;
    private Vector3 separationForce;
    public float stoppingDistance = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Follow()
    {
        if (playerTransform != null)
        {
            Debug.Log("I am now following!");

            Vector3 steering = CalculateSteeringDirection();

            float stepDistance = 1.5f;
            Vector3 desiredPosition = transform.position + steering * stepDistance;

            agent.SetDestination(desiredPosition);

            if (agent.remainingDistance <= stoppingDistance)
            {
                agent.isStopped = true;
            }
            else agent.isStopped = false;
        }
    }

    private Vector3 CalculateAttraction()
    {
        Vector3 separationForce = Vector3.zero;
        Vector3 toPlayer = playerTransform.position - transform.position;
        toPlayer.y = 0f;

        float distToPlayer = toPlayer.magnitude;
        if (distToPlayer > stoppingDistance)
        {
            separationForce += toPlayer.normalized * attractionWeight;

        }
        return separationForce;
    }
    
    private Collider[] GetNeighbours()
    {
        return Physics.OverlapSphere(transform.position, bumpRange, hotGuyLayerMask);
    }

    private Vector3 ApplySeparationForce(Collider[] neighbours, Vector3 separationForce)
    {
        foreach (var neighbour in neighbours)
        {
            var dir = neighbour.transform.position - transform.position;
            var distance = dir.magnitude;
            var away = -dir.normalized;

            if (distance > 0)
            {
                separationForce += away / distance * separationWeight;
            }
        }
        return separationForce;
    }

    private Vector3 ApplyAlignment(Collider[] neighbours)
    {
        Vector3 neighboursForward = Vector3.zero;

        foreach (var neighbour in neighbours)
        {
            neighboursForward += neighbour.transform.position;
        }

        if (neighboursForward != Vector3.zero)
        {
            neighboursForward.Normalize();
        }

        separationForce += neighboursForward * alignmentWeight;
        return separationForce;
    }

    private Vector3 ApplyCohesion(Collider[] neighbours)
    {
        Vector3 averagePosition = Vector3.zero;

        foreach (var neighbour in neighbours)
        {
            averagePosition += neighbour.transform.position;
        }

        averagePosition /= neighbours.Length;
        Vector3 cohesionDir = (averagePosition - transform.position).normalized;
        separationForce += cohesionDir * cohesionWeight;
        return separationForce;
    }

    private Vector3 CalculateSteeringDirection()
    {
        Vector3 steering = Vector3.zero;

        // 1. Player Attraction
        Vector3 toPlayer = playerTransform.position - transform.position;
        toPlayer.y = 0f;

        float distToPlayer = toPlayer.magnitude;
        if (distToPlayer > stoppingDistance)
        {
            steering += toPlayer.normalized * attractionWeight;
        }

        // 2. Separation
        steering += CalculateSeparation() * separationWeight;

        return steering;

    }

    Vector3 CalculateSeparation()
    {
        Collider[] neighbours = Physics.OverlapSphere(transform.position, bumpRange, hotGuyLayerMask);
        Vector3 force = Vector3.zero;
        int count = 0;

        foreach (Collider col in neighbours)
        {
            if (col.gameObject == gameObject)
                continue;

            Vector3 diff = transform.position - col.transform.position;
            float dist = diff.magnitude;

            if (dist > 0f)
            {
                force += diff.normalized / dist;
                count++;
            }
        }

        if (count == 0) { return Vector3.zero; }
        return force / count;

    }

}