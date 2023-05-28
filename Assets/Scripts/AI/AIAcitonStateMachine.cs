using DiningCombat.AI.States;
using DiningCombat.Manger;
using DiningCombat.Player;
using DiningCombat.Player.States;
using DiningCombat.UI;
using System;
using UnityEngine.AI;

namespace DiningCombat.AI
{
    public class AIAcitonStateMachine : ActionStateMachine
    {
        private NavMeshAgent m_Agent;

        #region Unity
        private void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();

            AIStateFree free = new AIStateFree(this, m_Agent);
            AIStateHoldsObj holdsing = new AIStateHoldsObj(m_Agent, this);
            AIStatePowering powering = new AIStatePowering(this, m_Powering, m_Agent);
            AIStateThrowing throwing = new AIStateThrowing(m_Agent);

            ManagerGameFoodObj.Instance.OnCollected += free.OnCollcatedAnyFood;
            powering.OnStopPowering += Powering_OnStopPowering;
            powering.OnStopPowering += throwing.Powering_OnStopPowering;
            holdsing.OnStartCharging += Holdsing_OnStartCharging;

            m_Stats = new IStatePlayerHand[]
            {
                free,
                holdsing,
                powering,
                throwing
            };
        }

        private void Start()
        {
            OnNetworkSpawn();
        }

        public override void OnNetworkSpawn()
        {
            PlayerAnimationChannel channel = GetComponentInChildren<PlayerAnimationChannel>();
            StatePowering powering = m_Stats[StatePowering.k_Indx] as StatePowering;
            m_PoweringVisual = PoweringVisual.Instance.GetPoweringVisual();
            SetLaunchingAnimation(channel);

            powering.OnPoweringNormalized += m_PoweringVisual.UpdateBarNormalized;
            channel.ThrowPoint += Animation_ThrowPoint;
            channel.ThrowPoint += () => { _ = powering.OnThrowPoint(out _); };
            m_StateIndex = StateFree.k_Indx;
            CurrentState.OnStateEnter();
            ManagerGameFoodObj.Instance.OnCollected += ManagerGameFoodObj_OnCollected;
            if (gameObject.TryGetComponent<Player.Player>(out Player.Player player))
            {
                player.OnPlayerSweepFall += Player_OnPlayerSweepFall;
            }
        }

        private void Player_OnPlayerSweepFall(bool i_IsPlayerSweepFall)
        {
            m_Agent.isStopped = i_IsPlayerSweepFall;
        }

        private void ManagerGameFoodObj_OnCollected()
        {
            AIStateFree stateFree = CurrentState as AIStateFree;
            if (stateFree is not null)
            {
                stateFree.FindTarget();
            }
        }
        #endregion

        protected override void Update()
        {
            CurrentState.Update();
        }
    }
}