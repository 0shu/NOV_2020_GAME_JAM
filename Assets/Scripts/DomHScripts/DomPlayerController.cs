using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(RespawnPlayer))]
public class DomPlayerController : MonoBehaviour
{
    List<Interactable> m_InteractablesList = new List<Interactable>();
    List<Grabbable> m_GrabbablesList = new List<Grabbable>();

    // Animator
    private Animator pengAnim;
    public enum Player
    {
        One = 1,
        Two = 2
    }

    public Player m_Player;

    [Range(0f, 100f)]
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
        pengAnim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        /*m_fDistanceToGround = m_Collider.bounds.extents.y;

        m_bGroundedPlayer = Physics.Raycast(transform.position, Vector3.down, m_fDistanceToGround + 0.2f);

        if (m_bGroundedPlayer && m_bJumping)
        {
            m_bJumping = false;
            pengAnim.SetBool("isJumping", false);
        }*/

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        float horizontal = 0;
        float vertical = 0;

        if (m_Player == Player.One)
        {
            horizontal = Input.GetAxis("HorizontalPlayer1") * movePower;
            vertical = Input.GetAxis("VerticalPlayer1") * movePower;
        }
        else if (m_Player == Player.Two)
        {
            horizontal = Input.GetAxis("HorizontalPlayer2") * movePower;
            vertical = Input.GetAxis("VerticalPlayer2") * movePower;
        }

        if ((m_Player == Player.One && (Input.GetButton("HorizontalPlayer1") || Input.GetButton("VerticalPlayer1")))
            || (m_Player == Player.Two && (Input.GetButton("HorizontalPlayer2") || Input.GetButton("VerticalPlayer2"))))
            pengAnim.SetBool("isWalking", true);
        else
            pengAnim.SetBool("isWalking", false);

        Vector3 forceA = new Vector3(camForward.x, 0, camForward.z).normalized * vertical;
        Vector3 forceB = new Vector3(camRight.x, 0, camRight.z).normalized * horizontal;
        m_Rigidbody.AddForce(forceA + forceB);

        Vector3 x = new Vector3();
        transform.LookAt(Vector3.SmoothDamp(transform.position + transform.forward, transform.position + (forceA + forceB), ref x, 0.1f), Vector3.up);


        /*//Jump
        if (m_bJumping)
        {
            pengAnim.SetBool("isJumping", true);
            m_Rigidbody.AddForce(Vector3.up * m_fJumpPower * -3.0f * m_kfGravity);
            m_bJumping = false;
        }*/
    }

    void Update()
    {
        // Polarity swap
        if (    (Input.GetButtonDown("PolarityPlayer1") && m_Player == Player.One)
            ||  (Input.GetButtonDown("PolarityPlayer2") && m_Player == Player.Two))
        {
            Magnetic mag = GetComponent<Magnetic>();
            mag.Swap();
            AudioManager.PlaySFX(AudioManager.SFXClip.Magnet);
        }

        /*//Jump
        if (    (Input.GetButtonDown("JumpPlayer1") && m_bGroundedPlayer && m_Player == Player.One)
            ||  (Input.GetButtonDown("JumpPlayer2") && m_bGroundedPlayer && m_Player == Player.Two))
        {
            pengAnim.SetBool("isJumping", true);
            m_bJumping = true;
        }

        // Running
        if (    (Input.GetButton("RunPlayer1") && m_bGroundedPlayer && m_Player == Player.One)
            ||  (Input.GetButton("RunPlayer2") && m_bGroundedPlayer && m_Player == Player.Two))
        {
            m_bRunning = true;
            pengAnim.SetBool("isRunning", true);
            m_bSliding = false;
        }
        else
        {
            m_bRunning = false;
            pengAnim.SetBool("isRunning", false);
        }

        //Slide
        if (    (Input.GetButtonDown("SlidePlayer1") && m_Player == Player.One)
            ||  (Input.GetButtonDown("SlidePlayer2") && m_Player == Player.Two))
        {
            m_bSliding = true;
            pengAnim.SetBool("isSliding", true);
        }*/

        //Interact
        if (    (Input.GetButtonDown("InteractPlayer1") && m_Player == Player.One)
            ||  (Input.GetButtonDown("InteractPlayer2") && m_Player == Player.Two))
        {
            for (int i = 0; i < m_InteractablesList.Count; ++i)
                m_InteractablesList[i].Interact();
            Debug.Log("Interact");
        }

        //Grab
        if (    (Input.GetButtonDown("GrabPlayer1") && m_Player == Player.One)
            ||  (Input.GetButtonDown("GrabPlayer2") && m_Player == Player.Two))
        {
            for (int i = 0; i < m_GrabbablesList.Count; ++i)
                m_GrabbablesList[i].Grab(gameObject);
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

        Grabbable grabbable = other.gameObject.GetComponent<Grabbable>();
        if (grabbable != null)
        {
            if (m_GrabbablesList.Contains(grabbable) == false)
                m_GrabbablesList.Add(grabbable);
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

        Grabbable grabbable = other.gameObject.GetComponent<Grabbable>();
        if (grabbable != null)
        {
            if (m_GrabbablesList.Contains(grabbable) == true)
                m_GrabbablesList.Remove(grabbable);
        }
    }
}
