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

public class playerMovement : MonoBehaviour
{
    gameHandler gameScript;
    CharacterController controller;

    public bool isPlayerOne;
    public bool hardCodeKeyboard;
    public GameObject player;
    public Transform forwardTransform;
    public Transform cameraTransform;
    public Transform playerObj;


    [Header("Moving")]
    public float currentVelocity;
    public float walkSpeed;
    public float sprintSpeed;

    private float speed;
    private bool isMoving;
    private bool sprinting;
    private float forwardMoveAm;
    private float sideMoveAm;
    private Vector3 playerVelocity;


    [Header("Jumping")]
    public float groundCheckDistance;
    public float jumpHeight;
    public float airDashSpeed;

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
    public float xSensController;
    public float ySensController;
    public float xSensKeyboard;
    public float ySensKeyboard;

    private float horizontalTurnAmount;
    private float verticalTurnAmount;


    [Header("Camera")]
    public float camHeight;
    float initialCamHeight;
    float actualCamHeight;
    public float camDistance;
    float initialCamDistance;
    float actualCamDistance;
    public float minHeightOverGround;
    float xRotation;
    float yRotation;


    [Header("Camera Follow")]
    public float cameraAngle;


    [Header("Audio")]
    public AudioSource footstepsSound;
    public AudioSource jumpSound;
    public AudioClip jumpSoundEffect;
    public AudioSource landingOnGrass;
    public AudioClip landingOnGrassSound;


    [Header("Animation")]
    Animator playerAnimator;


    [Header("Trails")]
    public TrailRenderer trail;


    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        controller = GetComponent<CharacterController>();
        playerAnimator = player.GetComponent<Animator>();

        initialCamDistance = camDistance; actualCamDistance = camDistance;
        initialCamHeight = camHeight; actualCamHeight = camHeight;
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

        trail.enabled = isDodging;
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

        horizontalTurnAmount = turnInput[0];
        verticalTurnAmount = turnInput[1];
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

        /*
         * This was the origonal stuff
        float turnStrength = isPlayerOne ? ySensKeyboard : ySensController;
        xRotation -= verticalTurnAmount * turnStrength * Time.deltaTime;
        forwardTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        */

        /*
         * Tried a few things, nothin workin
        float xmouse = Input.GetAxis("Mouse X");
        float turnStrength = isPlayerOne ? ySensKeyboard : ySensController;
        xRotation += xmouse * turnStrength * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -50f, 55f);
        Vector3 LOOK = new Vector3(1f, 0f, -10f);
        Quaternion look2 = Quaternion.Euler(xRotation, 0f, 0f);
        cameraTransform.position = forwardTransform.position + look2 * LOOK;
        cameraTransform.LookAt(forwardTransform);
        */

        /*
         * This was a last ditch effort
        float xmouse = Input.GetAxis("Mouse X");
        float turnStrength = isPlayerOne ? ySensKeyboard : ySensController;
        forwardTransform.eulerAngles += new Vector3(xmouse * turnStrength, 0, 0);
        */


    }

    //// Moving ////

    void OnMove(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        sideMoveAm = turnInput[0];
        forwardMoveAm = turnInput[1];

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
        isMoving = currentVelocity > 0.75f;

        ableToAirDash = !isGrounded && playerVelocity.y > -1f && !airDashing;
    }

    IEnumerator Dodging()
    {
        if (isGrounded && !isDodging)
        {
            Vector3 dod = new Vector3(sideMoveAm, 0f, forwardMoveAm) * dodgeSpeed;
            var moveDir = (cameraTransform.TransformDirection(dod)); moveDir.y = 0f;
            if (moveDir.magnitude != 0){
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
                
                if (shiftLock) {
                    Vector3 playerForward = cameraTransform.TransformDirection(Vector3.forward); playerForward.y = 0f;
                    targetRotation = Quaternion.LookRotation(playerForward); }
                
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

    }



    //// CAMERA ////
    
    void handleCamera()
    {
        float speedHeight = currentVelocity / 6f;
        float speedDepth = currentVelocity / 2f;

        camHeight = initialCamHeight + speedHeight;
        camDistance = initialCamDistance + speedDepth;

        if (shiftLock)
        {
            camHeight = 5f;
            camDistance = 7f;
        }
        
        // Position the camera around the player
        cameraTransform.localPosition = new Vector3(Mathf.Sin(cameraAngle * Mathf.PI / 180f) * actualCamDistance,   actualCamHeight,   Mathf.Cos(cameraAngle * Mathf.PI / 180f) * actualCamDistance);

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

        actualCamHeight += (camHeight - actualCamHeight) / 150f;
        actualCamDistance += (camDistance - actualCamDistance) / 75f;

        // Finally Look at the player
        cameraTransform.LookAt(transform.position + cameraTransform.forward + Vector3.up);
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
            if (Input.GetKey(KeyCode.W))
                forwardMoveAm = 1;
            if (Input.GetKey(KeyCode.S))
                forwardMoveAm = -1;

            sideMoveAm = 0;
            if (Input.GetKey(KeyCode.A))
                sideMoveAm = -1;
            if (Input.GetKey(KeyCode.D))
                sideMoveAm = 1;

            if (Input.GetMouseButtonDown(1))
                OnDodge();

            sprinting = Input.GetKey(KeyCode.LeftShift) && !shiftLock;

            if (Input.GetKeyDown(KeyCode.Space))
                OnJump();

            if (Input.GetKeyDown(KeyCode.E) && !sprinting)
                shiftLock = !shiftLock;

            if (Input.GetKeyDown(KeyCode.Q))
                GetComponent<playerAccessWeapons>().OnDropItem();

            if (Input.GetKeyDown(KeyCode.R))
                gameScript.possibleRecipes();

        }
    }


    public Vector3 getLookDirection()
    {
        return cameraTransform.forward * 4f + Vector3.up * 2;
    }

}