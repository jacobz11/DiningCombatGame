using DesignPatterns.Abstraction;
using Unity.VisualScripting;
using UnityEngine;

internal interface IStatePlayerHand : IDCState
{
    void Update();
    void ExitCollisionFoodObj(Collider other);
    void EnterCollisionFoodObj(Collider other);
    bool OnPickUpAction(out GameFoodObj o_Collcted);
    bool OnThrowPoint(out float o_Force);

    bool OnChargingAction { get; set; }
}