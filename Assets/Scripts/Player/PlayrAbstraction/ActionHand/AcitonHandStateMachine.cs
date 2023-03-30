using DesignPatterns.Abstraction;
using DiningCombat.FoodObj;
using DiningCombat.Player.Offline.State;
using DiningCombat.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.PlayrAbstraction.ActionHand
{
    internal class AcitonHandStateMachine : MonoBehaviourStateMachine
    {
        private PlayerHand m_PlayersHand;
        private GameObject m_FoodObj;

        public PlayerHand Player
        {
            get { return m_PlayersHand; }
            set
            {
                if (m_PlayersHand == null)
                {
                    m_PlayersHand = value;
                }
                else
                {
                    Debug.LogError("Can't reboot more than once");
                }
            }
        }

        public virtual void SetPlayerHand(PlayerHand i_Player)
        {
            Debug.Log("SetPlayerHand ");
            Player = i_Player;
        }

        public virtual void Update()
        {
            CurrentStates.OnStateUpdate();
        }
        public virtual void OnPlayerSetFoodObj(GameObject i_ColctedFoodObj)
        {
            if (i_ColctedFoodObj == null)
            {
                Debug.Log("OnPlayerSetFoodObj is null");
            }
            else if (i_ColctedFoodObj.GetComponent<GameFoodObj>() != null)
            {
                Debug.Log("OnPlayerSetFoodObj");
                m_FoodObj = i_ColctedFoodObj;
            }
            else
            {
                Debug.LogError("Colcted Food Obj is null not FoodObj type");
            }
        }

        //internal void BuildOfflinePlayerState()
        //{
        //    if (m_PlayersHand != null)
        //    {
        //        List<IDCState> states = new List<IDCState>();
        //        StateFreeOffline free = new StateFreeOffline(m_PlayersHand, this);
        //        states.Add(free);
        //        free.PlayerCollectedFood += OnPlayerSetFoodObj;


        //        StateHoldsObjOffline holdsObj = new StateHoldsObjOffline(m_PlayersHand, this);
        //        states.Add(holdsObj);

        //        StatePoweringOffline powering = new StatePoweringOffline(m_PlayersHand, this);
        //        states.Add(powering);

        //        StateThrowingOffline throwing = new StateThrowingOffline(m_PlayersHand, this);
        //        states.Add(throwing);

        //        SetStates(states);
        //    }
        //    else
        //    {
        //        Debug.LogError("Missing component PlayersHand - It is impossible to create StateMachineImplemntor without a PlayersHand");
        //    }
        //}
    }
}
