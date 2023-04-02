using System;
using UnityEngine;
using DiningCombat.FoodObj;

namespace DiningCombat.Player
{
    public class BridgeAbstractionAction : MonoBehaviour
    {
        [SerializeField]
        [Range(500f, 3000f)]
        public int m_MaxCargingPower = 1800;
        private float m_ChargingPower;
        private GameFoodObj m_FoodItem;
        private Transform m_PikUpPonit;
        private PlayerAnimationChannel m_AnimationChannel;

        public Transform PikUpPonit
        {
            get => m_PikUpPonit;
            private set => m_PikUpPonit= value;
        }
        public bool ThrowingAnimator 
        {
            get => false;
            internal set
            {
                m_AnimationChannel.SetPlayerAnimationToThrow(value); 
            }
        }

        public float ForceMulti
        {
            get => this.m_ChargingPower;
        }

        public void SetForceMulti(float value)
        {
            this.m_ChargingPower = value;
        }

        private void Awake()
        {
            m_AnimationChannel = gameObject.GetComponentInChildren<PlayerAnimationChannel>();
        }

        internal void SetGameFoodObj(GameObject i_GameObject)
        {
            bool isSucceed = false;

            if (i_GameObject == null)
            {
                this.m_FoodItem = null;
            }
            else
            {
                GameFoodObj obj = i_GameObject.GetComponent<GameFoodObj>();
                if (obj != null)
                {
                    isSucceed = true;
                    this.m_FoodItem = obj;
                    obj.PickedUp(this);
                }
            }
        }

        internal void ThrowObj()
        {
            if (this.m_FoodItem == null)
            {
                Debug.LogError("foodItem is null");
            }
            else
            {
                GameFoodObj gameFoodObj = m_FoodItem.GetComponent<GameFoodObj>();
                m_FoodItem.ThrowFood(ForceMulti, this.m_PikUpPonit.transform.forward);
                m_FoodItem = null;
            }
        }

        public void SetPickUpPoint(Transform i_PickUpPoint)
        {
            if (PikUpPonit is null)
            {
                PikUpPonit = i_PickUpPoint;
            }
            else
            {
                Debug.LogError("Try to set PickUpPoint more then once");
            }
        }

        internal bool DidIHurtMyself(Collision i_Collision)
        {
            return this.gameObject.Equals(i_Collision.gameObject);
        }
    }
}