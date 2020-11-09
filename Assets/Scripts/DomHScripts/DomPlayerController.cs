using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PlayerRespawn))]
public class DomPlayerController : MonoBehaviour
{
    List<Interactable> m_InteractablesList = new List<Interactable>();
    List<Grabbable> m_GrabbablesList = new List<Grabbable>();

    public enum Player
    {
        One = 1,
        Two = 2
    }

    public Player m_Player;
    public Material northMat;
    public Material southMat;
    public MeshRenderer meshRend;

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

        m_Rigidbody.AddForce(new Vector3(camForward.x, 0, camForward.z).normalized * camForward.magnitude * vertical);
        m_Rigidbody.AddForce(new Vector3(camRight.x, 0, camRight.z).normalized * camRight.magnitude * horizontal);

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
        if (    (Input.GetButtonDown("PolarityPlayer1") && m_Player == Player.One)
            ||  (Input.GetButtonDown("PolarityPlayer2") && m_Player == Player.Two))
        {
            Magnetic mag = GetComponent<Magnetic>();
            mag.Swap();
            if (mag.pole == Polarity.North) meshRend.material = northMat;
            else if (mag.pole == Polarity.South) meshRend.material = southMat;
        }

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

        //Grab
        if (    (Input.GetButtonDown("GrabPlayer1") && m_Player == Player.One)
            ||  (Input.GetButtonDown("GrabPlayer2") && m_Player == Player.Two))
        {
            for (int i = 0; i < m_GrabbablesList.Count; ++i)
                m_GrabbablesList[i].Grab(gameObject);
            Debug.Log("Grab");
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
