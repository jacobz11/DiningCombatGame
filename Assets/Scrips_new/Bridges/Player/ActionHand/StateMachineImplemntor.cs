using Assets.Scripts.Player.PickUpItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scrips_new.DesignPatterns;

namespace Assets.Scrips_new.Bridges.Player.ActionHand
{
    internal class StateMachineImplemntor : MonoBehaviourStateMachine
    {   
        private PlayersHand m_PlayersHand;
        public PlayersHand Player
        {
            get { return m_PlayersHand; }
            set 
            {
                if (m_PlayersHand != null)
                {
                    Player = value;
                }
                else
                {
                    Debug.LogError("Can't reboot more than once");
                }
            }
        }

        public virtual void SetPlayersHand(PlayersHand Pla)
        {
            Player = Pla;
        }

        public virtual void Update()
        {
            CurrentStates.OnStateUpdate();
        }
    }
}
