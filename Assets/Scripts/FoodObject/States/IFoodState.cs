using DesignPatterns.Abstraction;
using System.Collections;
using UnityEngine;

internal interface IFoodState : IDCState    
{
    bool IsThrowingAction();

    bool TryCollect(AcitonStateMachine i_Collcter);
    void Update();
}