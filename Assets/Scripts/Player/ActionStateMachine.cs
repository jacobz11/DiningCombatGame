using DiningCombat.DataObject;
using DiningCombat.FoodObject;
using DiningCombat.Manger;
using DiningCombat.Player.States;
using DiningCombat.UI;
using DiningCombat.Util.DesignPatterns;
using System;
using Unity.Netcode;
using UnityEngine;

using static DiningCombat.FoodObject.GameFoodObj;
namespace DiningCombat.Player
{
    public class ActionStateMachine : NetworkBehaviour, IStateMachine<IStatePlayerHand, int>
    {
        protected Action<eThrowAnimationType> m_LaunchingAnimation;
        protected IStatePlayerHand[] m_Stats;
        protected int m_StateIndex;
        protected Rigidbody m_Rigidbody;

        private GameFoodObj m_FoodObj;

        [SerializeField]
        private Transform m_PickUpPoint;
        [SerializeField]
        protected PoweringDataSo m_Powering;
        [SerializeField]
        protected Player m_Player;

        public Transform PickUpPoint => m_PickUpPoint;
        public bool IsPower { get; private set; }

        public int Index
        {
            get => m_StateIndex;
            set
            {
                CurrentState.OnStateExit();
                m_StateIndex = value;
                CurrentState.OnStateEnter();
            }
        }

        public IStatePlayerHand CurrentState => m_Stats[m_StateIndex];
        public PlayerScore GetScore()
        {
            return m_Player.PlayerScore;
        }
        #region Unity
        private void Awake()
        {
            StateFree stateFree = new StateFree(this);
            StateHoldsObj holdsing = new StateHoldsObj();
            StatePowering powering = new StatePowering(this, m_Powering);
            StateThrowing throwing = new StateThrowing();

            powering.OnStopPowering += Powering_OnStopPowering;
            powering.OnStopPowering += throwing.Powering_OnStopPowering;
            holdsing.OnStartCharging += Holdsing_OnStartCharging;

            m_Stats = new IStatePlayerHand[]
            {
            stateFree,
            holdsing,
            powering,
            throwing
            };
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            AddLisenrToInput(GetComponent<GameInput>());

            StatePowering powering = m_Stats[StatePowering.k_Indx] as StatePowering;
            m_Rigidbody = GetComponent<Rigidbody>();
            SetLaunchingAnimation(m_Player.PlayerAnimation);
            m_Player.PlayerLifePoint.OnPlayerDied += PlayerLifePoint_OnPlayerDied;
            if (!m_Player.IsAi)
            {
                powering.OnPoweringNormalized += GameManger.Instance.PoweringVisual.UpdateBarNormalized;
            }

            m_Player.PlayerAnimation.ThrowPoint += Animation_ThrowPoint;
            m_Player.PlayerAnimation.ThrowPoint += () => { _ = powering.OnThrowPoint(out _); };
            m_StateIndex = StateFree.k_Indx;
            CurrentState.OnStateEnter();
        }

        private void PlayerLifePoint_OnPlayerDied()
        {
            if (m_FoodObj != null)
            {
                m_FoodObj.OnPlayerDied();
            }
        }

        protected void SetLaunchingAnimation(PlayerAnimationChannel channel)
        {
            m_LaunchingAnimation += (eThrowAnimationType animationType) =>
            {
                switch (animationType)
                {
                    case eThrowAnimationType.Throwing:

                        channel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Throw, true);
                        //channel.SetPlayerAnimationToThrow(0f);
                        break;
                    case eThrowAnimationType.Falling:
                        channel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Throw, true);

                        //channel.SetPlayerAnimationDroping();
                        break;
                    default:
                        channel.AnimationBool(PlayerAnimationChannel.AnimationsNames.k_Throw, true);
                        //channel.SetPlayerAnimationToThrow(0f);
                        break;
                }
            };
        }

        protected virtual void Update()
        {
            if (IsOwner)
            {
                CurrentState.Update();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CurrentState.EnterCollisionFoodObj(other);
        }
        private void OnTriggerExit(Collider other)
        {
            CurrentState.ExitCollisionFoodObj(other);
        }
        #endregion

        #region Chnaging state
        protected void Powering_OnStopPowering(float obj)
        {
            if (Index == StatePowering.k_Indx)
            {
                eThrowAnimationType animationType = m_FoodObj.StopPowering();
                Index = StateThrowing.k_Indx;
                m_LaunchingAnimation(animationType);
            }
        }

        protected virtual void Holdsing_OnStartCharging()
        {
            if (Index == StateHoldsObj.k_Indx)
            {
                Index = StatePowering.k_Indx;
            }
        }

        public void GameInput_OnPickUpAction(object sender, System.EventArgs e)
        {
            bool isPickItem = CurrentState.OnPickUpAction(out GameFoodObj o_Collcted);

            if (isPickItem)
            {
                m_FoodObj = o_Collcted;
                Index = StateHoldsObj.k_Indx;
            }
        }
        protected void Animation_ThrowPoint()
        {
            if (CurrentState.OnThrowPoint(out float o_Force))
            {
                m_FoodObj.ThrowingAction(PickUpPoint.forward, o_Force + m_Rigidbody.velocity.magnitude);
                m_FoodObj = null;
                Index = StateFree.k_Indx;
            }
        }
        #endregion

        #region GameInput Action
        public virtual void GameInput_OnStartChargingAction(object sender, System.EventArgs e)
        {
            IsPower = true;
            CurrentState.OnChargingAction = true;
        }
        public void GameInput_OnStopChargingAction(object sender, System.EventArgs e)
        {
            IsPower = false;
            CurrentState.OnChargingAction = false;
        }
        #endregion
        #region Add Lisenrs
        private void AddLisenrToInput(GameInput input)
        {
            input.OnStartChargingAction += GameInput_OnStartChargingAction;
            input.OnStopChargingAction += GameInput_OnStopChargingAction;
            input.OnPickUpAction += GameInput_OnPickUpAction;
        }
        #endregion
    }
}