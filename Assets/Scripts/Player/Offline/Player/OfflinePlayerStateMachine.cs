using UnityEngine;
using System.Collections.Generic;
using DiningCombat.Player.Offline.State;
using DesignPatterns.Abstraction;
using DiningCombat.FoodObj;

namespace DiningCombat.Player.Manger
{
    public class OfflinePlayerStateMachine : MonoBehaviourStateMachine
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
        protected virtual void OnPlayerSetFoodObj(GameObject i_ColctedFoodObj)
        {
            if (i_ColctedFoodObj == null)
            {

            }
            else if (i_ColctedFoodObj.GetComponent<GameFoodObj>() != null)
            {
                m_FoodObj = i_ColctedFoodObj;
            }
            else
            {
                Debug.LogError("Colcted Food Obj is null not FoodObj type");
            }
        }

        internal void BuildOfflinePlayerState()
        {
            if (m_PlayersHand != null)
            {
                List<DCState> states = new List<DCState>();
                StateFreeOffline free = new StateFreeOffline(0, "free");
                states.Add(free);
                free.PlayerCollectedFood += OnPlayerSetFoodObj;

                StateHoldsObjOffline holdsObj =  new StateHoldsObjOffline(1, "holding");
                states.Add(holdsObj);

                StatePoweringOffline powering = new StatePoweringOffline(2, "powering");
                states.Add(powering);
                StateThrowingOffline throwing = new StateThrowingOffline(3, "throwing");
                states.Add(throwing);

                SetStates(states);
            }
            else
            {
                Debug.LogError("Missing component PlayersHand - It is impossible to create StateMachineImplemntor without a PlayersHand");
            }
        }

    }
}
