using Assets.Scripts.Player.Offline.Player.States;
using System;
using UnityEngine;
using DesignPatterns.Abstraction;

namespace Assets.Scripts.Player.PlayrAbstraction.ActionHand
{
    internal class StateFree : IStatePlayerHand
    {
        private const string k_FoodObj = "uncollct";
        //private AcitonStateMachine m_AcitonStateMachine;
        private FoodObject m_FoodObj;

        public event Action<CollectedFoodEvent> PlayerCollectedFood;

        public class CollectedFoodEvent : EventArgs
        {
            public FoodObject gameFood;
        }

        protected bool HaveGameObject => this.m_FoodObj != null;

        //public StateFree(AcitonStateMachine i_AcitonStateMachine)
        //{
        //    m_FoodObj = null;
        //    m_AcitonStateMachine = i_AcitonStateMachine;
        //}

        public void EnterCollisionFoodObj(Collider other)
        {
            if (other.transform.TryGetComponent<FoodObject>(out FoodObject o_FoodObj))
            {
                //if (o_FoodObj.CanCollect())
                //{
                //    this.m_FoodObj = o_FoodObj;
                //}
            }
        }

        public void ExitCollisionFoodObj(Collider other)
        {
            if (m_FoodObj is not null)
            {
                if (other.gameObject.CompareTag(k_FoodObj))
                {
                    this.m_FoodObj = null;
                }
            }
        }

        public override string ToString()
        {
            return "StateFree : ";
        }

        public void OnSteteEnter()
        {
            this.m_FoodObj = null;
            Debug.Log("init state : StateFree");
        }


        public void OnSteteExit()
        {
            PlayerCollectedFood?.Invoke(new CollectedFoodEvent()
            {
                gameFood = m_FoodObj
            });
        }

        public void Update()
        {
        }

        public bool OnPickUpAction()
        {
            return HaveGameObject
                && m_FoodObj.CurrentStatus.TryCollect(m_AcitonStateMachine.PicUpPoint);
        }
        public void OnChargingAction()
        {
        }

        public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
        {
            switch (i_State)
            {
                case IDCState.eState.ExitingState:
                    PlayerCollectedFood += i_Action;
                    break;
            }
        }
    }

}
