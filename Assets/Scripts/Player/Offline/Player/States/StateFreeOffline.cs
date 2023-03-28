using DesignPatterns.Abstraction;
using DiningCombat.Player.Manger;
using System;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace DiningCombat.Player.Offline.State
{
    internal class StateFreeOffline : DCState
    {
        public event Action<GameObject> PlayerCollectedFood;

        public GameObject m_FoodObj;

        private bool HaveGameObject => this.m_FoodObj != null;

        private void Awake()
        {
            InternalMangerPlayer mangerPlayer = gameObject.GetComponent<InternalMangerPlayer>();
            if (mangerPlayer == null ) 
            {
                Debug.Log("mangerPlayer is null");
            }

            PlayerChannel channel = gameObject.GetComponentInChildren<PlayerChannel>();

            if (channel == null)
            {
                Debug.Log("channel is null");
            }
            else
            {
                channel.PickUpZonEnter += EnterCollisionFoodObj;
                channel.PickUpZonExit += ExitCollisionFoodObj;

            }
        }
        public void EnterCollisionFoodObj(Collider other)
        {
            this.m_FoodObj = other.gameObject;
        }
        public void ExitCollisionFoodObj(Collider other)
        {
            this.m_FoodObj = null;
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