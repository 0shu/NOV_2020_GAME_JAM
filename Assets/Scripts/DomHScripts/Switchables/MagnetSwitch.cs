using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Magnetic))]
public class MagnetSwitch : Switch // A bit bad, inherits interactable but shouldn't really, but we'll just override those bits - should've used interfaces
{
    bool m_bSwitched = false;

    protected override void DoInteraction() { }

    private void OnTriggerEnter(Collider other)
    {
        if (m_bSwitched == false)
        {
            MagnetSwitcherObject magnetSwitcher = other.GetComponent<MagnetSwitcherObject>();

            if (magnetSwitcher != null)
            {
                base.DoInteraction();
                RespawnBasic respawnComponent = other.GetComponent<RespawnBasic>();
                if (respawnComponent != null)
                    respawnComponent.enabled = false;

                m_bSwitched = true;
            }
        }
    }
}
