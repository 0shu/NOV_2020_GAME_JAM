using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    List<Interactable> m_InteractablesList = new List<Interactable>();

    #region Unity's example
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed;
    private const float top_speed = 15f;
    private const float defaultWaddlingSpeed = 4f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private const float drag = 0.5f;

    public Transform cam;
    public GameObject SlidingPlayerModel;

    private float horizontal;
    private float vertical;
    private Vector3 direction;
    private Vector3 moveDirection;

    private float targetAngle;
    private float angle;

    float TurnSmoothVelocity;

    private Boolean Sliding;
    private Boolean Running;
    

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
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
                    playerSpeed += 0.5f * direction.magnitude; // ground acceleration to waddling speed
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
                    playerSpeed -= 0.05f * Time.deltaTime;
                    Debug.Log("Sliding!");
                    SlidingPlayerModel.SetActive(true);
                }
                else
                {
                    playerSpeed -= 5.0f * Time.deltaTime;
                    SlidingPlayerModel.SetActive(false);
                }
            }
        }

        moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDirection.normalized * Time.deltaTime * playerSpeed);


        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        if (Input.GetButton("Run") && groundedPlayer)
        {
            Running = true;
            Sliding = false;
        }
        else
        {
            Running = false;
        }

        if (Input.GetButtonDown("Slide") && playerSpeed > 0)
        {
            Sliding = true;
            
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);



        ////NOT UNITY'S EXAMPLE
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < m_InteractablesList.Count; ++i)
                m_InteractablesList[i].Interact();
        }
    }

    #endregion Unity's example

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
