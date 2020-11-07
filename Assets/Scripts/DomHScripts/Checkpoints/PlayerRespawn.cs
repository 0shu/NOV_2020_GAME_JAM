using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    CharacterController m_CharacterController;
    Rigidbody m_Rigidbody;
    Checkpoint m_LastCheckpoint;
    Vector3 m_StartPosition;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_StartPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "KillPlane")
            Respawn();

        Checkpoint checkpoint = other.GetComponent<Checkpoint>();

        if (checkpoint != null)
        {
            if (m_LastCheckpoint == null)
                m_LastCheckpoint = checkpoint;
            else if (checkpoint.CheckpointNumber > m_LastCheckpoint.CheckpointNumber)
                m_LastCheckpoint = checkpoint;
        }
    }

    private void Update()
    {
        if (transform.position.y < -10)
            Respawn();
    }

    private void Respawn()
    {
        CharacterController x = GetComponent<CharacterController>();
        x.enabled = false;
        if (m_LastCheckpoint != null)
            transform.position = m_LastCheckpoint.SpawnPosition;
        else
            transform.position = m_StartPosition;

        m_Rigidbody.velocity = Vector3.zero;

        Debug.Log($"Respawn() : {transform.position}");
        x.enabled = true;
    }
}
