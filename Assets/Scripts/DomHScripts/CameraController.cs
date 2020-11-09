using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject m_Player1;
    [SerializeField]
    GameObject m_Player2;

    Camera m_Camera;
    const int m_kiMaxDistFromEdge = 50;
    const float m_kfMaxScale = 4.5f;
    float m_fStartingOrthoSize;
    float m_fStartingDistance;
    float m_fScale = 1f;
    float m_fSmoothTime = 0.1f;


    void Start()
    {
        m_Camera = GetComponent<Camera>();
        Vector3 middlePos = (m_Player1.transform.position + m_Player2.transform.position) / 2;
        m_fStartingDistance = (middlePos - transform.position).magnitude;

        if (m_Camera.orthographic)
            m_fStartingOrthoSize = m_Camera.orthographicSize;
    }

    void FixedUpdate()
    {
        Vector3 middlePos = (m_Player1.transform.position + m_Player2.transform.position) / 2;

        Vector3 screenPos1 = m_Camera.WorldToScreenPoint(m_Player1.transform.position); //player 1 screen pos
        Vector3 screenPos2 = m_Camera.WorldToScreenPoint(m_Player2.transform.position); //player 2 screen pos

        //if the players are at the boundaries
        if (    (screenPos1.x < m_kiMaxDistFromEdge || Screen.width - screenPos2.x < m_kiMaxDistFromEdge)
            ||  (screenPos2.x < m_kiMaxDistFromEdge || Screen.width - screenPos1.x < m_kiMaxDistFromEdge)
            ||  (screenPos1.y < m_kiMaxDistFromEdge || Screen.width - screenPos2.y < m_kiMaxDistFromEdge)
            ||  (screenPos2.y < m_kiMaxDistFromEdge || Screen.width - screenPos1.y < m_kiMaxDistFromEdge))
        {
            m_fScale += 0.01f;
        }
        else if (   (screenPos1.x > 2 * m_kiMaxDistFromEdge && Screen.width - screenPos2.x > 2 * m_kiMaxDistFromEdge)
            &&      (screenPos2.x > 2 * m_kiMaxDistFromEdge && Screen.width - screenPos1.x > 2 * m_kiMaxDistFromEdge)
            &&      (screenPos1.y > 2 * m_kiMaxDistFromEdge && Screen.width - screenPos2.y > 2 * m_kiMaxDistFromEdge)
            &&      (screenPos2.y > 2 * m_kiMaxDistFromEdge && Screen.width - screenPos1.y > 2 * m_kiMaxDistFromEdge))
        {
            m_fScale -= 0.01f;
        }

        m_fScale = Mathf.Clamp(m_fScale, 1f, m_kfMaxScale);

        transform.position = Vector3.Lerp(transform.position, middlePos - transform.forward * m_fStartingDistance * m_fScale, m_fSmoothTime);

        if (m_Camera.orthographic)
            m_Camera.orthographicSize = Mathf.Lerp(m_Camera.orthographicSize, m_fStartingOrthoSize * m_fScale, m_fSmoothTime);
    }
}