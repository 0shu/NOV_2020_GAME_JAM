using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool down { get { return down; } set { down = value; } }//!<Is the switch down?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter3D(Collision collision)
    {
        down = true;
    }

    void OnCollisionExit3D(Collision collision)
    {
        down = false;
    }

}
