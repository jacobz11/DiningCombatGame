using Assets.Scripts.PickUpItem;
using DiningCombat;
using UnityEngine;

internal class StatePowering : IStatePlayerHand
{
    private const string k_ClassName = "StatePowering";
    private PickUpItem m_PickUpItem;
    private KeysHamdler m_KeyHamdler;

    public StatePowering(PickUpItem i_PickUpItem)
    {
        m_PickUpItem = i_PickUpItem;
    }

    public void CollisionFoodObj(GameObject i_GameObject)
    {
    }

    public void EnterCollisionFoodObj(GameObject i_GameObject)
    {
    }

    public void ExitCollisionFoodObj()
    {
    }
    private void dedugger(string func, string i_var)
    {
        GameGlobal.Dedugger(k_ClassName, func, i_var);
    }
    public void InitState()
    {
        dedugger("InitState", "enter");
    }

    public bool IsPassStage()
    {
        return m_KeyHamdler.Up && m_PickUpItem.ForceMulti > 10;
    }

    public void UpdateByState()
    {

        if (m_KeyHamdler.Press )
        {
            dedugger("UpdateByState", "m_KeyHamdler.Press enter");
            m_PickUpItem.ForceMulti += 1400 * Time.deltaTime;
        }
        else if (IsPassStage())
        {
            dedugger("UpdateByState", "IsPassStage() enter");

            m_PickUpItem.ThrowObj();
            m_PickUpItem.StatePlayerHand = new StateFree(m_PickUpItem);
        }
        else
        {
            dedugger("UpdateByState", "else enter");
            if (Time.deltaTime > 0.2)
            {
                m_PickUpItem.StatePlayerHand = new StateHoldsObj(m_PickUpItem);
            }
        }
    }

    
}
