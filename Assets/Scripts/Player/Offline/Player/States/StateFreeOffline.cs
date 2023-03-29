using DesignPatterns.Abstraction;
using DiningCombat.Player.Manger;
using System;
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
            //Debug.Log("ExitCollisionFoodObj");
            this.m_FoodObj = null;
        }

        public override void OnStateExit(params object[] list)
        {
        }

        public override void OnStateMove(params object[] list)
        {
        }

        public override void OnStateIK(params object[] list)
        {
        }
        public override void OnStateEnter(params object[] list)
        {
            this.m_FoodObj = null;
            Debug.Log("init state : StateFree");
        }

        public override void OnStateUpdate(params object[] list)
        {
            bool isPassStage = this.HaveGameObject && Input.GetKey(KeyCode.E);
            
            if (isPassStage)
            {
                Debug.Log("isPassStage ");
                PlayerCollectedFood?.Invoke(this.m_FoodObj);
            }
        }

        public override string ToString()
        {
            return "StateFree : ";
        }
    }
}