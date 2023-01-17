using Assets.Scripts.PickUpItem;
using DiningCombat;
using UnityEngine;

/// <summary>
/// This class represents the situation in which
/// The player <b>not holding</b> an object of <see cref="GameFoodObj"/>
/// and the player is looking for <see cref="GameFoodObj"/> to pick up 
/// <para>THIS IS INIT Stat </para>
/// implenrt the interface <see cref="IStatePlayerHand"/>
/// </summary>
internal class StateFree : IStatePlayerHand
{
    // ================================================
    // constant Variable 
    private const string k_ClassName = "StateFree";
    private const int k_Next = HandPickUp.k_HoldsObj;
    private const int k_Previous = HandPickUp.k_Powering;
    // ================================================
    // Delegate

    // ================================================
    // Fields
    private HandPickUp m_PickUpItem;
    private GameObject m_GameObject;

    // ================================================
    // ----------------Serialize Field-----------------

    // ================================================
    // properties
    private bool haveGameObject
    {
        get => m_GameObject != null; 
    }
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
    public StateFree(HandPickUp i_PickUpItem)
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
        return haveGameObject && m_PickUpItem.Power.Press;
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