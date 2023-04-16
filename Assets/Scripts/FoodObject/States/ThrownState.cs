using DesignPatterns.Abstraction;
using System;
using Unity.VisualScripting;
using UnityEngine;

internal class ThrownState : IFoodState, IAnimationDisturbing
{
    public event Action<HitPointEventArgs> OnHit;
    private Rigidbody m_Rigidbody;
    public class HitPointEventArgs : EventArgs
    {
        public float Damage;
        public GameObject PlayerHit;
        public GameObject PlayerTrown;
    }

    public ThrownState (Rigidbody i_Rigidbody)
    {
        m_Rigidbody = i_Rigidbody;
    }
    public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
    {
        switch (i_State)
        {
            case IDCState.eState.ExitingState:
                OnHit += i_Action;
                break;
        }
    }

    public void OnSteteEnter()
    {
    }

    public void OnSteteExit()
    {
    }

    public bool TryCollect(AcitonStateMachine i_Collcter)
    {
        return false;
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

    public bool ThrowingAction()
    {
        return true;
    }
}
