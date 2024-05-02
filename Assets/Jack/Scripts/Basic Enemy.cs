using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public NavMeshAgent agent;

    GameObject[] player;
    public GameObject Enemy;

    public LayerMask ground, playerArea;

    // Patroling Variables
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Animation")]
    public Animator EnemyAnimator;


    // Attack placeholder
    private bool PlayerInRange = false;
    private bool CanAtt = true;


    // Enemy States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, player2inSightRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
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

            //  this is a line in the SCENE view that shows its patrol spot
            Debug.DrawLine(transform.position, walkPoint, Color.yellow);
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
     if (Vector3.Distance(transform.position + Vector3.up, player[0].transform.position) < Vector3.Distance(transform.position + Vector3.up, player[1].transform.position))
            {
            agent.SetDestination(player[0].transform.position);
            }
     else
        {
            agent.SetDestination(player[1].transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            PlayerInRange = false;
        }
    }

    private void EngagingPlayer()
    {
        if (PlayerInRange == true)
        {
            EnemyAnimator.SetTrigger("ATTAAACK");
        }
        else
        {
            EnemyAnimator.SetTrigger("STOOOP");
        }
    }
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerArea);
        EngagingPlayer();

        if (!playerInSightRange)
        {
            EnemyAnimator.SetTrigger("RUUUN");
            Patroling();
        }
        if (playerInSightRange)
        {
            EnemyAnimator.SetTrigger("RUUUN");
            Chase();
            Debug.DrawLine(transform.position, agent.destination, Color.red);
        }
    }
}
