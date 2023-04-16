//using Assets.Scripts.Player.Offline.Player.States;
//using DesignPatterns.Abstraction;
//using DiningCombat.FoodObj;
//using DiningCombat.Player;
//using System;
//using UnityEngine;

//namespace Assets.Scripts.Player.PlayrAbstraction.ActionHand
//{
//    internal class BridgeImplementorAcitonStateMachine : MonoBehaviourStateMachine
//    {
//        private BridgeAbstractionAction m_PlayersHand;
//        private GameObject m_FoodObj;

//        public StateFree Free
//        {
//            get => m_States[0] as StateFree;
//        }

//        public StateHoldsObj HoldsObj
//        {
//            get => m_States[1] as StateHoldsObj;
//        }

//        public StatePowering Powering
//        {
//            get => m_States[2] as StatePowering;
//        }

//        public StateThrowing Throwing
//        {
//            get => m_States[3] as StateThrowing;
//        }
//        public BridgeAbstractionAction Player
//        {
//            get { return m_PlayersHand; }
//            set
//            {
//                if (m_PlayersHand == null)
//                {
//                    m_PlayersHand = value;
//                }
//                else
//                {
//                    Debug.LogError("Can't reboot more than once");
//                }
//            }
//        }

//        public virtual void SetPlayerHand(BridgeAbstractionAction i_Player)
//        {
//            Player = i_Player;
//        }

//        public virtual void Update()
//        {
//            CurrentStates.OnStateUpdate();
//        }

//        public virtual void OnPlayerSetFoodObj(GameObject i_ColctedFoodObj)
//        {
//            if (i_ColctedFoodObj == null)
//            {
//                m_CurrentState = 0;
//                Debug.Log("OnPlayerSetFoodObj is null");
//            }
//            else if (i_ColctedFoodObj.GetComponent<FoodObject>() != null)
//            {
//                Debug.Log("OnPlayerSetFoodObj");
//                m_FoodObj = i_ColctedFoodObj;
//            }
//            else
//            {
//                Debug.LogError("Colcted Food Obj is null not FoodObj type");
//            }
//        }

//        public virtual void SetStates(IStatePlayerHand[] i_States)
//        {
//            if (IsValidStates(i_States))
//            {
//                m_States = i_States;
//                m_CurrentState = m_InitialState;
//            }
//            else
//            {
//                Debug.LogError("SetStates is not valid");
//            }
//        }

//        private static bool IsValidStates(IStatePlayerHand[] i_States)
//        {
//            bool isValid = i_States is not null && i_States.Length != 4;

//            if (!isValid)
//            {
//                Debug.Log("IsValidStates");
//                return false;
//            }

//            bool isFree = i_States[0] is StateFree;
//            bool isHolding = i_States[1] is StateHoldsObj;
//            bool isPowering = i_States[2] is StatePowering;
//            bool isThrowing = i_States[3] is StateThrowing;

//            Debug.Log("isFree " + isFree + " isHolding" + isHolding + " isPowering" + isPowering + " isThrowing ");
//            return isFree && isHolding && isThrowing && isPowering;
//        }

//        internal virtual void BuildState()
//        {
//            Debug.LogWarning("Aciton Hand State Machine is nut implentting BuildState");
//        }

//        internal void OnJumpingEnd()
//        {
//        }
//    }
//}
