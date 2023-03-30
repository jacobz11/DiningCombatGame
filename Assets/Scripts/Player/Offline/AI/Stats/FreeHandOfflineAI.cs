using Assets.Scrips_new.AI.Algo;
using Assets.Scripts.AI;
using Assets.Scripts.AI.Algo;
using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using DesignPatterns.Abstraction;
using DiningCombat.Player;
using DiningCombat.Player.Manger;
using System;
using UnityEngine;

namespace Assets.Scrips_new.AI.Stats
{
    internal class FreeHandOfflineAI : StateFree
    {
        public FreeHandOfflineAI(PlayerHand i_PickUpItem, OfflineAIStateMachine i_Machine) 
            : base(i_PickUpItem, i_Machine)
        {
        }

        protected override bool IsPassStage()
        {
            return false;
        }
    }
}
