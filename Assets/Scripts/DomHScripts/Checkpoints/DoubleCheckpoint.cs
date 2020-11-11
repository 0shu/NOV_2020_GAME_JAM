using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class DoubleCheckpoint : CheckpointBase
{
    bool m_bValidCheckpoint = false;
    List<string> m_PlayersInside = new List<string>();
    const int m_kiNumPlayers = 2;
    [SerializeField]
    Material m_UnsavedCheckpoint;
    [SerializeField]
    Material m_SavedCheckpoint;

    MeshRenderer m_Renderer;

    private void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_Renderer.material = m_UnsavedCheckpoint;
    }

    public override bool ValidCheckpoint
    {
        get => m_bValidCheckpoint;
        protected set
        {
            m_bValidCheckpoint = value;
            if (value == true)
                m_Renderer.material = m_SavedCheckpoint;
        }
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