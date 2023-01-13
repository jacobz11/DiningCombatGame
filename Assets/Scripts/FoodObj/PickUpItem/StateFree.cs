using Assets.Scripts.PickUpItem;
using DiningCombat;
using UnityEngine;

internal class StateFree : IStatePlayerHand
{
    private const string k_ClassName = "StateFree";

    private PickUpItem m_PickUpItem;
    private GameObject m_GameObject;

    public StateFree(PickUpItem i_PickUpItem)
    {
        m_PickUpItem = i_PickUpItem;
        m_GameObject = null;
    }
    //        
    //        dedugger("EnterCollisionFoodObj", "enter");
    //        dedugger("InitState", "enter");

    public void CollisionFoodObj(GameObject i_GameObject)
    {
        dedugger("CollisionFoodObj", "enter");
    }

    public void InitState()
    {
        dedugger("InitState", "enter");
        m_PickUpItem.ForceMulti = 0;
        //m_PickUpItem.SetThrowingAnim(!PickUpItem.k_ThrowObj);
    }

    public bool IsPassStage()
    {
        if (m_GameObject != null)
        {
            return m_PickUpItem.Power.Press;
        }
        //Debug.Log("Update-By-State-StateFree-IsPassStage");

        return false;
    }
    private void dedugger(string func, string i_var)
    {
        GameGlobal.Dedugger(k_ClassName, func, i_var);
    }

    private bool isPickUpDistance()
    {
        return Vector3.Distance(m_GameObject.transform.position, m_PickUpItem.transform.position) <= 2;
    }

    public void UpdateByState()
    {
        if (IsPassStage())
        {
            dedugger("UpdateByState", " IsPassStage enter");
            m_PickUpItem.SetGameFoodObj(m_GameObject);
        }
    }

    public void EnterCollisionFoodObj(GameObject i_GameObject)
    {
        dedugger("EnterCollisionFoodObj", "enter");
        m_GameObject = i_GameObject;
    }

    public void ExitCollisionFoodObj()
    {
        dedugger("ExitCollisionFoodObj", "enter");
        m_GameObject = null;
    }
}