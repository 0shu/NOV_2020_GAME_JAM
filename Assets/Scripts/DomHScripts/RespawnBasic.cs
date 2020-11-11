using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBasic : MonoBehaviour
{
    protected Rigidbody m_Rigidbody;
    protected Vector3 m_StartPosition;
    protected bool m_bRespawning = false;

    protected void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_StartPosition = transform.position;
    }

    protected void Update()
    {
        if (transform.position.y < -10)
            Respawn();
    }

    protected void LateUpdate()
    {
        if (m_bRespawning)
            m_Rigidbody.velocity /= 10;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Kill")
        {
            m_bRespawning = true;
            IEnumerator coroutine = DelayedRespawn();
            StartCoroutine(coroutine);

            //play splash
            //GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }

    protected void Respawn()
    {
        transform.position = m_StartPosition;
        m_Rigidbody.velocity = Vector3.zero;
        m_bRespawning = false;
    }

    protected IEnumerator DelayedRespawn()
    {
        yield return new WaitForSecondsRealtime(2f);
        Respawn();
    }
}
