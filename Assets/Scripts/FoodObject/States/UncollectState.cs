using DesignPatterns.Abstraction;
using System;
using UnityEngine;

internal class UncollectState : IFoodState
{
    public const int k_Indx = 0;
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

    public bool IsThrowingAction()
    {
        return false;
    }

    public void Update()
    {}

    public void SetThrowDirection(Vector3 i_Direction, float i_PowerAmount)
    {
        Debug.LogWarning("trying to set Throw Direction in CollectState");
    }
}