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
    public float xSensKeyboard;
    public float ySensController;
    public float ySensKeyboard;

    private float horizontalTurnAmount;
    private float verticalTurnAmount;


    [Header("Moving")]
    public bool sprinting;
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpHeight;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private float forwardMoveAm;
    private float sideMoveAm;
    private bool isGrounded;
    private float speed;
    private float gravity = Physics.gravity.y;

    [Header("Air Dashing")]
    public bool ableToAirDash;
    public float airDashSpeed;


    [Header("Camera")]
    public float camSideOffset;
    public float camHeight;
    public float actualCamHeight;
    public float camDistance;
    public float actualCamDistance;
    public float minHeightOverGround;
    public float xRotation;


    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        controller = GetComponent<CharacterController>();

        actualCamDistance = camDistance;
        actualCamHeight = camHeight;
    }

    void Update()
    {
        checkIsGrounded();

        // Turning // 
        horizontalTurns();
        verticalTurns();

        // Moving //
        move();

        // Camera
        handleCamera();
    }


    ///////// Jumping //////////

    void Jump()
    {
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void OnControllerJump()
    {
        if ((isPlayerOne && gameScript.playerOneControls == "Controller") || (!isPlayerOne && gameScript.playerTwoControls == "Controller"))
        {
            if (isGrounded)
                Jump();
            else if (playerVelocity.y > 0f && ableToAirDash) // Dash if Player is not on ground and has not started falling down
            {
                Vector3 moveDirection = new Vector3(sideMoveAm, 0f, forwardMoveAm) * airDashSpeed + Vector3.up;
                playerVelocity += (transform.TransformDirection(moveDirection));
                ableToAirDash = false;
            }
        }
    }

    void OnKeyboardJump()
    {
        if ((isPlayerOne && gameScript.playerOneControls == "Keyboard") || (!isPlayerOne && gameScript.playerTwoControls == "Keyboard"))
        {
            if (isGrounded)
                Jump();
            else if (playerVelocity.y > 0f && ableToAirDash) // Dash if Player is not on ground and has not started falling down
            {
                Vector3 moveDirection = new Vector3(sideMoveAm, 0f, forwardMoveAm) * airDashSpeed + Vector3.up;
                playerVelocity += (transform.TransformDirection(moveDirection));
                ableToAirDash = false;
            }
        }

        
    }

    void checkIsGrounded()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded) ableToAirDash = true;
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

        xRotation = Mathf.Clamp(xRotation, -55f, 55f);
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
        if (sprinting && (Mathf.Abs(forwardMoveAm) > 0.05f || Mathf.Abs(sideMoveAm) > 0.05f))
            speed = sprintSpeed;
        else
            speed = walkSpeed;

        
    }

    void move()
    {
        setSpeed();


        Vector3 moveDirection = new Vector3(sideMoveAm, 0f, forwardMoveAm);
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity = Vector3.down * 2f;

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }



    //// CAMERA ////
    
    void handleCamera()
    {
        float magOfMovement = (Mathf.Sqrt(sideMoveAm * sideMoveAm + forwardMoveAm * forwardMoveAm));

        float scaledCamDistance = camDistance;
        if (magOfMovement > 0.5f)
            scaledCamDistance += (magOfMovement-0.5f);


        // Position the camera behind
        cameraTransform.localPosition = new Vector3(camSideOffset, actualCamHeight, -actualCamDistance);
        float distToCam = Vector3.Distance(transform.position, cameraTransform.position);


        Vector3 toCamera = Vector3.Normalize(cameraTransform.position - transform.position); RaycastHit hit2;
        if (Physics.Raycast(transform.position, toCamera, out hit2, distToCam * 1.25f))
        {
            float DistToObject = Vector3.Distance(hit2.point, transform.position);
            actualCamHeight += 0.025f;
            if (actualCamDistance < camDistance * 3f && DistToObject < actualCamDistance)
            {
                actualCamDistance += (DistToObject - actualCamDistance) / 10f; // Smaller number is faster
            }
        }
        else
            actualCamDistance += (camDistance - actualCamDistance) / 150f; // Bigger number is faster

        if (actualCamHeight > camHeight)
        {
            actualCamHeight += (camHeight - actualCamHeight) / 100f;
        }

        // Finally, Look at the player
        cameraTransform.LookAt(transform.position);
    }


}
