using Assets.Scripts.PickUpItem;
using DiningCombat;
using UnityEngine;

/// <summary>
/// This class represents the situation in which
/// The player <b> holding</b> an object of <see cref="GameFoodObj"/>
/// implenrt the interface <see cref="IStatePlayerHand"/>
/// </summary>
internal class StateHoldsObj : IStatePlayerHand
{
    // ================================================
    // constant Variable 
    private const int k_Next = HandPickUp.k_Powering;
    private const int k_Previous = HandPickUp.k_Free;

    // ================================================
    // Delegate

    // ================================================
    // Fields
    private HandPickUp m_PickUpItem;

    // ================================================
    // ----------------Serialize Field-----------------

    // ================================================
    // properties

    // ================================================
    // auxiliary methods programmings

    // ================================================
    // Unity Game Engine

    // ================================================
    //  methods
    public StateHoldsObj(HandPickUp i_PickUpItem)
    {
        m_PickUpItem = i_PickUpItem;
    }

    public void EnterCollisionFoodObj(Collider other)
    {
        // for now this is should be empty
        // the implementing only in StateFree
    }

    public void ExitCollisionFoodObj(Collider other)
    {
        // for now this is should be empty
        // the implementing only in StateFree
    }

    public void InitState()
    {
        m_PickUpItem.ForceMulti = 0;
    }
    public bool IsPassStage()
    {
        return m_PickUpItem.Power.Down;
    }

    public void SetEventTrowing()
    {
        // for now this is should be empty
        // the implementing only in StateFre
    }

    public void SetEventTrowingEnd()
    {
        // for now this is should be empty
        // the implementing only in StateFre
    }

    public void UpdateByState()
    {
        if (IsPassStage())
        {
            m_PickUpItem.StatePlayerHand = k_Next;
        }
    }
    // ================================================
    // auxiliary methods

    // ================================================
    // Delegates Invoke 

    // ================================================
    // ----------------Unity--------------------------- 
    // ----------------GameFoodObj---------------------
}
