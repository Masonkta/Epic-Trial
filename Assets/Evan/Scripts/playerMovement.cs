using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    gameHandler gameScript;
    public bool isPlayerOne;
    public Transform forwardTransform;
    public Transform cameraTransform;


    [Header("Turning")]
    public float xSensController;
    public float ySensController;
    public float xSensKeyboard;
    public float ySensKeyboard;

    private float horizontalTurnAmount;
    private float verticalTurnAmount;


    [Header("Moving")]
    public float currentVelocity;
    public bool isMoving;
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
    private float gravity = Physics.gravity.y;

    [Header("Air Dashing")]
    public bool ableToAirDash;
    public bool airDashing;
    public float airDashSpeed;


    [Header("Camera")]
    public float camSideOffset;
    float initialCamSideOffset;
    float actualCamSideOffset;
    public float camHeight;
    float initialCamHeight;
    float actualCamHeight;
    public float camDistance;
    float initialCamDistance;
    float actualCamDistance;
    public float minHeightOverGround;
    float xRotation;

    [Header("Audio")]
    public AudioSource footstepsSound;
    public AudioSource jumpSound;
    public AudioClip jumpSoundEffect;
    public AudioSource landingOnGrass;
    public AudioClip landingOnGrassSound;
    

    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        controller = GetComponent<CharacterController>();

        initialCamSideOffset = camSideOffset; actualCamSideOffset = camSideOffset;
        initialCamDistance = camDistance; actualCamDistance = camDistance;
        initialCamHeight = camHeight; actualCamHeight = camHeight;
    }

    void Update()
    {
        checkIsGrounded();

        // Turning // 
        horizontalTurns();
        verticalTurns();

        // Moving //
        move();

        // Camera //
        handleCamera();

        // Audio //
        handleAudio();
    }


    ///////// Jumping //////////

    void Jump()
    {
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        jumpSound.PlayOneShot(jumpSoundEffect);
    }

    void OnControllerJump()
    {
        if ((isPlayerOne && gameScript.playerOneControls == "Controller") || (!isPlayerOne && gameScript.playerTwoControls == "Controller"))
        {
            if (isGrounded)
                Jump();
            else if (playerVelocity.y > -1f && ableToAirDash) // Dash if Player is not on ground and has not started falling down
            {
                Vector3 moveDirection = new Vector3(sideMoveAm, 0f, forwardMoveAm) * airDashSpeed + Vector3.up;
                playerVelocity += (transform.TransformDirection(moveDirection));
                ableToAirDash = false; airDashing = true;
            }
        }
    }

    void OnKeyboardJump()
    {
        if ((isPlayerOne && gameScript.playerOneControls == "Keyboard") || (!isPlayerOne && gameScript.playerTwoControls == "Keyboard"))
        {
            if (isGrounded)
                Jump();
            else if (playerVelocity.y > -1f && ableToAirDash) // Dash if Player is not on ground and has not started falling down
            {
                Vector3 moveDirection = new Vector3(sideMoveAm, 0f, forwardMoveAm) * airDashSpeed + Vector3.up;
                playerVelocity += (transform.TransformDirection(moveDirection));
                ableToAirDash = false; airDashing = true;
            }
        }

        
    }

    void checkIsGrounded()
    {
        bool prev = isGrounded;
        Debug.DrawRay(transform.position + Vector3.down, Vector3.down * groundCheckDistance, Color.red);
        isGrounded = Physics.Raycast(transform.position + Vector3.down, Vector3.down, groundCheckDistance);
        if (!prev && isGrounded)
        {
            // We just landed
            landingOnGrass.PlayOneShot(landingOnGrassSound);
        }


        if (isGrounded) ableToAirDash = true;
        if (isGrounded) airDashing = false;
    }


    //////// Turning ///////////
    
    void OnControllerTurn(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        float turnX = turnInput[0];
        float turnY = turnInput[1];

        if (isPlayerOne && gameScript.playerOneControls == "Controller") // This is player 1 and player 1 uses controller
        {
            horizontalTurnAmount = turnX;
            verticalTurnAmount = turnY;
        }

        if (!isPlayerOne && gameScript.playerTwoControls == "Controller") // This is player 2 and player 2 uses controller
        {
            horizontalTurnAmount = turnX;
            verticalTurnAmount = turnY;
        }

    }

    void OnKeyboardTurn(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        float turnX = turnInput[0];
        float turnY = turnInput[1];

        if (isPlayerOne && gameScript.playerOneControls == "Keyboard")
        {
            horizontalTurnAmount = turnX;
            verticalTurnAmount = turnY;
        }

        if (!isPlayerOne && gameScript.playerTwoControls == "Keyboard")
        {
            horizontalTurnAmount = turnX;
            verticalTurnAmount = turnY;
        }

    }

    void horizontalTurns()
    {
        if (isPlayerOne)
        {
            float turnStrength = gameScript.playerOneControls == "Keyboard" ? xSensKeyboard : xSensController;
            transform.Rotate(Vector3.up * horizontalTurnAmount * turnStrength * Time.deltaTime);
        }
        else
        {
            float turnStrength = gameScript.playerTwoControls == "Keyboard" ? xSensKeyboard : xSensController;
            transform.Rotate(Vector3.up * horizontalTurnAmount * turnStrength * Time.deltaTime);
        }
    }

    void verticalTurns()
    {
        if (isPlayerOne)
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
        forwardTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }



    //// Moving ////

    void OnControllerMove(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        float moveX = turnInput[0];
        float moveY = turnInput[1];

        if ((isPlayerOne && gameScript.playerOneControls == "Controller") || (!isPlayerOne && gameScript.playerTwoControls == "Controller")) // This is player 1 and player 1 uses controller
        {
            sideMoveAm = moveX;
            forwardMoveAm = moveY;
        }

    }

    void OnKeyboardMove(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        float moveX = turnInput[0];
        float moveY = turnInput[1];

        if ((isPlayerOne && gameScript.playerOneControls == "Keyboard") || (!isPlayerOne && gameScript.playerTwoControls == "Keyboard")) // This is player 1 and player 1 uses controller
        {
            sideMoveAm = moveX;
            forwardMoveAm = moveY;
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

    void setSpeed()
    {
        if (sprinting)
            speed = sprintSpeed;
        else
            speed = walkSpeed;

        
    }

    void move()
    {
        setSpeed();

        if (!airDashing)
        {
            Vector3 moveDirection = new Vector3(sideMoveAm, 0f, forwardMoveAm);
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        }

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity = Vector3.down * 4f;

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        
    }



    //// CAMERA ////
    
    void handleCamera()
    {
        float magOfMovement = (Mathf.Sqrt(sideMoveAm * sideMoveAm + forwardMoveAm * forwardMoveAm));
        float playerVelocityMagnitude = new Vector2(playerVelocity.x, playerVelocity.z).magnitude;

        currentVelocity = magOfMovement * speed * (airDashing ? 0 : 1) + playerVelocityMagnitude;
        isMoving = currentVelocity > 2f;

        float speedHeight = currentVelocity / 6f;
        float speedDepth = currentVelocity / 3f;

        camSideOffset = Mathf.Max(0f, initialCamSideOffset - currentVelocity / walkSpeed * initialCamSideOffset);
        camHeight = initialCamHeight + speedHeight;
        camDistance = initialCamDistance + speedDepth;
        
        // Position the camera behind the player
        cameraTransform.localPosition = new Vector3(actualCamSideOffset, actualCamHeight, -actualCamDistance);



        float distToCam = Vector3.Distance(transform.position, cameraTransform.position);
        Vector3 toCamera = Vector3.Normalize(cameraTransform.position - transform.position); RaycastHit hit;
        if (Physics.Raycast(transform.position, toCamera, out hit, distToCam * 1.25f))
        {
            if (hit.transform.gameObject.layer != 6) // Does not hit ground
            {
                float DistToObject = Vector3.Distance(hit.point, transform.position);
                if (actualCamDistance < camDistance * 2f && DistToObject < actualCamDistance)
                    actualCamDistance += (DistToObject - actualCamDistance) / 30f; // Smaller number is faster
            }
        }

        RaycastHit hit2;
        if (Physics.Raycast(cameraTransform.position + Vector3.up * 10f, Vector3.down, out hit2, 10f + minHeightOverGround))
            if (hit2.transform.gameObject.layer == 6)
                actualCamHeight += 0.03f;



        actualCamSideOffset += (camSideOffset - actualCamSideOffset) / 30f;
        actualCamHeight += (camHeight - actualCamHeight) / 150f;
        actualCamDistance += (camDistance - actualCamDistance) / 75f; // Bigger number is slower

        // Finally Look at the player
        cameraTransform.LookAt(transform.position);
    }



    //// AUDIO ////
    
    void handleAudio()
    {
        footstepsSound.enabled = (isMoving && isGrounded);
        // Set Pitch of Footsteps Audio based on the player speed



    }
}
