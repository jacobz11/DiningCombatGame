using Assets.Scripts.Player.Offline.Player.States;
using DiningCombat.Player;
using DiningCombat;
using System;
using UnityEngine;

namespace Assets.Scripts.Player.PlayrAbstraction.ActionHand
{
    internal abstract class StateFree : IStatePlayerHand
    {
        public event Action<GameObject> PlayerCollectedFood;

        public GameObject m_FoodObj;

        public StateFree(PlayerHand i_PickUpItem, AcitonHandStateMachine i_Machine)
            : base(i_PickUpItem, i_Machine)
        {
            this.m_FoodObj = null;
        }

        protected bool HaveGameObject => this.m_FoodObj != null;

        protected abstract bool IsPassStage();

        public override void EnterCollisionFoodObj(Collider other)
        {
            if (other.gameObject.CompareTag(GameGlobal.TagNames.k_FoodObj))
            {
                this.m_FoodObj = other.gameObject;
            }
        }

        public override void ExitCollisionFoodObj(Collider other)
        {
            if (other.gameObject.CompareTag(GameGlobal.TagNames.k_FoodObj))
            {
                this.m_FoodObj = null;
            }
        }

        public override void OnStateExit(params object[] list)
        {
        }

        public override void OnStateEnter(params object[] list)
        {
            this.m_PlayrHand.SetGameFoodObj(null);
            this.m_FoodObj = null;
            base.OnStateEnter(list);
            Debug.Log("init state : StateFree");
        }

        public override void OnStateUpdate(params object[] list)
        {
            if (this.IsPassStage())
            {
                this.m_PlayrHand.SetGameFoodObj(this.m_FoodObj);
                this.m_Machine.StatesIndex++;
            }
        }

        public override string ToString()
        {
            return "StateFree : ";
        }
    }
}
