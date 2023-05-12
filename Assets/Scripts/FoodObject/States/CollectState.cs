using DesignPatterns.Abstraction;
using DiningCombat;
using System;
using UnityEngine;

internal class CollectState : IFoodState
{
    public const int k_Indx = 1;

    private Rigidbody m_Rigidbody;
    private Transform m_Transform;
    private GameFoodObj m_GameFoodObj;

    public string TagState => GameGlobal.TagNames.k_Picked;

    public bool IsThrowingAction() => true;
    public bool TryCollect(ActionStateMachine i_Collcter) => false;
    public void OnStateEnter() => IRagdoll.DisableRagdoll(m_Rigidbody);

    public CollectState()
    {
        // Not implemented
    }

    public CollectState(Rigidbody rigidbody, Transform transform, GameFoodObj gameFoodObj)
    {
        m_Rigidbody = rigidbody;
        m_Transform = transform;
        m_GameFoodObj = gameFoodObj;
    }

    public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
    {
        switch (i_State)
        {
            case IDCState.eState.ExitingState:
                break;
        }
    }

    public void OnStateExit()
    {
        // Not implemented
    }

    public void Update()
    {
        m_Transform.position = m_GameFoodObj.GetCollectorPosition();
    }

    public void SetThrowDirection(Vector3 i_Direction, float i_PowerAmount)
    {
        Debug.LogWarning("trying to set Throw Direction in CollectState");
    }
}
