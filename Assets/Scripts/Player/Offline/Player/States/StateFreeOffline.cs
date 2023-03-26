using DesignPatterns.Abstraction;
using System;
using UnityEngine;

namespace DiningCombat.Player.Offline.State
{
    internal class StateFreeOffline : DCState
    {
        public event Action<GameObject> PlayerCollectedFood;

        public GameObject m_FoodObj;
        public StateFreeOffline(byte stateId, string stateName)
            : base(stateId, stateName)
        {
        }

        private bool HaveGameObject => this.m_FoodObj != null;


        public void EnterCollisionFoodObj(bool isEnter, Collider other)
        {
            if (other.gameObject.CompareTag(GameGlobal.TagNames.k_FoodObj))
            {
                if (isEnter)
                {
                    this.m_FoodObj = other.gameObject;
                }
                else
                {
                    this.m_FoodObj = null;
                }
            }
        }

        public virtual void OnStateExit(params object[] list)
        {
        }

        public virtual void OnStateMove(params object[] list)
        {
        }

        public virtual void OnStateIK(params object[] list)
        {
        }
        public virtual void OnStateEnter(params object[] list)
        {
            this.m_FoodObj = null;
            Debug.Log("init state : StateFree");
        }

        public virtual void OnStateUpdate(params object[] list)
        {
            bool isPassStage = this.HaveGameObject && Input.GetKey(KeyCode.E);
            if (isPassStage)
            {
                PlayerCollectedFood?.Invoke(this.m_FoodObj);
            }
        }

        public override string ToString()
        {
            return "StateFree : ";
        }
    }
}