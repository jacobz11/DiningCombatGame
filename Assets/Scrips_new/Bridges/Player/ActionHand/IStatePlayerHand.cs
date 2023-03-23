
using Assets.Scripts.Player;
using UnityEngine;

public abstract class IStatePlayerHand
{
    protected HandPickUp playrHand;

    protected IStatePlayerHand(HandPickUp i_PickUpItem)
    {
        this.playrHand = i_PickUpItem;
    }

    public bool IsPowerKeyPress => Input.GetKey(KeyCode.E);

    public abstract void UpdateByState();

    public abstract void InitState();

    public abstract bool IsPassStage();

    public virtual void EnterCollisionFoodObj(Collider other)
    {
    }

    public virtual void ExitCollisionFoodObj(Collider other)
    {
    }
}