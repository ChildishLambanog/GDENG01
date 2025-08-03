using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float giveUpDistance = 15f;
    public float roamRadius = 10f;
    public float roamDelay = 3f;

    public LayerMask obstacleMask;  // Assign this to Environment layer (walls, etc.)
    public LayerMask playerMask;    // Assign this to Player layer

    private NavMeshAgent agent;
    private Vector3 roamDestination;
    private float roamTimer;
    private enum State { Roaming, Chasing }
    private State currentState = State.Roaming;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PickNewRoamDestination();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        switch (currentState)
        {
            case State.Roaming:
                if (distanceToPlayer < detectionRange && CanSeePlayer())
                {
                    currentState = State.Chasing;
                }
                else
                {
                    Roam();
                }
                break;

            case State.Chasing:
                if (distanceToPlayer > giveUpDistance || !CanSeePlayer())
                {
                    currentState = State.Roaming;
                    PickNewRoamDestination();
                }
                else
                {
                    agent.SetDestination(player.position);
                }
                break;
        }
    }

    void Roam()
    {
        roamTimer += Time.deltaTime;

        if (roamTimer >= roamDelay || Vector3.Distance(transform.position, roamDestination) < 1f)
        {
            roamTimer = 0f;
            PickNewRoamDestination();
        }

        agent.SetDestination(roamDestination);
    }

    void PickNewRoamDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, roamRadius, NavMesh.AllAreas))
        {
            roamDestination = hit.position;
        }
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer, out RaycastHit hit, distanceToPlayer, obstacleMask | playerMask))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }

        return false;
    }
}