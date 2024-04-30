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

public class CINEMACHINEplayerMovement : MonoBehaviour
{
    gameHandler gameScript;
    public bool isPlayerOne;
    public GameObject player;
    public Transform playerObj;
    public Transform forwardTransform;
    public Transform cameraTransform;


    [Header("Moving")]
    [SerializeField] private float currentVelocity;
    public float walkSpeed;
    public float sprintSpeed;
    private bool sprinting;
    private bool isMoving;

    //[Header("Dodging")]
    public float dodgeSpeed;
    private bool isDodging;

    //[Header("Air Dashing")]
    public float airDashSpeed;
    private bool airDashing;
    private bool ableToAirDash;

    [Header("Jumping")]
    public float groundCheckDistance;
    public float jumpHeight;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private float forwardMoveAm;
    private float sideMoveAm;
    private float speed;
    private float dodgeTime; //Will use with dodge animation
    private float gravity = Physics.gravity.y;
    private bool isGrounded;

    


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

    }

    void Update()
    {
        checkIsGrounded();

        // Moving //
        move();

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
        Vector3 moveDirection = new Vector3(sideMoveAm, 0f, forwardMoveAm) * airDashSpeed;
        var moveDir = cameraTransform.TransformDirection(moveDirection); moveDir.y = 1f; // This is our new up force
        playerVelocity += (moveDir);
        airDashing = true;

        jumpSound.PlayOneShot(jumpSoundEffect);
    }


    void OnJump()
    {
        if (isGrounded)
            Jump();
        else if (ableToAirDash)
            airDash();
    }

    void checkIsGrounded()
    {
        bool prev = isGrounded;

        isGrounded = controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if (!prev && isGrounded && playerVelocity.y < -6f)
        {   // We just landed
            landingOnGrass.PlayOneShot(landingOnGrassSound);
        }

        if (isGrounded) airDashing = false;
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
            var moveDir = (forwardTransform.TransformDirection(dod)); moveDir.y = 0f;

            isDodging = true; float timer = 0;

            while (timer < .5f)
            {
                controller.Move(moveDir * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }
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
            Vector3 moveInput = new Vector3(sideMoveAm, 0f, forwardMoveAm);
            var moveDir = (cameraTransform.TransformDirection(moveInput)); moveDir.y = 0f;
            controller.Move(moveDir * speed * Time.deltaTime);

            if (moveInput != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, 0.15f);
            }
        }

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity = Vector3.down * 3.5f; // Reset player velocity to (0, -3.5, 0)



        playerVelocity.y += gravity * Time.deltaTime * (isDodging ? 3f : 1f);

        controller.Move(playerVelocity * Time.deltaTime); // This only affects the player falling and air dashing

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