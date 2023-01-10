using Assets.Scripts.PickUpItem;
using DiningCombat;
using UnityEngine;

internal class StateHoldsObj : IStatePlayerHand
{
    private PickUpItem m_PickUpItem;
    private KeysHamdler m_KeyHamdler;
    public StateHoldsObj(PickUpItem i_PickUpItem, KeysHamdler i_KeyHamdler)
    {
        m_PickUpItem = i_PickUpItem;
        m_KeyHamdler = i_KeyHamdler;
    }

    public void InitState()
    {
        m_PickUpItem.SetPhysics(false);
        m_PickUpItem.SetTransform();
        m_PickUpItem.IsItemPicked = true;
        m_PickUpItem.ForceMulti = 0;
    }

    public bool IsPassStage()
    {
        return m_KeyHamdler.Down;
    }

    public void UpdateByState()
    {
        if (IsPassStage())
        {
            m_PickUpItem.StatePlayerHand = new StatePowering(m_PickUpItem, m_KeyHamdler);
        }
    }
}
