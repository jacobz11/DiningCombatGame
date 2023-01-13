using Assets.Scripts.PickUpItem;
using DiningCombat;
using System.Diagnostics;
using UnityEngine;

internal class StateHoldsObj : IStatePlayerHand
{
    private const string k_ClassName = "StateHoldsObj";

    private PickUpItem m_PickUpItem;
    public StateHoldsObj(PickUpItem i_PickUpItem)
    {
        m_PickUpItem = i_PickUpItem;
    }

    public void CollisionFoodObj(GameObject i_GameObject)
    {
        dedugger("CollisionFoodObj", "enter");
    }

    public void EnterCollisionFoodObj(GameObject i_GameObject)
    {
        dedugger("EnterCollisionFoodObj", "enter");
    }

    public void ExitCollisionFoodObj()
    {
        dedugger("ExitCollisionFoodObj", "enter");
    }

    public void InitState()
    {
        dedugger("InitState", "enter");
        //m_PickUpItem.IsItemPicked = true;
        m_PickUpItem.ForceMulti = 0;
    }

    private void dedugger(string func, string i_var)
    {
        GameGlobal.Dedugger(k_ClassName, func, i_var);
    }
    public bool IsPassStage()
    {
        return m_PickUpItem.Power.Down;
    }

    public void UpdateByState()
    {

        if (IsPassStage())
        {
            dedugger("UpdateByState", "enter IsPassStage");
            m_PickUpItem.StatePlayerHand = new StatePowering(m_PickUpItem);
        }
        else
        {
            dedugger("UpdateByState", "enter else IsPassStage");
        }
    }
}
