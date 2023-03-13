namespace Assets.Scripts.Player.StatePlayerRightHand
{
    using UnityEngine;

    /// <summary>
    /// This class represents the situation in which
    /// The player <b>not holding</b> an object of <see cref="GameFoodObj"/>
    /// and the player is looking for <see cref="GameFoodObj"/> to pick up
    /// <para>THIS IS INIT Stat </para>
    /// implenrt the interface <see cref="IStatePlayerHand"/>
    /// </summary>
    internal class StateFree : IStatePlayerHand
    {
        private GameObject gameObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateFree"/> class.
        /// </summary>
        /// <param name="i_PickUpItem">the player</param>
        public StateFree(PlayerRightHand i_PickUpItem)
            : base(i_PickUpItem)
        {
            this.gameObject = null;
        }

        private bool HaveGameObject => this.gameObject != null;

        /// <inheritdoc/>
        public override void InitState()
        {
            this.ForceMulti = 0;
            this.playrHand.CollectFoodItem(null);
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
                if (this.playrHand.CollectFoodItem(this.gameObject))
                {
                    this.playrHand.StatePlayerHand++;
                }
            }
        }

        /// <inheritdoc/>
        public override void EnterCollisionFoodObj(Collider other)
        {
            if (other.gameObject.CompareTag("FoodObj"))
            {
                this.gameObject = other.gameObject;
            }
        }

        /// <inheritdoc/>
        public override void ExitCollisionFoodObj(Collider other)
        {
            if (other.gameObject.CompareTag(GameGlobal.TagNames.FoodObj))
            {
                this.gameObject = null;
            }
        }
    }
}
