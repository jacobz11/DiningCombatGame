using Assets.Scripts.PickUpItem;
using DiningCombat;
using UnityEngine;

internal class StatePowering : IStatePlayerHand
{
    private PickUpItem m_PickUpItem;
    private KeysHamdler m_KeyHamdler;

    public StatePowering(PickUpItem i_PickUpItem, KeysHamdler i_KeyHamdler)
    {
        m_PickUpItem = i_PickUpItem;
        m_KeyHamdler = i_KeyHamdler;
    }

    public void InitState()
    {
    }

    public bool IsPassStage()
    {
        return m_KeyHamdler.Up && m_PickUpItem.ForceMulti > 10;
    }

    public void UpdateByState()
    {
        if (m_KeyHamdler.Press)
        {
            m_PickUpItem.ForceMulti += 1400 * Time.deltaTime;
        }
        else if (IsPassStage())
        {
            m_PickUpItem.ThrowObj();
            m_PickUpItem.StatePlayerHand = new StateFree(m_PickUpItem, m_KeyHamdler);
        }
        else
        {
            m_PickUpItem.StatePlayerHand = new StateHoldsObj(m_PickUpItem, m_KeyHamdler);
        }
    }
}
