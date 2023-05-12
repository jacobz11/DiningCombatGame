using Assets.scrips;
using Assets.scrips.Player.Data;
using Assets.scrips.Player.States;
using Assets.scrips.UI;
using System;
using Unity.Netcode;
using UnityEngine;
using static GameFoodObj;

internal class ActionStateMachine : NetworkBehaviour, IStateMachine<IStatePlayerHand, int>
{
    protected Action<eThrowAnimationType> LaunchingAnimation;
    protected IStatePlayerHand[] m_Stats;
    protected int m_StateIndex;

    private PlayerScore m_PlayerScore;
    private GameFoodObj m_FoodObj;

    [SerializeField]
    private readonly Transform r_PickUpPoint;
    [SerializeField]
    protected PoweringData m_Powering;
    [SerializeField]
    protected PoweringVisual m_PoweringVisual;
    public Transform PickUpPoint => r_PickUpPoint;
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
    internal PlayerScore GetScore() => m_PlayerScore;
    #region Unity
    private void Awake()
    {
        StateFree stateFree = new StateFree(this);
        StateHoldsObj holdsing = new StateHoldsObj();
        StatePowering powering = new StatePowering(this, m_Powering);
        StateThrowing throwing = new StateThrowing();

        powering.OnStopPowering += powering_OnStopPowering;
        powering.OnStopPowering += throwing.powering_OnStopPowering;
        holdsing.OnStartCharging += holdsing_OnStartCharging;

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
        ListenToPlayer();
        AddLisenrToInput(GetComponent<GameInput>());

        PlayerAnimationChannel channel = GetComponentInChildren<PlayerAnimationChannel>();
        StatePowering powering = m_Stats[StatePowering.k_Indx] as StatePowering;
        SetLaunchingAnimation(channel);

        m_PoweringVisual = PoweringVisual.Instance.GetPoweringVisual();

        powering.OnPoweringNormalized += m_PoweringVisual.UpdateBarNormalized;
        channel.ThrowPoint += Animation_ThrowPoint;
        channel.ThrowPoint += () => { powering.OnThrowPoint(out float _); };

        //channel.StartTrowing += channel_StartTrowing;

        m_StateIndex = StateFree.k_Indx;
        CurrentState.OnStateEnter();
    }

    protected void SetLaunchingAnimation(PlayerAnimationChannel channel)
    {
        LaunchingAnimation += (eThrowAnimationType animationType) =>
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
    protected void channel_StartTrowing()
    {/* m_FoodObj.OnStartTrowing(); */}

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
    protected void powering_OnStopPowering(float obj)
    {
        if (Index == StatePowering.k_Indx)
        {
            eThrowAnimationType animationType = m_FoodObj.StopPowering();
            Index = StateThrowing.k_Indx;
            LaunchingAnimation(animationType);
        }
    }

    protected virtual void holdsing_OnStartCharging()
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
            m_FoodObj.ThrowingAction(PickUpPoint.forward, o_Force);
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
    private void GameInput_OnStopChargingAction(object sender, System.EventArgs e)
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
    private void ListenToPlayer()
    {
        Player player = GetComponent<Player>();
        player.OnExitCollisionFoodObj += m_Stats[StateFree.k_Indx].ExitCollisionFoodObj;
        player.OnEnterCollisionFoodObj += m_Stats[StateFree.k_Indx].EnterCollisionFoodObj;
    }
    #endregion
}