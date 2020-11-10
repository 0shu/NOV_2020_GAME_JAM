using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    CharacterController m_CharacterController;
    Rigidbody m_Rigidbody;
    CheckpointBase m_LastCheckpoint;
    Vector3 m_StartPosition;
    bool m_bRespawning = false;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_StartPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Kill")
        {
            m_bRespawning = true;
            IEnumerator coroutine = DelayedRespawn();
            StartCoroutine(coroutine);

            //play splash
        }
    }

    private void OnTriggerStay(Collider other)
    {
        CheckpointBase checkpoint = other.GetComponent<CheckpointBase>();

        if (checkpoint != null)
        {
            if (checkpoint.ValidCheckpoint == true)
            {
                if (m_LastCheckpoint == null)
                    m_LastCheckpoint = checkpoint;
                else if (checkpoint.CheckpointNumber > m_LastCheckpoint.CheckpointNumber)
                    m_LastCheckpoint = checkpoint;
            }
        }
    }

    private void Update()
    {
        if (transform.position.y < -10)
            Respawn();
    }

    private void LateUpdate()
    {
        if (m_bRespawning)
            m_Rigidbody.velocity /= 10;
    }

    private void Respawn()
    {
        if (m_LastCheckpoint != null)
            transform.position = m_LastCheckpoint.SpawnPosition;
        else
            transform.position = m_StartPosition;

        m_Rigidbody.velocity = Vector3.zero;

        Debug.Log($"Respawn() : {transform.position}");
        m_bRespawning = false;
    }

    private IEnumerator DelayedRespawn()
    {
        yield return new WaitForSecondsRealtime(2f);
        Respawn();
    }
}
