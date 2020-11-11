using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    protected float m_fRange = 1.5f;
    protected bool m_bInRange = false;
    protected bool m_bIsGrabbed = false;
    public virtual bool IsGrabbed
    {
        get => m_bIsGrabbed;
        protected set { m_bIsGrabbed = value; }
    }
    GameObject m_GrabbingPlayer;
    Rigidbody m_Rigidbody;
    Collider m_Collider;

    protected virtual void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
    }

    public virtual void Grab(GameObject player) // Player script calls this
    {
        if (m_bInRange)
        {
            IsGrabbed = !IsGrabbed;
            Debug.Log($"Grab: {IsGrabbed}");
            m_Rigidbody.isKinematic = IsGrabbed;
            //m_Collider.isTrigger = IsGrabbed;

            if (IsGrabbed == true)
            {
                m_GrabbingPlayer = player;
            }
        }
    }

    private void Update()
    {
        if (IsGrabbed == true && m_GrabbingPlayer != null)
        {
            float size = transform.localScale.magnitude;
            Vector3 pos = m_GrabbingPlayer.transform.forward * size * 0.8f;
            pos.y += size;
            transform.position = m_GrabbingPlayer.transform.position + pos;
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