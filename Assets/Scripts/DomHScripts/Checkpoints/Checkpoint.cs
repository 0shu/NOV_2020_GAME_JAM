using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private int m_iCheckpointNumber;

    public int CheckpointNumber { get => m_iCheckpointNumber; }

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
