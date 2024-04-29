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
    public bool isPlayerOne;
    public GameObject player;
    public Transform forwardTransform;
    public Transform cameraTransform;
    public Transform playerObj;


    


    [Header("Moving")]
    public float currentVelocity;
    public bool isMoving;
    public bool isDodging;
    public float walkSpeed;
    public float sprintSpeed;
    public bool sprinting;

    [Header("Jumping")]
    public bool isGrounded;
    public float groundCheckDistance;
    public float jumpHeight;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private float forwardMoveAm;
    private float sideMoveAm;
    private float speed;
    private float dodgeTime; //Will use with dodge animation
    private float gravity = Physics.gravity.y;

    [Header("Air Dashing")]
    public bool ableToAirDash;
    public bool airDashing;
    public float airDashSpeed;

    [Header("Turning")]
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

        // Moving //
        move();

        // Turning // 
        horizontalTurns();
        verticalTurns();
        Turning();


        // Camera //
        handleCamera();

        // Audio //
        handleAudio();

    }


    ///////// Jumping //////////

    void Jump()
    {
        if (!isDodging)
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void airDash()
    {
        Vector3 moveDirection = new Vector3(sideMoveAm, 0.5f, forwardMoveAm) * airDashSpeed + Vector3.up;
        playerVelocity += (cameraTransform.TransformDirection(moveDirection));
        airDashing = true;

        jumpSound.PlayOneShot(jumpSoundEffect);
    }

    void OnControllerJump()
    {
        if ((isPlayerOne && gameScript.playerOneControls == "Controller") || (!isPlayerOne && gameScript.playerTwoControls == "Controller"))
        {
            if (isGrounded)
                Jump();
            else if (ableToAirDash)
                airDash();
        }
    }

    void OnKeyboardJump()
    {
        if ((isPlayerOne && gameScript.playerOneControls == "Keyboard") || (!isPlayerOne && gameScript.playerTwoControls == "Keyboard"))
        {
            if (isGrounded)
                Jump();
            else if (ableToAirDash) // Dash if Player is not on ground and has not started falling down
                airDash();
        }

        
    }

    void checkIsGrounded()
    {
        bool prev = isGrounded;
        
        isGrounded = controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);

        if (!prev && isGrounded && playerVelocity.y < -6f)
        {   // We just landed
            landingOnGrass.PlayOneShot(landingOnGrassSound);
        }

        if (isGrounded) airDashing = false;
    }
    

    //////// Turning ///////////
    void OnControllerTurn(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        if (isPlayerOne && gameScript.playerOneControls == "Controller" || (!isPlayerOne && gameScript.playerTwoControls == "Controller"))
        {
            horizontalTurnAmount = turnInput[0];
            verticalTurnAmount = turnInput[1];
        }

    }

    void OnKeyboardTurn(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        if (isPlayerOne && gameScript.playerOneControls == "Keyboard" || (!isPlayerOne && gameScript.playerTwoControls == "Keyboard"))
        {
            horizontalTurnAmount = turnInput[0];
            verticalTurnAmount = turnInput[1];
        }
    }

    void Turning()
    {

        
        
    }

    void horizontalTurns()
    {
        float turnStrength = gameScript.playerOneControls == "Keyboard" ? xSensKeyboard : xSensController;
        cameraAngle += horizontalTurnAmount * turnStrength * Time.deltaTime;
        if (cameraAngle > 360f)  cameraAngle -= 360f;
        if (cameraAngle <   0f)  cameraAngle += 360f;
    }

    void verticalTurns()
    {
        /*if (isPlayerOne)
        {
            float turnStrength = gameScript.playerOneControls == "Keyboard" ? ySensKeyboard : ySensController;
            xRotation -= verticalTurnAmount * turnStrength * Time.deltaTime;
        }
        else
        {
            float turnStrength = gameScript.playerTwoControls == "Keyboard" ? ySensKeyboard : ySensController;
            xRotation -= verticalTurnAmount * turnStrength * Time.deltaTime;
        }

        xRotation = Mathf.Clamp(xRotation, -50f, 55f);
        forwardTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);*/

    }

    //// Moving ////

    void OnControllerMove(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        if ((isPlayerOne && gameScript.playerOneControls == "Controller") || (!isPlayerOne && gameScript.playerTwoControls == "Controller"))
        {
            sideMoveAm = turnInput[0];
            forwardMoveAm = turnInput[1];
        }

    }

    void OnKeyboardMove(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        if ((isPlayerOne && gameScript.playerOneControls == "Keyboard") || (!isPlayerOne && gameScript.playerTwoControls == "Keyboard"))
        {
            sideMoveAm = turnInput[0];
            forwardMoveAm = turnInput[1];
        }

    }

    void OnControllerSprint()
    {
        sprinting = !sprinting;
    }

    void OnKeyboardSprint()
    {
        sprinting = !sprinting;
    }

    void OnControllerDodge()
    {
        StartCoroutine(Dodging());
    }

    void OnKeyboardDodge()
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
            isDodging = true;
            float timer = 0;
            while (timer < .5f)
            {
                
                speed = sprintSpeed;
                Vector3 dod = (new Vector3(sideMoveAm, 0f, forwardMoveAm) * (speed * 2)) + (Vector3.up * (Time.deltaTime * gravity));
                controller.Move(cameraTransform.TransformDirection(dod) * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }
            speed = walkSpeed;
            isDodging = false;
        }
    }

    void move()
    {
        setSpeed();

        playerAnimator.SetTrigger(isMoving ? "Go" : "Stop");


        if (!airDashing && !isDodging)
        {
            //////////////////    WE MOVIN HERE     ///////////////////////////////////////
            Vector3 moveDirection = new Vector3(sideMoveAm, 0f, forwardMoveAm);
            controller.Move(cameraTransform.TransformDirection(moveDirection) * speed * Time.deltaTime);


            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(cameraTransform.TransformDirection(moveDirection).x, 0f, cameraTransform.TransformDirection(moveDirection).z));
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, 0.15f);
            }

        }

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity = Vector3.down * 3.5f;

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }



    //// CAMERA ////
    
    void handleCamera()
    {
        float speedHeight = currentVelocity / 6f;
        float speedDepth = currentVelocity / 2f;

        camHeight = initialCamHeight + speedHeight;
        camDistance = initialCamDistance + speedDepth;

        
        cameraTransform.localPosition = new Vector3(Mathf.Sin(cameraAngle * Mathf.PI / 180f) * actualCamDistance, actualCamHeight, Mathf.Cos(cameraAngle * Mathf.PI / 180f) * actualCamDistance);


        float distToCam = Vector3.Distance(transform.position, cameraTransform.position);
        Vector3 toCamera = Vector3.Normalize(cameraTransform.position - transform.position); RaycastHit hit;
        if (Physics.Raycast(transform.position, toCamera, out hit, distToCam * 1.15f))
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
        cameraTransform.LookAt(transform.position);
    }




    //// AUDIO ////
    
    void handleAudio()
    {
        footstepsSound.enabled = (isMoving && isGrounded);
        
        // Set Pitch of Footsteps Audio based on the player speed
        footstepsSound.pitch = (1.35f - 0.8f) / 10f * Mathf.Clamp(currentVelocity, 0f, sprintSpeed) + 0.8f;


    }
}


/*void handleCamera()
    {
        float speedHeight = currentVelocity / 6f;
        float speedDepth = Mathf.Clamp(currentVelocity, 5f, airDashSpeed) / 2f;

        camSideOffset = Mathf.Max(0f, initialCamSideOffset - currentVelocity / walkSpeed * initialCamSideOffset);
        camHeight = initialCamHeight + speedHeight;
        camDistance = initialCamDistance + speedDepth;
        
        // Position the camera behind the player
        cameraTransform.localPosition = new Vector3(actualCamSideOffset, actualCamHeight, -actualCamDistance);


        float distToCam = Vector3.Distance(transform.position, cameraTransform.position);
        Vector3 toCamera = Vector3.Normalize(cameraTransform.position - transform.position); RaycastHit hit;
        if (Physics.Raycast(transform.position, toCamera, out hit, distToCam * 1.15f))
        {
            if (hit.transform.gameObject.layer != 6) // Does not hit ground
            {
                float DistToObject = Vector3.Distance(hit.point, transform.position);
                if (actualCamDistance < camDistance && DistToObject < actualCamDistance)
                    actualCamDistance += (DistToObject - actualCamDistance) / 30f;
            }
        }

        RaycastHit hit2;
        if (Physics.Raycast(cameraTransform.position + Vector3.up * 10f, Vector3.down, out hit2, 10f + minHeightOverGround))
            if (hit2.transform.gameObject.layer == 6)
                actualCamHeight += 0.05f;



        actualCamSideOffset += (camSideOffset - actualCamSideOffset) / 30f;
        actualCamHeight += (camHeight - actualCamHeight) / 150f;
        actualCamDistance += (camDistance - actualCamDistance) / 75f;

        // Finally Look at the player
        cameraTransform.LookAt(transform.position);
    }*/