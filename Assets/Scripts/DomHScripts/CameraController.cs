using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //do the thing
    [SerializeField]
    GameObject m_Player1;
    [SerializeField]
    GameObject m_Player2;

    const int m_kiMaxDistFromEdge = 20;


    public Vector3 offset = new Vector3(0, 1, 0);
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        DistanceFromSide(m_Player1);
    }

    int DistanceFromSide(GameObject obj)
    {
        Vector3 screenPos = cam.worldToCameraMatrix.MultiplyVector(obj.transform.position);
        //Debug.Log($"Distance from side: {screenPos.magnitude}");


        return 0;
    }
}
