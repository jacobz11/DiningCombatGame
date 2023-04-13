using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using DiningCombat.Player.Manger;
using DiningCombat.Player;
using Assets.Scripts.Player.Trinnig;
using DesignPatterns.Abstraction;
using Unity.MLAgents.Actuators;

namespace Assets.Scripts.Player.Offline.AI.ML
{
    internal class MLAcitonStateMachine : BridgeImplementorAcitonStateMachine //, IMLCollecting
    {
        public override void Update()
        {
            base.Update();
            (CurrentStates as IMLState).ClearData();
        }

        public void SetMlAgnetData(DCActionBuffers i_MlAgnetData)
        {
            (CurrentStates as IMLState).SetData(i_MlAgnetData.IsPress);
        }

        internal override void BuildState()
        {
            SetStates(new FreeHandML(Player, this), new StateHoldsML(Player, this),
                new StatePoweringML(Player, this), new StateThrowingML(Player, this));
        }

        internal class FreeHandML : StateFree , IMLState
        {
            private bool m_Data;

            public FreeHandML(BridgeAbstractionAction i_PickUpItem, MLAcitonStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
                m_Data = false;
            }

            public void ClearData()
            {
                m_Data = false;
            }

            public void SetData(bool i_Data)
            {
                m_Data = i_Data;
            }

            protected override bool IsPassStage()
            {
                return HaveGameObject;
            }
        }

        internal class StateHoldsML : StateHoldsObj, IMLState
        {
            private bool m_Data;

            public StateHoldsML(BridgeAbstractionAction i_PickUpItem, MLAcitonStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
                m_Data = false;
            }

            public void ClearData()
            {
                m_Data = false;
            }

            public void SetData(bool i_Data)
            {
                m_Data = i_Data;
            }

            protected override bool IsPassStage()
            {
                return m_Data;
            }
        }

        internal class StatePoweringML : StatePowering, IMLState
        {
            private bool m_Data;

            public StatePoweringML(BridgeAbstractionAction i_PickUpItem, MLAcitonStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
                m_Data = false;
            }

            public void ClearData()
            {
                m_Data = false;
            }

            public void SetData(bool i_Data)
            {
                m_Data = i_Data;
            }

            protected override bool IsAddPower()
            {
                return m_Data;
            }

            protected override bool IsPassStage()
            {
                return !m_Data;
            }
        }

        internal class StateThrowingML : StateThrowing, IMLState
        {
            private bool m_Data;
            public StateThrowingML(BridgeAbstractionAction i_PickUpItem, MLAcitonStateMachine i_Machine)
                : base(i_PickUpItem, i_Machine)
            {
                m_Data = false;
            }

            public void SetData(bool i_Data)
            {
                m_Data = false;
            }

            public void ClearData()
            {
                m_Data = false;
            }
        }
   
    }
}

//    public void ClearData()
//    {
//        m_Data = false;
//    }

//    public bool SetData(bool i_Data)
//    {
//        m_Data = i_Data;
//    }
//}