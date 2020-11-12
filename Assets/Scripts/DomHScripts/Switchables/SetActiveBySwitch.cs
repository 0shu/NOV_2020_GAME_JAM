using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveBySwitch : SwitchableVoid
{
    protected override void DoAction()
    {
        for (int i = 0; i < transform.childCount; ++i)
            transform.GetChild(i).gameObject.SetActive(true);
    }
}
