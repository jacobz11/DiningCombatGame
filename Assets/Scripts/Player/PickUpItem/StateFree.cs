using Assets.Scripts.PickUpItem;
using DiningCombat;
using UnityEngine;

internal class StateFree : IStatePlayerHand
{
    // ================================================
    // constant Variable 
    private const string k_ClassName = "StateFree";
    private const int k_Next = PickUpItem.k_HoldsObj;
    private const int k_Previous = PickUpItem.k_Powering;
    // ================================================
    // Delegate

    // ================================================
    // Fields
    private PickUpItem m_PickUpItem;
    private GameObject m_GameObject;

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
    public StateFree(PickUpItem i_PickUpItem)
    {
        m_PickUpItem = i_PickUpItem;
        m_GameObject = null;
    }

    public void InitState()
    {
        m_PickUpItem.ForceMulti = 0;
    }

    public bool IsPassStage()
    {
        bool haveObj = m_GameObject != null;

        return haveObj && m_PickUpItem.Power.Press;
    }


    public void UpdateByState()
    {
        if (IsPassStage())
        {
            m_PickUpItem.SetGameFoodObj(m_GameObject);
        }
    }

    public void EnterCollisionFoodObj(GameObject i_GameObject)
    {
        m_GameObject = i_GameObject;
    }

    public void ExitCollisionFoodObj()
    {
        m_GameObject = null;
    }
    // ================================================
    // auxiliary methods

    // ================================================
    // Delegates Invoke 

    // ================================================
    // ----------------Unity--------------------------- 
    // ----------------GameFoodObj---------------------
}


//        
//        dedugger("EnterCollisionFoodObj", "enter");
//        dedugger("InitState", "enter");



//private bool isPickUpDistance()
//{
//    return Vector3.Distance(m_GameObject.transform.position, m_PickUpItem.transform.position) <= 2;
//}