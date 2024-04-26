using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public NavMeshAgent agent;

    Transform player;

    public LayerMask ground, playerArea;

    // Patroling Variables
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    // Attack placeholder


    // Enemy States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Patroling()
    {
        if (!walkPointSet){
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // calculates random x and z in range
        float zPoint = Random.Range(-walkPointRange, walkPointRange);
        float xPoint = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + xPoint, transform.position.y, transform.position.z + zPoint);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, ground))
        {
            walkPointSet = true;
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void EngagingPlayer()
    {
    }
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerArea);

        if (!playerInSightRange)
        {
            Patroling();
        }
        if (playerInSightRange)
        {
            Chase();
        }
    }
}
