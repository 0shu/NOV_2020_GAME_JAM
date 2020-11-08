using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switchable : MonoBehaviour
{
    [SerializeField]
    protected Switch m_ControllingSwitch;

    protected abstract void BindSwitch();
    protected abstract void UnbindSwitch();

    private void Start()
    {
        BindSwitch();
    }

    private void OnDestroy()
    {
        UnbindSwitch();
    }
}

public abstract class SwitchableVoid : Switchable
{
    protected override void BindSwitch()
    {
        m_ControllingSwitch.AddBehaviour(DoAction);
    }

    protected override void UnbindSwitch()
    {
        m_ControllingSwitch.RemoveBehaviour(DoAction);
    }

    protected abstract void DoAction();
}

public abstract class SwitchableBool : Switchable
{
    protected override void BindSwitch()
    {
        m_ControllingSwitch.AddBehaviour(DoAction);
    }

    protected override void UnbindSwitch()
    {
        m_ControllingSwitch.RemoveBehaviour(DoAction);
    }

    protected abstract void DoAction(bool switchStatus);
}