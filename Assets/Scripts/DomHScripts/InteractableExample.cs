using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableExample : Interactable
{
    public string ThingToSay;

    private void Start()
    {
        m_fRange = 2.5f; //change the interaction range of this object
    }

    protected override void DoInteraction()
    {
        m_bInteracted = true; //optionally make it only interactable once

        Debug.Log(ThingToSay);
    }
}
