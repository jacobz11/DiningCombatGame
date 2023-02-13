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
        m_PickUpItem.SetGameFoodObj(null);
        m_GameObject = null;
        
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

    public void EnterCollisionFoodObj(Collider other)
    {
        if (other.gameObject.CompareTag(GameGlobal.TagNames.k_FoodObj))
        {
            m_GameObject = other.gameObject;
        }
    }

    public void ExitCollisionFoodObj(Collider other)
    {
        if (other.gameObject.CompareTag(GameGlobal.TagNames.k_FoodObj))
        {
            m_GameObject = null;
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
