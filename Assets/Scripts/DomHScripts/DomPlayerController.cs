using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PlayerRespawn))]
public class DomPlayerController : MonoBehaviour
{
    List<Interactable> m_InteractablesList = new List<Interactable>();

    public enum Player
    {
        One = 1,
        Two = 2
    }

    public Player m_Player;

    [Range(0f, 10f)]
    public float movePower = 2f;
    public float m_fJumpPower = 1.0f;

    private bool m_bGroundedPlayer;
    private float m_fDistanceToGround;
    private const float m_kfGravity = -9.81f;

    private CapsuleCollider m_Collider;
    private Rigidbody m_Rigidbody;
    //private Transform m_CameraTransform;

    private bool m_bSliding;
    private bool m_bRunning;
    private bool m_bJumping;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<CapsuleCollider>();
    }

    void FixedUpdate()
    {
        m_fDistanceToGround = m_Collider.bounds.extents.y;

        m_bGroundedPlayer = Physics.Raycast(transform.position, Vector3.down, m_fDistanceToGround + 0.2f);

        if (m_bGroundedPlayer && m_bJumping)
        {
            m_bJumping = false;
        }

        if (m_Player == Player.One)
        {
            m_Rigidbody.AddForce((Vector3.back * movePower * Input.GetAxis("HorizontalPlayer1")) + (Vector3.right * movePower * Input.GetAxis("VerticalPlayer1")));
        }
        else if (m_Player == Player.Two)
        {
            m_Rigidbody.AddForce((Vector3.back * movePower * Input.GetAxis("HorizontalPlayer2")) + (Vector3.right * movePower * Input.GetAxis("VerticalPlayer2")));
        }

        //angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, 1f * Time.deltaTime);

        // transform.rotation = Quaternion.Euler(0f, angle, 0f);


        //moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        //Jump
        if (m_bJumping)
            m_Rigidbody.AddForce(Vector3.up * m_fJumpPower * -3.0f * m_kfGravity);
    }

    void Update()
    {
        //Jump
        if (    (Input.GetButtonDown("JumpPlayer1") && m_bGroundedPlayer && m_Player == Player.One)
            ||  (Input.GetButtonDown("JumpPlayer2") && m_bGroundedPlayer && m_Player == Player.Two))
        {
            m_bJumping = true;
        }

        //Run
        if (    (Input.GetButton("RunPlayer1") && m_bGroundedPlayer && m_Player == Player.One)
            ||  (Input.GetButton("RunPlayer2") && m_bGroundedPlayer && m_Player == Player.Two))
        {
            m_bRunning = true;
            m_bSliding = false;
        }
        else
        {
            m_bRunning = false;
        }

        //Slide
        if (    (Input.GetButtonDown("SlidePlayer1") && m_Player == Player.One)
            ||  (Input.GetButtonDown("SlidePlayer2") && m_Player == Player.Two))
        {
            m_bSliding = true;
        }

        //Interact
        if (    (Input.GetButtonDown("InteractPlayer1") && m_Player == Player.One)
            ||  (Input.GetButtonDown("InteractPlayer2") && m_Player == Player.Two))
        {
            for (int i = 0; i < m_InteractablesList.Count; ++i)
                m_InteractablesList[i].Interact();
            Debug.Log("Interact");
        }
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
