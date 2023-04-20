using DesignPatterns.Abstraction;
using System;
using Unity.VisualScripting;
using UnityEngine;

internal class StateFree : IStatePlayerHand
{
    public const int k_Indx = 0;
    private AcitonStateMachine m_AcitonStateMachine;
    private GameFoodObj m_FoodObj;

    public event Action<CollectedFoodEvent> PlayerCollectedFood;

    public class CollectedFoodEvent : EventArgs
    {
        public GameFoodObj gameFood;
    }

    protected bool HaveGameObject => this.m_FoodObj != null;

    bool IStatePlayerHand.OnChargingAction { get => false; set { } }

    public StateFree(AcitonStateMachine i_AcitonStateMachine)
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

    public override string ToString()
    {
        return "StateFree : ";
    }

    public void OnSteteEnter()
    {
        this.m_FoodObj = null;
        Debug.Log("init state : StateFree");
    }


    public void OnSteteExit()
    {
        PlayerCollectedFood?.Invoke(new CollectedFoodEvent()
        {
            gameFood = m_FoodObj
        });
    }

    public void Update()
    {
    }
    public bool OnPickUpAction(out GameFoodObj o_Collcted)
    {
        bool canCollect = HaveGameObject
            && m_FoodObj.CurrentState.TryCollect(m_AcitonStateMachine);
        o_Collcted = canCollect ? m_FoodObj : null;

        return canCollect;
    }

    public void OnChargingAction()
    {
    }

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
