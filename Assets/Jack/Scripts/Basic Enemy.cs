using System.Collections;
using System.Collections.Generic;
//using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    gameHandler gameScript;
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
    public float attackCoolDown = 1.0f;

    [Header("Animation")]
    public Animator EnemyAnimator;
    public Animator ARMOR;


    // Attack placeholder
    public bool CanAtt = true;


    // Enemy States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private float timetoPath;

    private void Awake()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        playerKeyboard = gameScript.keyboardPlayer;
        playerController = gameScript.controllerPlayer;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Patroling()
    {
        if (!walkPointSet){
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            if (agent.isOnNavMesh)
                agent.SetDestination(walkPoint);
            else
                walkPointSet = false;

            //  this is a line in the SCENE view that shows its patrol spot
            Debug.DrawLine(transform.position, walkPoint, Color.yellow);
        }
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f || timetoPath >= 3f)
        {
            walkPointSet = false;
            timetoPath = 0f;
        }


    }

    private void SearchWalkPoint()
    {
        // calculates random x and z in range
        float zPoint = Random.Range(-walkPointRange, walkPointRange);
        float xPoint = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + xPoint, transform.position.y, transform.position.z + zPoint);

        ////////////////////////////////////////////////////////////////  evans shit
        
        // hol up make them drift towards players a bit tho
        if (gameScript.keyboardPlayerHealth > 0f && gameScript.controllerPlayerHealth > 0f) // Both Players
        {
            Vector3 avgPosOfPlayers = playerKeyboard.transform.position / 2 + playerController.transform.position / 2;
            Vector3 dirToMiddle = Vector3.Normalize(avgPosOfPlayers - walkPoint);
            walkPoint += dirToMiddle * 2f;
        }
        if (gameScript.keyboardPlayerHealth > 0f && gameScript.controllerPlayerHealth <= 0f) // Just Keyboard
        {
            Vector3 dirToPlayer = Vector3.Normalize(playerKeyboard.transform.position - walkPoint);
            walkPoint += dirToPlayer * 2f;
        }
        if (gameScript.keyboardPlayerHealth <= 0f && gameScript.controllerPlayerHealth > 0f) // Just Controller
        {
            Vector3 dirToPlayer = Vector3.Normalize(playerController.transform.position - walkPoint);
            walkPoint += dirToPlayer * 2f;
        }

        

        ////////////////////////////////////////////////////////////////   back to jack

        if (Physics.Raycast(walkPoint, -transform.up, 15f, ground))
        {
            walkPointSet = true;
        }
    }

    private void Chase()
    {
        if (playerKeyboard.activeInHierarchy && playerController.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position + Vector3.up, playerKeyboard.transform.position) < Vector3.Distance(transform.position + Vector3.up, playerController.transform.position))
            {
                if (agent.isOnNavMesh)
                    agent.SetDestination(playerKeyboard.transform.position);
            }
            else
            {
                if (agent.isOnNavMesh)
                    agent.SetDestination(playerController.transform.position);
            }
        }

        if (!playerKeyboard.activeInHierarchy && playerController.activeInHierarchy)
        {
            if (agent.isOnNavMesh)
                agent.SetDestination(playerController.transform.position);
        }

        if (playerKeyboard.activeInHierarchy && !playerController.activeInHierarchy)
        {
            if (agent.isOnNavMesh)
                agent.SetDestination(playerKeyboard.transform.position);
        }
    }

    private void EngagingPlayer()
    {
        if (playerInAttackRange && CanAtt)
        {
            Attacking();
        }
    }

    public void Attacking()
    {
        CanAtt = false;
        EnemyAnimator.SetTrigger("ATTAAACK");
        if (ARMOR)
            ARMOR.SetTrigger("ATTAAACK");
        StartCoroutine(ResetAtt());
    }

    IEnumerator ResetAtt()
    {
        yield return new WaitForSeconds(attackCoolDown);
        Chase();
        EnemyAnimator.SetTrigger("STOOOP");
        if (ARMOR)
            ARMOR.SetTrigger("STOOOP");
        CanAtt = true;
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerArea);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerArea);
        EngagingPlayer();

        if (!playerInSightRange)
        {
            enemyEyes.SetActive(false);
            EnemyAnimator.SetTrigger("RUUUN");
            if (ARMOR)
                ARMOR.SetTrigger("RUUUN");
            timetoPath = timetoPath + Time.deltaTime;
            Patroling();
        }
        if (playerInSightRange)
        {
            enemyEyes.SetActive(true);
            if (CanAtt)
            {
                EnemyAnimator.SetTrigger("RUUUN");
                if (ARMOR)
                    ARMOR.SetTrigger("RUUUN");
                Chase();
            }
            Debug.DrawLine(transform.position, agent.destination, Color.red);
        }
    }
}
