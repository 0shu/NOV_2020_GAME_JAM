using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCheckpoint : CheckpointBase
{
    bool m_bValidCheckpoint = false;
    List<string> m_PlayersInside = new List<string>();
    const int m_kiNumPlayers = 2;

    public override bool ValidCheckpoint
    {
        get => m_bValidCheckpoint;
        protected set { m_bValidCheckpoint = value; }
    }

    private void OnTriggerStay(Collider other)
    {
        if (ValidCheckpoint == false)
        {
            if (other.tag == "Player")
            {
                if (m_PlayersInside.Contains(other.name) == false)
                {
                    m_PlayersInside.Add(other.name);
                }
            }

            if (m_PlayersInside.Count == m_kiNumPlayers)
                ValidCheckpoint = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ValidCheckpoint == false)
        {
            if (other.tag == "Player")
            {
                    m_PlayersInside.RemoveAll(names => names == other.name);
            }
        }
    }
}