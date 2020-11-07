using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Replace with base class for interactable objects
    [SerializeField]
    private GameObject link;//!<Object linked to the door, which controls its state
    private Vector3 closePos;//!<Object's position when closed
    private Vector3 openPos;//!<Objects's position when open

    private float moveTime;//!<Object's time spent moving
    private bool lastState;//!<True if object was opening last frame

    // Start is called before the first frame update
    void Start()
    {
        closePos = gameObject.transform.position;//Close pos is current pos (default)
        openPos = gameObject.transform.position - new Vector3(0, 5, 0);//Open pos is 5 units down (don't know how these numebrs shape up)
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*PSEUDOCODE
     If(link is active)
     { 
        if(!lastState){
            if(moveTime < 1) moveTime = 1 - moveTime;
            else moveTime = 0;
        }
        moveTime += time.DeltaTime;
        Vector3.Lerp(closePos, openPos, moveTime);
     }
     else
     { 
        if(lastState){
            if(moveTime < 1) moveTime = 1 - moveTime;
            else moveTime = 0;
        }
        moveTime += Time.deltaTime;
        Vector3.Lerp(openPos, closePos, moveTime);
     }
     */
}
