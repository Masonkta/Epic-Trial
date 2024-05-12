using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Tilemaps.Tilemap;

public class playerMTutorial : MonoBehaviour
{
    gameHandler gameScript;
    public Tutorial tutorialScript;
    CharacterController controller;

    public bool isPlayerOne;
    public bool hardCodeKeyboard;
    public GameObject player;
    public Transform forwardTransform;
    public Transform cameraTransform;


    [Header("Moving")]
    public float currentVelocity;
    public float walkSpeed = 6f;
    public float sprintSpeed = 11f;

    private float speed;
    private bool isMoving;
    private bool sprinting;
    private float forwardMoveAm;
    private float sideMoveAm;
    private Vector3 playerVelocity;


    [Header("Jumping")]
    public float groundCheckDistance = 2.2f;
    public float jumpHeight = 3.3f;
    public float airDashSpeed = 20f;

    private bool isGrounded;
    private float gravity = Physics.gravity.y;
    private bool airDashing;
    private bool ableToAirDash;


    [Header("Dodging")]
    public float dodgeSpeed = 20f;
    public float dodgeTime = 0.4f;

    private bool isDodging;


    [Header("Turning")]
    public bool shiftLock = false;
    public float xSensController = 250f;
    public float ySensController = 150f;
    public float xSensKeyboard = 80f;
    public float ySensKeyboard = 25f;

    private float horizontalTurnAmount;
    private float verticalTurnAmount;


    [Header("Camera")]
    public float camHeight = 2.5f;
    float initialCamHeight;
    float actualCamHeight;
    public float camDistance = 6f;
    float initialCamDistance;
    float actualCamDistance;


    [Header("Camera Follow")]
    public float cameraAngle;


    [Header("Audio")]
    public AudioSource footstepsSound;
    public AudioSource jumpSound;
    AudioClip jumpSoundEffect;
    public AudioSource landingOnGrass;
    AudioClip landingOnGrassSound;


    [Header("Animation")]
    Animator playerAnimator;


    [Header("Trails")]
    public TrailRenderer trail;
    /*
    [Header("Boss for camera")]
    public GameObject bossObj;
    public BossEnemy bossScript;
    public bool bossSpawned = false;
    public bool bossFalling = false;
    */

    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        controller = GetComponent<CharacterController>();
        playerAnimator = player.GetComponent<Animator>();

        initialCamDistance = camDistance; actualCamDistance = camDistance;
        initialCamHeight = camHeight; actualCamHeight = camHeight;

        jumpSoundEffect = jumpSound.clip;
        landingOnGrassSound = landingOnGrass.clip;
    }

    void Update()
    {
        checkIsGrounded();
        if (hardCodeKeyboard) hardCodedKeyboardPlayerInput();

        // Turning // 
        horizontalTurns();
        verticalTurns();

        // Moving //
        move();

        // Camera //
        handleCamera();

        // Audio //
        handleAudio();

        getLookDirection();

        TutorialParts();
    }


    ///////// Jumping //////////

    void Jump()
    {
        if (!isDodging)
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void airDash()
    {
        Vector3 moveDirection = new Vector3(sideMoveAm, 0f, forwardMoveAm) * airDashSpeed;
        var moveDir = cameraTransform.TransformDirection(moveDirection); moveDir.y = 1f; // This is our new up force
        playerVelocity += moveDir;
        airDashing = true;

        jumpSound.PlayOneShot(jumpSoundEffect);
    }

    void checkIsGrounded()
    {
        bool prev = isGrounded;

        isGrounded = controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        //Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);

        if (!prev && isGrounded && playerVelocity.y < -6f)
        {   // We just landed and were falling a decent bit
            landingOnGrass.PlayOneShot(landingOnGrassSound);
        }

        if (isGrounded) airDashing = false;
    }


    void OnJump()
    {
        if (isGrounded)
            Jump();
        else if (ableToAirDash) // Dash if Player is not on ground and has not started falling down
            airDash();
    }

    void OnShiftLock()
    {
        if (sprinting == false)
            shiftLock = !shiftLock;
    }


    //////// Turning ///////////
    void OnTurn(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        if (tutorialScript.playersCanMove && tutorialScript.playerHasTouchedTable)
        {
            horizontalTurnAmount = turnInput[0];
            verticalTurnAmount = turnInput[1];
        }
    }

    void horizontalTurns()
    {

        float turnStrength = isPlayerOne ? xSensKeyboard : xSensController;
        cameraAngle += horizontalTurnAmount * turnStrength * Time.deltaTime;
        if (cameraAngle > 360f) cameraAngle -= 360f;
        if (cameraAngle < 0f) cameraAngle += 360f;

        //if (shiftLock) cameraAngle = 180f;
    }

    void verticalTurns()
    {
        float turnStrength = isPlayerOne ? ySensKeyboard : ySensController;
        initialCamHeight -= turnStrength * verticalTurnAmount * Time.deltaTime;
        initialCamHeight = Mathf.Clamp(initialCamHeight, -1f, 5f);


    }

    //// Moving ////

    void OnMove(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        if (tutorialScript.playersCanMove)
        {
            if (tutorialScript.playerHasTouchedTable)
            {
                sideMoveAm = turnInput[0];
                forwardMoveAm = turnInput[1];
            }
        }


    }

    void OnSprint()
    {
        if (shiftLock == false)
            sprinting = !sprinting;
    }

    void OnDodge()
    {
        StartCoroutine(Dodging());
    }

    void setSpeed()
    {
        speed = sprinting ? sprintSpeed : walkSpeed;

        // Calculate current Velocity
        float magOfMovement = (Mathf.Sqrt(sideMoveAm * sideMoveAm + forwardMoveAm * forwardMoveAm));
        float playerVelocityMagnitude = new Vector2(playerVelocity.x, playerVelocity.z).magnitude;

        currentVelocity = magOfMovement * speed * (airDashing ? 0 : 1) + playerVelocityMagnitude;
        playerAnimator.speed = currentVelocity / 10f + 0.5f;
        isMoving = currentVelocity > 0.75f;

        ableToAirDash = !isGrounded && playerVelocity.y > -1f && !airDashing;
    }

    IEnumerator Dodging()
    {
        if (isGrounded && !isDodging)
        {
            Vector3 dod = new Vector3(sideMoveAm, 0f, forwardMoveAm) * dodgeSpeed;
            var moveDir = (cameraTransform.TransformDirection(dod)); moveDir.y = 0f;
            if (moveDir.magnitude != 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);

                isDodging = true; float timer = 0;

                while (timer < dodgeTime)
                {
                    controller.Move(moveDir * Time.deltaTime);
                    if (!shiftLock) player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, 0.25f); // Only turn to face dash direction if player is not in shift lock, otherwise we want to face forward
                    timer += Time.deltaTime;
                    yield return null;
                }

                isDodging = false;
            }
        }
    }

    void move()
    {
        setSpeed();

        //playerAnimator.SetTrigger(isMoving ? "Go" : "Stop");
        if (isMoving)
        {
            playerAnimator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
        }
        else
        {
            playerAnimator.SetFloat("Speed", 0.0f, 0.1f, Time.deltaTime);
        }

        if (!airDashing && !isDodging)
        {
            //////////////////    WE MOVIN HERE     ///////////////////////////////////////
            Vector3 moveInput = new Vector3(sideMoveAm, 0f, forwardMoveAm);
            var moveDir = (cameraTransform.TransformDirection(moveInput)); moveDir.y = 0f;
            controller.Move(moveDir * speed * Time.deltaTime);

            if (moveInput != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);

                if (shiftLock)
                {
                    Vector3 playerForward = cameraTransform.TransformDirection(Vector3.forward); playerForward.y = 0f;
                    targetRotation = Quaternion.LookRotation(playerForward);
                }

                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, 0.05f);
            }

            if (shiftLock)
            {
                Vector3 playerForward = cameraTransform.TransformDirection(Vector3.forward); playerForward.y = 0f;
                Quaternion targetRotation = Quaternion.LookRotation(playerForward);
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, 0.05f);
            }

        }

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity = Vector3.down * 3.5f; // Reset player velocity to (0, -3.5, 0)



        playerVelocity.y += gravity * Time.deltaTime * (isDodging ? 3f : 1f);

        controller.Move(playerVelocity * Time.deltaTime); // This only affects the player falling and air dashing

        trail.enabled = isDodging;
    }



    //// CAMERA ////

    void handleCamera()
    {
        float speedHeight = currentVelocity / 4f;
        float speedDepth = currentVelocity / 2f;

        camHeight = initialCamHeight + speedHeight;
        camDistance = initialCamDistance + speedDepth;

        if (shiftLock)
        {
            camHeight = 6.5f;
            camDistance = 7f;
        }

        // Position the camera around the player
        cameraTransform.localPosition = new Vector3(Mathf.Sin(cameraAngle * Mathf.PI / 180f) * actualCamDistance, actualCamHeight, Mathf.Cos(cameraAngle * Mathf.PI / 180f) * actualCamDistance);

        float distToCam = Vector3.Distance(transform.position, cameraTransform.position);
        Vector3 toCamera = Vector3.Normalize(cameraTransform.position - transform.position); RaycastHit hit;
        if (Physics.Raycast(transform.position, toCamera, out hit, distToCam * 1.1f))
        {
            if (hit.transform.gameObject.layer != 6 || true) // Does not hit ground < ALWAYS WILL PASS RIGHT NOW
            {
                float DistToObject = Vector3.Distance(hit.point, transform.position);
                actualCamDistance += (DistToObject - actualCamDistance) / 30f;
            }
        }

        actualCamHeight += (camHeight - actualCamHeight) / 50f;
        actualCamDistance += (camDistance - actualCamDistance) / 35f;

        // Finally Look at the player
        cameraTransform.LookAt(transform.position + cameraTransform.forward + Vector3.up);

        /*// Unless boss is in air
        if (bossSpawned && bossFalling)
        {
            shiftLock = true;
            cameraTransform.LookAt(bossScript.gameObject.transform.position + Vector3.down);
            cameraAngle = 180f;
            actualCamDistance = 5f;
            actualCamHeight = 1f;
        }/**/
    }




    //// AUDIO ////

    void handleAudio()
    {
        footstepsSound.enabled = (isMoving && isGrounded);

        // Set Pitch of Footsteps Audio based on the player speed
        footstepsSound.pitch = (1.35f - 0.8f) / 10f * Mathf.Clamp(currentVelocity, 0f, sprintSpeed) + 0.8f;


    }

    ///
    /// 
    /// 
    /// 
    ///// RANDOM NEEDS TO BE MOVED //////

    void hardCodedKeyboardPlayerInput()
    {
        if (isPlayerOne)
        {
            forwardMoveAm = 0;
            sideMoveAm = 0;

            if (tutorialScript.playersCanMove)
            {
                if (Input.GetKey(KeyCode.W))
                    forwardMoveAm = 1;
                if (Input.GetKey(KeyCode.S))
                    forwardMoveAm = -1;

                if (tutorialScript.playerHasTouchedTable)
                {
                    if (Input.GetKey(KeyCode.A))
                        sideMoveAm = -1;
                    if (Input.GetKey(KeyCode.D))
                        sideMoveAm = 1;
                }
            }
                

            if (Input.GetMouseButtonDown(1))
                OnDodge();

            sprinting = Input.GetKey(KeyCode.LeftShift) && !shiftLock;

            if (tutorialScript.playersCanMove)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    OnJump();
            }

            if (Input.GetKeyDown(KeyCode.E) && !sprinting)
                shiftLock = !shiftLock;

            if (Input.GetKeyDown(KeyCode.Q))
                GetComponent<playerAccessWeapons>().OnDropItem();

        }
    }


    public Vector3 getLookDirection()
    {
        return cameraTransform.forward * 4f + Vector3.up * 2;
    }

    public void TutorialParts()
    {
        if (Time.timeSinceLevelLoad < tutorialScript.beginningMovementLockTime)
        {
            cameraAngle = 225f;
            actualCamHeight = 4f;
        }
    }
}