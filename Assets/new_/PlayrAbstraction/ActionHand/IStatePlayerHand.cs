using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using Assets.Scripts.Util.Channels;
using DesignPatterns.Abstraction;
using DiningCombat.Player;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Player.Offline.Player.States
{
    internal interface IStatePlayerHand : IDCState
    {
        void Update();
        void ExitCollisionFoodObj(Collider other);
        void EnterCollisionFoodObj(Collider other);
        bool OnPickUpAction();
        void OnChargingAction();
    }
}
