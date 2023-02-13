using Assets.Scripts.PickUpItem;
using DiningCombat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Player.PickUpItem
{
    internal class StateThrowing : IStatePlayerHand 
    {
        private const int k_FramesToThrow = 614;
        private const int k_MaxFrame = 800;
        private int m_NumOfFrames;
        private bool m_IsThrownAway;
        private HandPickUp m_PickUpItem;

        public StateThrowing(HandPickUp i_PickUpItem)
        {
            m_PickUpItem = i_PickUpItem;
            m_NumOfFrames = 0;
            m_IsThrownAway = false;
        }
        public void EnterCollisionFoodObj(Collider other)
        {
            // for now this is should be empty
            // the implementing only in StateFree
        }

        public void ExitCollisionFoodObj(Collider other)
        {
            // for now this is should be empty
            // the implementing only in StateFree
        }

        public void InitState()
        {
            m_PickUpItem.ThrowingAnimator = true;
            m_NumOfFrames = 0;
            m_IsThrownAway = false;
        }

        public bool IsPassStage()
        {
            return m_NumOfFrames >= k_FramesToThrow;
        }

        public void UpdateByState()
        {
            if (IsPassStage() && !m_IsThrownAway)
            {
                Debug.Log("now need to Throw");
                m_IsThrownAway = true;
                m_PickUpItem.ThrowObj();
            }
            
            if (m_NumOfFrames > k_MaxFrame)
            {
                m_PickUpItem.StatePlayerHand++;
            }
            //Debug.Log("IsPassStage() :" + IsPassStage() + ", m_NumOfFrames : " + m_NumOfFrames + " anima :" + m_PickUpItem.ThrowingAnimator);
            

            ++m_NumOfFrames;
        }

        IEnumerator syncThrowToAnimation()
        {
            for (int i = 0; i < 46; i++)
            {
                yield return null;
            }
        }
    }
}
