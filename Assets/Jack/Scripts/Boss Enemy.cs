using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour
{
    gameHandler gameScript;
    public NavMeshAgent agent;
    public GameObject enemyEyes;
    public GameObject healthbar;
    public GameObject healthbar1;

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


    // Attack placeholder
    private bool CanAtt = true;


    // Enemy States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public AudioSource audioSource;
    public AudioClip audioClipStart;
    public AudioClip audioClipImpact;

    private bool playedAudio = false;

    // FALLING BOOL
    public bool bossLanded = false;

    public Transform swordTip;

    [Header("Get players over here")]
    public Transform centerOfBossFight;
    public float fightDist = 60f;
    public Transform startOfFightSpot;
    public GameObject walls;

    public bool playerCamerasShouldBeShaking = false;

    float timeOfBossLand;
    float camShakeTime = 2.5f;
    public float shakeIntensity = 0.2f;

    private void Awake()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        audioSource.clip = audioClipStart;
        //audioSource.Play();
        gameScript.gameObject.GetComponent<AudioSource>().enabled = false;
        playerKeyboard = gameScript.keyboardPlayer;
        playerController = gameScript.controllerPlayer;
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        healthbar.SetActive(false);
        healthbar1.SetActive(false);
    }

    private void Patroling()
    {
        if (!walkPointSet){
            SearchWalkPoint();
        }
        if (transform.position.y <= 3)
        {
            if (walkPointSet)
            {
                if (agent.isOnNavMesh)
                    agent.SetDestination(walkPoint);
                else
                    walkPointSet = false;

                //  this is a line in the SCENE view that shows its patrol spot
                Debug.DrawLine(transform.position, walkPoint, Color.yellow);
            }
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
        if (transform.position.y <= 3)
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
        StartCoroutine(ResetAtt());
    }

    IEnumerator ResetAtt()
    {
        yield return new WaitForSeconds(attackCoolDown);
        Chase();
        EnemyAnimator.SetTrigger("STOOOP");
        CanAtt = true;
    }

    void Update()
    {
        if (transform.position.y <= 3)
        {
            if (!bossLanded)
            {
                timeOfBossLand = Time.time;
                // THIS IS WHERE BOSS FIRST LANDS
                //print("BOSS LANDED");
                float dK = Vector3.Distance(centerOfBossFight.position, playerKeyboard.transform.position);
                float dC = Vector3.Distance(centerOfBossFight.position, playerController.transform.position);


                if (dK > fightDist)
                    playerKeyboard.transform.position = startOfFightSpot.position;

                if (dC > fightDist)
                    playerController.transform.position = startOfFightSpot.position;

                walls.SetActive(true);

                playerCamerasShouldBeShaking = true;
            }


            bossLanded = true;
            agent.enabled = true;
            if (!playedAudio)
            {
                //audioSource.Stop();
                //audioSource.PlayOneShot(audioClipImpact);
                //playedAudio = true;
                healthbar.SetActive(true);
                healthbar1.SetActive(true);
            }
        }
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerArea);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerArea);
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
            if (CanAtt)
            {
                EnemyAnimator.SetTrigger("RUUUN");
                Chase();
            }
        }

        // Sword damage deal
        if (Vector3.Distance(swordTip.position, gameScript.keyboardPlayer.transform.position) < 3f)
        {
            //print("DAMAGE");
            gameScript.keyboardPlayerHealth -= Time.deltaTime * 3f;
        }
        
        if (Vector3.Distance(swordTip.position, gameScript.controllerPlayer.transform.position) < 3f)
        {
            //print("DAMAGE");
            gameScript.controllerPlayerHealth -= Time.deltaTime * 3f;
        }


        // Camera Shake Stuff
        if (playerCamerasShouldBeShaking)
        {
            if (bossLanded) shakeIntensity = (camShakeTime - (Time.time - timeOfBossLand)) / camShakeTime;
            
            if (Time.time > timeOfBossLand + camShakeTime)
            {
                playerCamerasShouldBeShaking = false;
            }
        }

    }


}
