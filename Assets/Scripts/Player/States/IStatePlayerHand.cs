using DiningCombat.FoodObject;
using DiningCombat.Util.DesignPatterns;
using UnityEngine;
namespace DiningCombat.Player.States
{
    public interface IStatePlayerHand : IDCState
    {
        void Update();
        void ExitCollisionFoodObj(Collider other);
        void EnterCollisionFoodObj(Collider other);
        bool OnPickUpAction(out GameFoodObj o_Collcted);
        bool OnThrowPoint(out float o_Force);

        bool OnChargingAction { get; set; }
    }
}