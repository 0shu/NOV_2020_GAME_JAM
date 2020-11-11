using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CheckpointBase : MonoBehaviour
{
    [SerializeField]
    private int m_iCheckpointNumber;

    public int CheckpointNumber { get => m_iCheckpointNumber; }
    public abstract bool ValidCheckpoint { get; protected set; }

    //prevent 2 players respawning at the same time in the same place
    [SerializeField]
    private Vector3 m_SpawnPositionA;
    [SerializeField]
    private Vector3 m_SpawnPositionB;

    bool spawnA = false;

    public Vector3 SpawnPosition
    {
        get
        {
            spawnA = !spawnA;

            if (spawnA == true)
                return transform.position + m_SpawnPositionA;
            else
                return transform.position + m_SpawnPositionB;
        }
    }
}