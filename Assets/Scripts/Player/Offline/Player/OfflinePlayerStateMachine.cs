using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using UnityEngine;

namespace DiningCombat.Player.Manger
{
    internal class OfflinePlayerStateMachine : BridgeImplementorAcitonStateMachine
    {
        internal override void BuildState()
        {
            SetStates(new StateFreeOffline(Player, this), new StateHoldsObjOffline(Player, this),
                new StatePoweringOffline(Player, this), new StateThrowingOffline(Player, this));

        }

        internal class StateFreeOffline : StateFree
        {
            public StateFreeOffline(BridgeAbstractionAction i_PickUpItem, OfflinePlayerStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }

            protected override bool IsPassStage()
            {
                if (this.IsBufferTime)
                {
                    return this.HaveGameObject && this.IsPowerKeyPress;
                }
                return false;
            }
        }

        internal class StateHoldsObjOffline : StateHoldsObj
        {
            public StateHoldsObjOffline(BridgeAbstractionAction i_PickUpItem, OfflinePlayerStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }

            protected override bool IsPassStage()
            {
                return this.IsPowerKeyPress;
            }
        }

        internal class StatePoweringOffline : StatePowering
        {
            public StatePoweringOffline(BridgeAbstractionAction i_PickUpItem, OfflinePlayerStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }

            protected override bool IsPassStage()
            {
                if (this.IsBufferTime)
                {
                    return Input.GetKeyUp(KeyCode.E);
                }

                return false;
            }
        }
        internal class StateThrowingOffline : StateThrowing
        {
            public StateThrowingOffline(BridgeAbstractionAction i_PickUpItem, OfflinePlayerStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
            }
        }
    }
}
