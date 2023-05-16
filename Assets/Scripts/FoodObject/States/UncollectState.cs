using DesignPatterns.Abstraction;
using DiningCombat;
using System;
using UnityEngine;
// TODO: arrange the code
// TODO : Add a namespace
internal class UncollectState : IFoodState
{
    public const int k_Indx = 0;
    public event Action<ActionStateMachine> Collect;
    // TODO: check why it exists
    private readonly GameFoodObj r_GameFood;

    public string TagState => GameGlobal.TagNames.k_FoodObj;
    public bool IsThrowingAction() => false;

    public UncollectState(GameFoodObj gameFood)
    {
        this.r_GameFood = gameFood;
    }

    #region Not Implemented
    public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
    {
        // Not Implemented
    }

    public virtual void OnStateEnter()
    {
        // Not Implemented
    }

    public virtual void OnStateExit()
    {
        // Not Implemented
    }

    public virtual void Update()
    {
        // Not Implemented
    }
    #endregion

    public virtual bool TryCollect(ActionStateMachine i_Collcter)
    {
        Collect?.Invoke(i_Collcter);
        return true;
    }

    public void SetThrowDirection(Vector3 i_Direction, float i_PowerAmount)
    {
        Debug.LogWarning("trying to set Throw Direction in CollectState");
    }
}
