using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBySwitch : SwitchableVoid
{
    bool m_bAtStartPos = true;
    Vector3 m_StartPosition;
    public Vector3 m_Translation = new Vector3();
    public float m_fMoveSpeed = 0f;

    private void Start()
    {
        Init();
        m_StartPosition = transform.position;
    }

    private void OnDestroy()
    {
        UnInit();
    }

    protected override void DoAction()
    {
        Debug.Log("MoveBySwitch");
        m_bAtStartPos = !m_bAtStartPos;
    }

    private void FixedUpdate()
    {
        if (m_bAtStartPos) //should get to start pos
        {
            transform.position = Vector3.Lerp(transform.position, m_StartPosition, m_fMoveSpeed * Time.deltaTime);
        }
        else //should get to translated pos
        {
            transform.position = Vector3.Lerp(transform.position, m_StartPosition + m_Translation, m_fMoveSpeed * Time.deltaTime);
        }
    }
}
