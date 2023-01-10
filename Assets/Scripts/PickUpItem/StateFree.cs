using Assets.Scripts.PickUpItem;
using DiningCombat;
using UnityEngine;

internal class StateFree : IStatePlayerHand
{
    private PickUpItem m_PickUpItem;
    private KeysHamdler m_KeyHamdler;

    public StateFree(PickUpItem i_PickUpItem, KeysHamdler i_KeyHamdler)
    {
        m_PickUpItem = i_PickUpItem;
        m_KeyHamdler = i_KeyHamdler;
    }

    public void InitState()
    {
        m_PickUpItem.ForceMulti = 0;
    }

    public bool IsPassStage()
    {
        return m_PickUpItem.IsDistance() && m_KeyHamdler.Press && m_PickUpItem.HasItem;
    }

    public void UpdateByState()
    {
        if (IsPassStage())
        {
            m_PickUpItem.StatePlayerHand = new StateHoldsObj(m_PickUpItem, m_KeyHamdler);
        }
    }
}