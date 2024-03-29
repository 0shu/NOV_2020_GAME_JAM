﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchableExample : SwitchableVoid
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void DoAction()
    {
        Debug.Log("Switch done got switched");
        GetComponent<Rigidbody>().useGravity = true;
    }
}
