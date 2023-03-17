using Assets.Scripts.PickUpItem;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.PickUpItem
{
    /// <summary>
    /// This mode should create synchronization between the shot and the animation
    /// </summary>
    internal class StateThrowing : IStatePlayerHand
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StateThrowing"/> class.
        /// </summary>
        /// <param name="i_PickUpItem">the holdingPoint</param>
        public StateThrowing(HandPickUp i_PickUpItem)
            : base(i_PickUpItem)
        {
        }

        /// <inheritdoc/>
        public override void InitState()
        {
            this.playrHand.ThrowingAnimator = true;
        }

        /// <inheritdoc/>
        public override bool IsPassStage() => !this.playrHand.ThrowingAnimator;

        /// <inheritdoc/>
        public override void UpdateByState()
        {
        }
    }
}