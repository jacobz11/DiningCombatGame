using DesignPatterns.Abstraction;
using System;
using Unity.VisualScripting;
using UnityEngine;

internal class CollectState : IFoodState, IAnimationDisturbing
{
    private Rigidbody m_Rigidbody;

    public CollectState()
    {
    }

    public CollectState(Rigidbody rigidbody)
    {
        m_Rigidbody = rigidbody;
    }

    public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
    {
        switch (i_State)
        {
            case IDCState.eState.ExitingState:
                break;
        }
    }

    public void DisableRagdoll()
    {
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.detectCollisions = false;
    }

    public void EnableRagdoll()
    {
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.detectCollisions = true;
    }

    public void OnSteteEnter()
    {
        DisableRagdoll();
    }

    public void OnSteteExit()
    {
        //EnableRagdoll();
    }

    public bool ThrowingAction()
    {
        return false;
    }

    public bool TryCollect(AcitonStateMachine i_Collcter)
    {
        return false;
    }
}