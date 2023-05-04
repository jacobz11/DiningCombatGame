using DesignPatterns.Abstraction;
using DiningCombat;
using System;
using UnityEngine;

internal class CollectState : IFoodState, IRagdoll
{
    public const int k_Indx = 1;

    private Rigidbody m_Rigidbody;
    private Transform m_Transform;
    private GameFoodObj m_GameFoodObj;

    public string TagState => GameGlobal.TagNames.k_Picked;

    public bool IsThrowingAction() => true;
    public bool TryCollect(AcitonStateMachine i_Collcter) => false;
    public void OnSteteEnter() => DisableRagdoll();
    public CollectState()
    { /* Not-Implemented */}

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
    #region Ragdoll
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
    #endregion

    public void OnSteteExit()
    { /* Not-Implemented */}

    public void Update()
    {
        m_Transform.position = m_GameFoodObj.GetCollctorPositin();
    }

    public void SetThrowDirection(Vector3 i_Direction, float i_PowerAmount)
    {
        Debug.LogWarning("trying to set Throw Direction in CollectState");
    }
}