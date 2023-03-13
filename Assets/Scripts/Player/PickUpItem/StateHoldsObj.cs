using Assets.Scripts.PickUpItem;
using Assets.Scripts.Player;
using DiningCombat;
using UnityEngine;

/// <summary>
/// This class represents the situation in which
/// The player <b> holding</b> an object of <see cref="GameFoodObj"/>
/// implenrt the interface <see cref="IStatePlayerHand"/>
/// </summary>
internal class StateHoldsObj : IStatePlayerHand
{
    private float initTimeEnteState;

    /// <summary>
    /// Initializes a new instance of the <see cref="StateHoldsObj"/> class.
    /// </summary>
    /// <param name="i_PickUpItem">the holdingPoint</param>
    public StateHoldsObj(HandPickUp i_PickUpItem)
        : base(i_PickUpItem)
    {
    }

    /// <inheritdoc/>
    public override void InitState()
    {
        this.playrHand.ForceMulti = 0;
        this.initTimeEnteState = Time.time;
        Debug.Log("init state : StateHoldsObj");
    }

    /// <inheritdoc/>
    public override bool IsPassStage()
    {
        return this.IsPowerKeyPress;
    }

    /// <inheritdoc/>
    public override void UpdateByState()
    {
        if (this.IsPassStage())
        {
            this.playrHand.StatePlayerHand++;
        }
    }
}
