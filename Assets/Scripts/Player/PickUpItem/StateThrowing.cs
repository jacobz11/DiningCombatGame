using Assets.Scripts.PickUpItem;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.PickUpItem
{
    /// <summary>
    /// This mode should create synchronization between the shot and the animation
    /// </summary>
    internal class StateThrowing : IStatePlayerHand
    {
        private const float KFramesToThrow = 1.9f;
        private const float KMaxFrame = 2.1f;
        private float numOfFrames;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateThrowing"/> class.
        /// </summary>
        /// <param name="i_PickUpItem">the holdingPoint</param>
        public StateThrowing(HandPickUp i_PickUpItem)
            : base(i_PickUpItem)
        {
            this.numOfFrames = 0;
        }

        /// <inheritdoc/>
        public override void InitState()
        {
            this.playrHand.ThrowingAnimator = true;
            this.numOfFrames = 0;
        }

        /// <inheritdoc/>
        public override bool IsPassStage() => this.numOfFrames >= KFramesToThrow;

        /// <inheritdoc/>
        public override void UpdateByState()
        {
        }
    }
}




//using Assets.Scripts.PickUpItem;
//using System.Collections;
//using UnityEngine;

//namespace Assets.Scripts.Player.PickUpItem
//{
//    /// <summary>
//    /// This mode should create synchronization between the shot and the animation
//    /// </summary>
//    internal class StateThrowing : IStatePlayerHand
//    {

//        private const float k_FramesToThrow = 1.9f;
//        private const float k_MaxFrame = 2.1f;
//        private float m_NumOfFrames;
//        private bool m_ThrowingEnd;
//        private bool m_IsThrownAway;
//        private HandPickUp m_PickUpItem;

//        public StateThrowing(HandPickUp i_PickUpItem)
//        {
//            m_PickUpItem = i_PickUpItem;
//            m_ThrowingEnd = false;
//            m_IsThrownAway = false;
//        }
//        public void EnterCollisionFoodObj(Collider other)
//        {
//            // for now this is should be empty
//            // the implementing only in StateFree
//        }

//        public void ExitCollisionFoodObj(Collider other)
//        {
//            // for now this is should be empty
//            // the implementing only in StateFree
//        }

//        public void InitState()
//        {
//            m_PickUpItem.ThrowingAnimator = true;
//            m_ThrowingEnd = false;
//            m_IsThrownAway = false;
//        }

//        public bool IsPassStage()
//        {
//            return m_ThrowingEnd;
//        }

//        public void UpdateByState()
//        {
//            //if (m_IsThrownAway)
//            //{
//            //    Debug.Log("in m_IsThrownAway");
//            //    m_IsThrownAway = false;
//            //    m_PickUpItem.ThrowObj();
//            //}
//            //if (IsPassStage())
//            //{
//            //    m_PickUpItem.ThrowingAnimator = false;
//            //    m_PickUpItem.StatePlayerHand++;
//            //}

//            if (IsPassStage() && !m_IsThrownAway)
//            {
//                Debug.Log("now need to Throw");
//                m_IsThrownAway = true;
//            }

//            if (m_NumOfFrames > k_MaxFrame)
//            {
//                m_PickUpItem.StatePlayerHand++;
//            }

//            m_NumOfFrames += 1 * Time.deltaTime;
//        }

//        public void SetEventTrowingEnd()
//        {
//            Debug.Log("in SetEventTrowingEnd");
//            m_ThrowingEnd = true;
//        }

//        public void SetEventTrowing()
//        {
//            Debug.Log("in SetEventTrowing");

//            m_IsThrownAway = true;
//        }
//    }
//}

