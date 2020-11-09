using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public float horizontalScrollSpeed = 0.25f;
    //public float verticalScrollSpeed = 0.25f;
    public float planeMinHeight = 0.3f;
    public float planeMaxHeight = 0.7f;

    private MeshRenderer m_Renderer;

    private void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        float sin = Mathf.Sin(Time.time / 5);
        float height = Remap(sin, -1f, 1f, 0f, 1f);
        float planeHeight = Remap(height, 0f, 1f, planeMinHeight, planeMaxHeight);
        transform.position = new Vector3(transform.position.x, planeHeight, transform.position.z);

        if (m_Renderer != null)
        {
            //float verticalOffset = Time.time * verticalScrollSpeed;
            float horizontalOffset = Time.time * horizontalScrollSpeed;
            m_Renderer.material.SetTextureOffset(Shader.PropertyToID("_MainTex"), new Vector2(horizontalOffset, 0/*verticalOffset*/));
            m_Renderer.material.SetFloat(Shader.PropertyToID("_Parallax"), -height);
            
        }
    }

    float Remap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
