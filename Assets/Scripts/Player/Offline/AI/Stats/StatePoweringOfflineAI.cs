using Assets.Scrips_new.AI.Algo;
using Assets.Scripts.AI;
using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using DesignPatterns.Abstraction;
using DiningCombat.Player;
using DiningCombat.Player.Manger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Offline.AI.Stats
{
    internal class StatePoweringOfflineAI : StatePowering
    {

        public StatePoweringOfflineAI(PlayerHand i_PickUpItem, OfflineAIStateMachine i_Machine) 
            : base(i_PickUpItem, i_Machine)
        {
        }

        protected override bool IsPassStage()
        {
            return false;
        }
    }
}
