using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    protected float m_fRange = 1.5f;
    protected bool m_bInRange = false;
    protected bool m_bInteracted = false;

    public void Interact() // Player calls this
    {
        if (m_bInRange && m_bInteracted == false)
        {
            DoInteraction();
        }
    }

    protected abstract void DoInteraction(); // Subclasses implement this which gets called in Interact()

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