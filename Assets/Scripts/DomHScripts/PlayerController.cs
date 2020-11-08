using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(PlayerRespawn))]
public class PlayerController : MonoBehaviour
{
    List<Interactable> m_InteractablesList = new List<Interactable>();

    enum Player
    {
        One = 1,
        Two = 2
    }

    [SerializeField]
    private Player m_Player;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float distanceToGround;
    public float playerSpeed;
    private const float top_speed = 10f;
    private const float defaultWaddlingSpeed = 4f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private const float drag = 0.5f;

    public CapsuleCollider collider;

    public Rigidbody rb;
    public Transform cam;
#if CINEMACHINE
    public GameObject player_2;
    public Transform camLookAt;

    public CinemachineFreeLook freelookcam;

    private CinemachineFreeLook.Orbit[] originalOrbits;
#endif

    public GameObject player_2;
    public Transform camLookAt;

    public CinemachineFreeLook freelookcam;

    private CinemachineFreeLook.Orbit[] originalOrbits;

    private float horizontal;
    private float vertical;
    private Vector3 direction;
    private Vector3 moveDirection;

    private float targetAngle;
    private float angle;

    float TurnSmoothVelocity;

    private Boolean Sliding;
    private Boolean Running;
    private Boolean Jumping;
    

    private void Start()
    {

#if CINEMACHINE
        originalOrbits = new CinemachineFreeLook.Orbit[freelookcam.m_Orbits.Length];
        for (int i = 0; i < freelookcam.m_Orbits.Length; i++)
        {
            originalOrbits[i].m_Height = freelookcam.m_Orbits[i].m_Height;
            originalOrbits[i].m_Radius = freelookcam.m_Orbits[i].m_Radius;
        }
#endif

        originalOrbits = new CinemachineFreeLook.Orbit[freelookcam.m_Orbits.Length];
        for (int i = 0; i < freelookcam.m_Orbits.Length; i++)
        {
            originalOrbits[i].m_Height = freelookcam.m_Orbits[i].m_Height;
            originalOrbits[i].m_Radius = freelookcam.m_Orbits[i].m_Radius;
        }
    }

    void Update()
    {

#if CINEMACHINE
        camLookAt.position = Vector3.Lerp(transform.position, player_2.transform.position, 0.5f);

        for (int i = 0; i < freelookcam.m_Orbits.Length; i++)
        {
            freelookcam.m_Orbits[i].m_Radius = Mathf.Lerp(freelookcam.m_Orbits[i].m_Radius, originalOrbits[i].m_Radius * (1 + (0.5f * Vector3.Distance(transform.position, camLookAt.position))), 0.1f);
            freelookcam.m_Orbits[i].m_Height = Mathf.Lerp(freelookcam.m_Orbits[i].m_Height, originalOrbits[i].m_Height * (1 + (0.5f * Vector3.Distance(transform.position, camLookAt.position))), 0.1f);
        }

        freelookcam.m_Lens.FieldOfView =Mathf.Clamp(95f - (Vector3.Distance(transform.position, camLookAt.position) * 0.75f), 60f,95f) ;

#endif

        camLookAt.position = Vector3.Lerp(transform.position, player_2.transform.position, 0.5f);

        for (int i = 0; i < freelookcam.m_Orbits.Length; i++)
        {
            freelookcam.m_Orbits[i].m_Radius = Mathf.Lerp(freelookcam.m_Orbits[i].m_Radius, originalOrbits[i].m_Radius * (1 + (0.5f * Vector3.Distance(transform.position, camLookAt.position))), 0.05f);
            freelookcam.m_Orbits[i].m_Height = Mathf.Lerp(freelookcam.m_Orbits[i].m_Height, originalOrbits[i].m_Height * (1 + (0.5f * Vector3.Distance(transform.position, camLookAt.position))), 0.05f);
        }

        freelookcam.m_Lens.FieldOfView = Mathf.Clamp(95f - (Vector3.Distance(transform.position, camLookAt.position) * 0.75f), 60f, 95f);


        distanceToGround = collider.bounds.extents.y;        

        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.2f);

        /*
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;            
        }
        */
        if (groundedPlayer && Jumping)
        {
            Jumping = false;
        }
        if (m_Player == Player.One)
        {
            horizontal = Input.GetAxisRaw("HorizontalPlayer1");
            vertical = Input.GetAxisRaw("VerticalPlayer1");
        }
        else if (m_Player == Player.Two)
        {
            horizontal = Input.GetAxisRaw("HorizontalPlayer2");
            vertical = Input.GetAxisRaw("VerticalPlayer2");
        }
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        


        if (direction.magnitude >= 0.1f) // Player Movement Input
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            if (Running)
            {
                if (playerSpeed <= top_speed)
                {
                    playerSpeed += 0.25f * direction.magnitude; // ground acceleration to waddling speed
                }
            }
            else
            {
                if (playerSpeed <= defaultWaddlingSpeed)
                {
                    playerSpeed += 1.0f * direction.magnitude; // ground acceleration to waddling speed
                }
            }

        }

        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, playerSpeed * 0.01f);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (playerSpeed <= 0)
        {
            playerSpeed = 0f;
            Sliding = false;
        }
        else
        {
            playerSpeed -= drag * Time.deltaTime;
            if (groundedPlayer)
            {
                
                if (Sliding)
                {
                    playerSpeed -= 0.25f * Time.deltaTime;
                    Debug.Log("Sliding!");
                }
                else
                {
                    playerSpeed -= 8f * Time.deltaTime;
                }
            }
        }

        moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        //controller.Move(moveDirection.normalized * Time.deltaTime * playerSpeed);

        //rb.AddForce(moveDirection.normalized * playerSpeed * 10000f, ForceMode.Acceleration);
        


        // Changes the height position of the player..
        if (m_Player == Player.One)
        {
            if (Input.GetButtonDown("JumpPlayer1") && groundedPlayer)
            {
                Jumping = true;
                //playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                moveDirection.y += jumpHeight * -3.0f * gravityValue;
                //rb.AddForce(Vector3.up * jumpHeight * -3.0f * gravityValue);
            }


            if (Input.GetButton("RunPlayer1") && groundedPlayer)
            {
                Running = true;
                Sliding = false;
            }
            else
            {
                Running = false;
            }

            if (Input.GetButtonDown("SlidePlayer1") && playerSpeed > 0)
            {
                Sliding = true;

            }
        }
        else if (m_Player == Player.Two)
        {
            if (Input.GetButtonDown("JumpPlayer2") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }


            if (Input.GetButton("RunPlayer2") && groundedPlayer)
            {
                Running = true;
                Sliding = false;
            }
            else
            {
                Running = false;
            }

            if (Input.GetButtonDown("SlidePlayer2") && playerSpeed > 0)
            {
                Sliding = true;

            }
        }

        //playerVelocity.y += gravityValue * Time.deltaTime;
        moveDirection.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);


        if (m_Player == Player.One)
        {
            if (Input.GetButtonDown("InteractPlayer1"))
            {
                for (int i = 0; i < m_InteractablesList.Count; ++i)
                    m_InteractablesList[i].Interact();
            }
        }
        else if (m_Player == Player.Two)
        {
            if (Input.GetButtonDown("InteractPlayer2"))
            {
                for (int i = 0; i < m_InteractablesList.Count; ++i)
                    m_InteractablesList[i].Interact();
            }
        }


    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection.normalized * playerSpeed;
        //rb.velocity = playerVelocity;

    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.gameObject.GetComponent<Interactable>();
        if (interactable != null)
        {
            if (m_InteractablesList.Contains(interactable) == false)
                m_InteractablesList.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.gameObject.GetComponent<Interactable>();
        if (interactable != null)
        {
            if (m_InteractablesList.Contains(interactable) == true)
                m_InteractablesList.Remove(interactable);
        }
    }
}
