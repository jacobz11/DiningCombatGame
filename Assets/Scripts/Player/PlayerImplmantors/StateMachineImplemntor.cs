using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Scripts.FoodObj;
using Abstraction.DesignPatterns;
using Player.Offline;
using Abstraction.DiningCombat.Player;

namespace DiningCombat
{
    namespace Managers
    {
        namespace Player
        {
            public class StateMachineImplemntor : MonoBehaviourStateMachine
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
                    else if (i_ColctedFoodObj.GetComponent<FoodObj>() != null)
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
                        StateFree free = new StateFree(0, "free");
                        free.PlayerCollectedFood += OnPlayerSetFoodObj;
                        StateHoldsObj holdsObj = new StateHoldsObj(1, "holding");
                        StatePowering powering = new StatePowering(2, "powering");
                        StateThrowing throwing = new StateThrowing(3, "throwing");
                        states.Add(free);
                        states.Add(holdsObj);
                        states.Add(powering);
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
    }
}
