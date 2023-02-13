using Assets.Scripts.PickUpItem;
using UnityEngine;

/// <summary>
/// This class represents the situation in which
/// The player <b>holding</b> an object of <see cref="GameFoodObj"/>
/// and the player is Powering 
/// this class will:
/// <para>OR Throw Obj  <see cref="HandPickUp.ThrowObj"/> </para>
/// <para>OR add add To Force Multi  <see cref="addToForceMulti"/></para>
/// <para>OR go back to <see cref="StateHoldsObj"/></para>
/// implenrt the interface <see cref="IStatePlayerHand"/>
/// </summary>
internal class StatePowering : IStatePlayerHand
{
    // ================================================
    // constant Variable 
    //private const byte k_Previous = HandPickUp.k_HoldsObj;
    //private const byte k_Next = HandPickUp.k_Throwing;
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
    public StatePowering(HandPickUp i_PickUpItem)
    {
        m_PickUpItem = i_PickUpItem;
    }
    public void InitState()
    {
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

    public bool IsPassStage()
    {
        return m_PickUpItem.Power.Up && m_PickUpItem.ForceMulti > 10;
    }

    public void UpdateByState()
    {

        if (m_PickUpItem.Power.Press)
        {
            addToForceMulti();
        }
        else if (IsPassStage())
        {
            m_PickUpItem.StatePlayerHand++;// = k_Next;
        }
        else
        {
            stepBack();
        }
    }
    // ================================================
    // auxiliary methods
    private void addToForceMulti()
    {
        m_PickUpItem.ForceMulti += 1400 * Time.deltaTime;
    }
    private void stepBack()
    {
        if (Time.deltaTime > 0.2)
        {
            m_PickUpItem.StatePlayerHand--;// = k_Previous;
        }
    }

    // ================================================
    // Delegates Invoke 

    // ================================================
    // ----------------Unity--------------------------- 
    // ----------------GameFoodObj---------------------
}
