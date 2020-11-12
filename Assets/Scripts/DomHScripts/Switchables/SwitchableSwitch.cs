using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Switch))]
public class SwitchableSwitch : SwitchableVoid
{
    Switch m_Switch;

    protected override void Start()
    {
        m_Switch = GetComponent<Switch>();
        base.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void DoAction()
    {
        m_Switch.Interact();
    }
}
