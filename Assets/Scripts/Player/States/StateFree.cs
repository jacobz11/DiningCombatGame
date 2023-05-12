using DesignPatterns.Abstraction;
using System;
using UnityEngine;

internal class StateFree : IStatePlayerHand
{
    public class CollectedFoodEvent : EventArgs
    {
        public GameFoodObj m_GameFood;
    }

    public const int k_Indx = 0;
    public event Action<CollectedFoodEvent> PlayerCollectedFood;

    protected ActionStateMachine m_AcitonStateMachine;
    private GameFoodObj m_FoodObj;

    protected bool HaveGameObject => this.m_FoodObj != null;
    public override string ToString() => "StateFree : ";

    bool IStatePlayerHand.OnChargingAction { get => false; set { } }

    public StateFree(ActionStateMachine i_AcitonStateMachine)
    {
        m_FoodObj = null;
        m_AcitonStateMachine = i_AcitonStateMachine;
    }

    public void EnterCollisionFoodObj(Collider other)
    {
        if (other.transform.TryGetComponent<GameFoodObj>(out GameFoodObj o_FoodObj))
        {
            if (o_FoodObj.CanCollect())
            {
                this.m_FoodObj = o_FoodObj;
            }
        }
    }
    public void ExitCollisionFoodObj(Collider other)
    {
        if (m_FoodObj is not null && m_FoodObj.gameObject.Equals(other))
        {
            this.m_FoodObj = null;
        }
    }

    public virtual void OnStateEnter()
    {
        this.m_FoodObj = null;
        Debug.Log("init state : StateFree");
    }

    public virtual void OnStateExit()
    {
        PlayerCollectedFood?.Invoke(new CollectedFoodEvent()
        {
            m_GameFood = m_FoodObj
        });
    }

    public bool OnPickUpAction(out GameFoodObj o_Collcted)
    {
        bool canCollect = HaveGameObject
            && m_FoodObj.CurrentState.TryCollect(m_AcitonStateMachine);
        o_Collcted = canCollect ? m_FoodObj : null;

        return canCollect;
    }
    #region Not-Implemented
    public virtual void Update()
    {/* Not-Implemented */}
    public void OnChargingAction()
    {/* Not-Implemented */}
    #endregion
    public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
    {
        switch (i_State)
        {
            case IDCState.eState.ExitingState:
                PlayerCollectedFood += i_Action;
                break;
        }
    }
    public bool OnThrowPoint(out float o_Force)
    {
        o_Force = 0;
        return false;
    }
}
