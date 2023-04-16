using DesignPatterns.Abstraction;
using UnityEngine;

internal interface IFoodState : IDCState    
{
    bool ThrowingAction();
    bool TryCollect(AcitonStateMachine i_Collcter);
}