using Assets.Scripts.PickUpItem;
using DiningCombat;
using System.Diagnostics;
using UnityEngine;

internal class StateHoldsObj : IStatePlayerHand
{
    // ================================================
    // constant Variable 
    private const string k_ClassName = "StateHoldsObj";
    private const int k_Next = PickUpItem.k_Powering;
    private const int k_Previous = PickUpItem.k_Free;

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
    public StateHoldsObj(PickUpItem i_PickUpItem)
    {
        m_PickUpItem = i_PickUpItem;
    }

    public void EnterCollisionFoodObj(GameObject i_GameObject)
    {

        // for now this is should be empty
        // the implementing only in StateFree
        //dedugger("EnterCollisionFoodObj", "enter");
    }

    public void ExitCollisionFoodObj()
    {

        // for now this is should be empty
        // the implementing only in StateFree
        //dedugger("ExitCollisionFoodObj", "enter");
    }

    public void InitState()
    {
        dedugger("InitState", "enter");
        m_PickUpItem.ForceMulti = 0;
    }
    public bool IsPassStage()
    {
        return m_PickUpItem.Power.Down;
    }

    public void UpdateByState()
    {
        if (IsPassStage())
        {
            //dedugger("UpdateByState", "enter IsPassStage");
            m_PickUpItem.StatePlayerHand = k_Next;
        }
        //else
        //{
        //    dedugger("UpdateByState", "enter else IsPassStage");
        //}
    }
    // ================================================
    // auxiliary methods

    // ================================================
    // Delegates Invoke 

    // ================================================
    // ----------------Unity--------------------------- 
    // ----------------GameFoodObj---------------------
}
