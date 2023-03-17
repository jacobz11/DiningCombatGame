using Assets.Scripts.PickUpItem;
using Assets.Scripts.Player;
using DiningCombat;
using UnityEngine;

    /// <summary>
    /// This class represents the situation in which
    /// The holdingPoint <b>not holding</b> an object of <see cref="GameFoodObj"/>
    /// and the holdingPoint is looking for <see cref="GameFoodObj"/> to pick up
    /// <para>THIS IS INIT Stat </para>
    /// implenrt the interface <see cref="IStatePlayerHand"/>
    /// </summary>
    internal class StateFree : IStatePlayerHand
{
    private GameObject gameObject;

    /// <summary>
    /// Initializes a new instance of the <see cref="StateFree"/> class.
    /// </summary>
    /// <param name="i_PickUpItem">the holdingPoint</param>
    public StateFree(HandPickUp i_PickUpItem)
        : base(i_PickUpItem)
    {
        this.gameObject = null;
    }

    private bool HaveGameObject => this.gameObject != null;

    /// <inheritdoc/>
    public override void InitState()
    {
        this.playrHand.ForceMulti = 0;
        this.playrHand.SetGameFoodObj(null);
        this.gameObject = null;
        Debug.Log("init state : StateFree");
    }

    /// <inheritdoc/>
    public override bool IsPassStage()
    {
        return this.HaveGameObject && this.IsPowerKeyPress;
    }

    /// <inheritdoc/>
    public override void UpdateByState()
    {
        if (this.IsPassStage())
        {
            this.playrHand.SetGameFoodObj(this.gameObject);
            this.playrHand.StatePlayerHand++;
            Debug.Log("UpdateByState : StateFree");
        }
    }

    /// <inheritdoc/>
    public override void EnterCollisionFoodObj(Collider other)
    {
        if (other.gameObject.CompareTag(GameGlobal.TagNames.k_FoodObj))
        {
            this.gameObject = other.gameObject;
        }
    }

    /// <inheritdoc/>
    public override void ExitCollisionFoodObj(Collider other)
    {
        if (other.gameObject.CompareTag(GameGlobal.TagNames.k_FoodObj))
        {
            this.gameObject = null;
        }
    }
}
