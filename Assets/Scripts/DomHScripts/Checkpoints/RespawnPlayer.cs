using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : RespawnBasic
{
    CheckpointBase m_LastCheckpoint;

    private void OnTriggerStay(Collider other)
    {
        CheckpointBase checkpoint = other.GetComponent<CheckpointBase>();

        if (checkpoint != null)
        {
            if (checkpoint.ValidCheckpoint == true)
            {
                if (m_LastCheckpoint == null)
                {
                    m_LastCheckpoint = checkpoint;
                    AudioManager.PlaySFX(AudioManager.SFXClip.Checkpoint);
                }
                else if (checkpoint.CheckpointNumber > m_LastCheckpoint.CheckpointNumber)
                {
                    m_LastCheckpoint = checkpoint;
                    AudioManager.PlaySFX(AudioManager.SFXClip.Checkpoint);
                }
            }
        }
    }

    protected override void Respawn()
    {
        base.Respawn();

        if (m_LastCheckpoint != null)
            transform.position = m_LastCheckpoint.SpawnPosition;
    }
}
