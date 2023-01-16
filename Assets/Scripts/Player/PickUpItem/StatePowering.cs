using Assets.Scripts.PickUpItem;
using DiningCombat;
using UnityEngine;

internal class StatePowering : IStatePlayerHand
{
    // ================================================
    // constant Variable 
    private const string k_ClassName = "StatePowering";
    private const int k_Previous = PickUpItem.k_HoldsObj;
    // ================================================
    // Delegate

    // ================================================
    // Fields
    private PickUpItem m_PickUpItem;

    // ================================================
    // ----------------Serialize Field-----------------

    // ================================================
    // properties

    // ================================================
    // auxiliary methods programmings
    private void dedugger(string func, string i_var)
    {
        GameGlobal.Dedugger(k_ClassName, func, i_var);
    }
    // ================================================
    // Unity Game Engine

    // ================================================
    //  methods
    public StatePowering(PickUpItem i_PickUpItem)
    {
        m_PickUpItem = i_PickUpItem;
    }
    public void InitState()
    {
    }

    public void EnterCollisionFoodObj(GameObject i_GameObject)
    {
        // for now this is should be empty
        // the implementing only in StateFree
    }

    public void ExitCollisionFoodObj()
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
            m_PickUpItem.ThrowObj();
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
            m_PickUpItem.StatePlayerHand = k_Previous;
        }
    }

    // ================================================
    // Delegates Invoke 

    // ================================================
    // ----------------Unity--------------------------- 
    // ----------------GameFoodObj---------------------
}
