using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject enemyEyes;

    GameObject playerKeyboard;

    GameObject playerController;

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
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        playerKeyboard = GameObject.FindGameObjectsWithTag("PlayerKeyboard")[0];
        playerController = GameObject.FindGameObjectsWithTag("PlayerController")[0];
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
        if (Vector3.Distance(transform.position + Vector3.up, playerKeyboard.transform.position) < Vector3.Distance(transform.position + Vector3.up, playerController.transform.position))
        {
            agent.SetDestination(playerKeyboard.transform.position);
        }
        else
        {
            agent.SetDestination(playerController.transform.position);
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
            enemyEyes.SetActive(false);
            EnemyAnimator.SetTrigger("RUUUN");
            Patroling();
        }
        if (playerInSightRange)
        {
            enemyEyes.SetActive(true);
            EnemyAnimator.SetTrigger("RUUUN");
            Chase();
            Debug.DrawLine(transform.position, agent.destination, Color.red);
        }
    }
}
