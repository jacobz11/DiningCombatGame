using Assets.Scrips_new.AI.Algo;
using Assets.Scrips_new.AI.Stats;
using Assets.Scripts.AI.Algo;
using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using DesignPatterns.Abstraction;
using DiningCombat.FoodObj;
using DiningCombat.Player.Offline.State;
using System.Collections.Generic;
using Unity.Barracuda;
using Unity.VisualScripting;
using UnityEngine;

namespace DiningCombat.Player.Manger
{
    internal class OfflineAIStateMachine : AcitonHandStateMachine
    {
        private bool m_Bug1 = false;
        private bool m_Bug2 = false;
        public override void Update()
        {
            if (m_States is null)
            {
                if (!m_Bug1)
                {
                    m_Bug1 = true;
                    Debug.Log("m_States is null");
                }
            }
            else if ( m_States.Length == 0)
            {
                if (!m_Bug2)
                {
                    m_Bug2= true;
                    Debug.Log("m_States.Length  is 0");
                }
            }
            else
            {
                base.Update();
            }
        }
        //private PlayerHand m_PlayersHand;
        //private GameObject m_FoodObj;
        //public PlayerHand Player
        //{
        //    get { return m_PlayersHand; }
        //    set
        //    {
        //        if (m_PlayersHand == null)
        //        {
        //            m_PlayersHand = value;
        //        }
        //        else
        //        {
        //            Debug.LogError("Can't reboot more than once");
        //        }
        //    }
        //}

        //public virtual void SetPlayerHand(PlayerHand i_Player)
        //{
        //    Debug.Log("SetPlayerHand ");
        //    Player = i_Player;
        //}

        //public virtual void Update()
        //{
        //    //CurrentStates.OnStateUpdate();
        //}

        //protected virtual void OnPlayerSetFoodObj(GameObject i_ColctedFoodObj)
        //{
        //    if (i_ColctedFoodObj == null)
        //    {

        //    }
        //    else if (i_ColctedFoodObj.GetComponent<GameFoodObj>() != null)
        //    {
        //        m_FoodObj = i_ColctedFoodObj;
        //    }
        //    else
        //    {
        //        Debug.LogError("Colcted Food Obj is null not FoodObj type");
        //    }
        //}

        //internal void BuildOfflineAIState()
        //{
        //    if (m_PlayersHand != null)
        //    {
        //        //Debug.Log(" BuildOfflineAIState");
        //        //List<IDCState> states = new List<IDCState>();
        //        //FreeHandOfflineAI free = m_PlayersHand.AddComponent<FreeHandOfflineAI>();
        //        //states.Add(free);
        //        //free.PlayerCollectedFood += OnPlayerSetFoodObj;

        //        //Debug.Log(" BuildOfflineAIState");
        //        //StateHoldsObjOffline holdsObj = m_PlayersHand.AddComponent<StateHoldsObjOffline>();
        //        //holdsObj.StateId = 1;
        //        //states.Add(holdsObj);

        //        //StatePoweringOffline powering = m_PlayersHand.AddComponent<StatePoweringOffline>();
        //        //powering.StateId = 2;
        //        //states.Add(powering);

        //        //StateThrowingOffline throwing = m_PlayersHand.AddComponent<StateThrowingOffline>();
        //        //throwing.StateId = 3;
        //        //states.Add(throwing);

        //        //SetStates(states);
        //    }
        //    else
        //    {
        //        Debug.LogError("Missing component PlayersHand - It is impossible to create StateMachineImplemntor without a PlayersHand");
        //    }
        //}
    }
}