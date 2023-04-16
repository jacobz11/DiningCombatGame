using DesignPatterns.Abstraction;
using System;
using UnityEngine;

internal class UncollectState : IFoodState
{
    public event Action<AcitonStateMachine> Collect;
    private GameFoodObj gameFood;
    public UncollectState(GameFoodObj gameFood)
    {
        this.gameFood = gameFood;

    }
    public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
    {
    }

    public void OnSteteEnter()
    {

    }

    public void OnSteteExit()
    {
    }

    public bool TryCollect(AcitonStateMachine i_Collcter)
    {
        Collect?.Invoke(i_Collcter);
        return true;
    }

    public bool ThrowingAction()
    {
        return false;
    }
}