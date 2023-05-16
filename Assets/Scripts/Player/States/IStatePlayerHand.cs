using DesignPatterns.Abstraction;
using UnityEngine;
// TODO : Add a namespace
internal interface IStatePlayerHand : IDCState
{
    void Update();
    void ExitCollisionFoodObj(Collider other);
    void EnterCollisionFoodObj(Collider other);
    bool OnPickUpAction(out GameFoodObj o_Collcted);
    bool OnThrowPoint(out float o_Force);

    bool OnChargingAction { get; set; }
}