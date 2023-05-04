using DesignPatterns.Abstraction;
using DiningCombat;
using System;
using UnityEngine;

internal class UncollectState : IFoodState
{
    public const int k_Indx = 0;
    public event Action<AcitonStateMachine> Collect;
    private GameFoodObj gameFood;

    public string TagState => GameGlobal.TagNames.k_FoodObj;
    public bool IsThrowingAction()=> false;
    
    public UncollectState(GameFoodObj gameFood)
    {
        this.gameFood = gameFood;
    }
    #region  Not-Implemented
    public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
    { /* Not-Implemented */}

    public virtual void OnSteteEnter()
    { /* Not-Implemented */}

    public virtual void OnSteteExit()
    { /* Not-Implemented */}
    public virtual void Update()
    { /* Not-Implemented */}
    #endregion
    public virtual bool TryCollect(AcitonStateMachine i_Collcter)
    {
        Collect?.Invoke(i_Collcter);
        return true;
    }

    public void SetThrowDirection(Vector3 i_Direction, float i_PowerAmount)
    {
        Debug.LogWarning("trying to set Throw Direction in CollectState");
    }
}