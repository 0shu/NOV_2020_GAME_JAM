using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchableExample : SwitchableVoid
{
    private void Start()
    {
        Init(); //Always put this in Start() - takes care of binding the DoAction function
    }

    private void OnDestroy()
    {
        UnInit(); //Tidy up
    }

    protected override void DoAction()
    {
        Debug.Log("Switch done got switched");
        GetComponent<Rigidbody>().useGravity = true;
    }
}
