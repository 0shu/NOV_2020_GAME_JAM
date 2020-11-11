using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Magnetic))]
public class GrabbableMagnetic : Grabbable
{
    Magnetic m_Magnetic;
    Polarity m_Pole;

    protected override void Start()
    {
        base.Start();
        m_Magnetic = GetComponent<Magnetic>();
        m_Pole = m_Magnetic.pole;
    }

    /*public override void Grab(GameObject player)
    {
        base.Grab(player);

        if (IsGrabbed == true)
            m_Magnetic.SetNeutral();
        else if (m_Pole == Polarity.North)
            m_Magnetic.SetNorth();
        else if (m_Pole == Polarity.South)
            m_Magnetic.SetSouth();
    }*/

    public override bool IsGrabbed
    {
        get => m_bIsGrabbed;
        protected set
        {
            m_bIsGrabbed = value;
            if (value == true)
                m_Magnetic.SetNeutral();
            else if (m_Pole == Polarity.North)
                m_Magnetic.SetNorth();
            else if (m_Pole == Polarity.South)
                m_Magnetic.SetSouth();
        }
    }

}