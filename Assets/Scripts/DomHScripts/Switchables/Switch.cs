using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SwitchEventVoid : UnityEvent { }
public class SwitchEventBool : UnityEvent<bool> { }

public class Switch : Interactable
{
    private bool m_bSwitchStatus = false;
    
    private SwitchEventVoid m_SwitchEventVoid;
    private SwitchEventBool m_SwitchEventBool;

    void Awake()
    {
        if (m_SwitchEventVoid == null) m_SwitchEventVoid = new SwitchEventVoid();
        if (m_SwitchEventBool == null) m_SwitchEventBool = new SwitchEventBool();
    }

    protected override void DoInteraction()
    {
        m_bSwitchStatus = !m_bSwitchStatus;

        m_SwitchEventVoid?.Invoke();
        m_SwitchEventBool?.Invoke(m_bSwitchStatus);
    }

    public void AddBehaviour(UnityAction function) // from a switchable object, pass in a void function you want 
    {                                              //to have execute when the switch is toggled
        m_SwitchEventVoid.AddListener(function);
    }

    public void AddBehaviour(UnityAction<bool> function) // from a switchable object, pass in a void function you want 
    {                                                    //to have execute (that takes a bool) when the switch is toggled
        m_SwitchEventBool.AddListener(function);
    }

    public void RemoveBehaviour(UnityAction function)
    {
        m_SwitchEventVoid.RemoveListener(function);
    }

    public void RemoveBehaviour(UnityAction<bool> function)
    {
        m_SwitchEventBool.RemoveListener(function);
    }

    private void OnDestroy()
    {
        m_SwitchEventVoid?.RemoveAllListeners();
        m_SwitchEventBool?.RemoveAllListeners();
    }
}
