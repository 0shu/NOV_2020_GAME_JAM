using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    protected float m_fRange = 1.5f;
    protected bool m_bInRange = false;
    bool m_bIsGrabbed = false;
    GameObject m_GrabbingPlayer;
    Rigidbody m_Rigidbody;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void Grab(GameObject player) // Player script calls this
    {
        if (m_bInRange)
        {
            m_bIsGrabbed = !m_bIsGrabbed;
            m_Rigidbody.isKinematic = m_bIsGrabbed;
            m_Rigidbody.detectCollisions = !m_bIsGrabbed;

            if (m_bIsGrabbed == true)
                m_GrabbingPlayer = player;
        }
    }

    private void Update()
    {
        if (m_bIsGrabbed == true && m_GrabbingPlayer != null)
        {
            float size = transform.localScale.magnitude;
            Vector3 pos = m_GrabbingPlayer.transform.forward * size * 0.8f;
            pos.y += size;
            transform.position = m_GrabbingPlayer.transform.position + pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Contains("Player"))
        {
            if ((other.transform.position - transform.position).sqrMagnitude < m_fRange * m_fRange)
            {
                m_bInRange = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name.Contains("Player"))
        {
            if ((other.transform.position - transform.position).sqrMagnitude < m_fRange * m_fRange)
            {
                m_bInRange = true;
            }
            else
            {
                m_bInRange = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name.Contains("Player"))
        {
            m_bInRange = false;
        }
    }
}
