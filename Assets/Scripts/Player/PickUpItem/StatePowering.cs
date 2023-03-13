using Assets.Scripts.PickUpItem;
using Assets.Scripts.Player;
using UnityEngine;

/// <summary>
/// This class represents the situation in which
/// The player <b>holding</b> an object of <see cref="GameFoodObj"/>
/// and the player is Powering 
/// this class will:
/// <para>OR Throw Obj  <see cref="HandPickUp.ThrowObj"/> </para>
/// <para>OR add add To Force Multi  <see cref="addToForceMulti"/></para>
/// <para>OR go back to <see cref="StateHoldsObj"/></para>
/// implenrt the interface <see cref="IStatePlayerHand"/>
/// </summary>
internal class StatePowering : IStatePlayerHand
{
    private float initTimeEnteState;

    private bool IsBufferTime => Time.time - this.initTimeEnteState > 0.2f;

    public StatePowering(HandPickUp i_PickUpItem)
        : base(i_PickUpItem)
    {
    }

    /// <inheritdoc/>
    public override void InitState()
    {
        Debug.Log("init state : StatePowering");
        this.initTimeEnteState = Time.time;
    }

    /// <inheritdoc/>
    public override bool IsPassStage()
    {
        if (this.IsBufferTime)
        {
            return Input.GetKeyUp(KeyCode.E) && this.playrHand.ForceMulti > 50;
        }

        return false;
    }

    /// <inheritdoc/>
    public override void UpdateByState()
    {
        if (this.IsPowerKeyPress)
        {
            this.AddToForceMulti();
        }
        else if (this.IsPassStage())
        {
            this.playrHand.StatePlayerHand++;
        }
        else
        {
            this.StepBack();
        }
    }

    private void AddToForceMulti()
    {
        this.playrHand.ForceMulti += 1400 * Time.deltaTime;
    }

    private void StepBack()
    {
        if (this.IsBufferTime)
        {
            this.playrHand.StatePlayerHand--;
        }
    }
}