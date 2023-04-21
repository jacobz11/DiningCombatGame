using Assets.scrips;
using Assets.scrips.Player.Data;
using Assets.scrips.Player.States;
using Assets.scrips.UI;
using System;
using Unity.Netcode;
using UnityEngine;
using static GameFoodObj;

internal class AcitonStateMachine : NetworkBehaviour, IStateMachine<IStatePlayerHand, int>
{
    private Action<eThrowAnimationType> LaunchingAnimation;
    private IStatePlayerHand[] m_Stats;
    private int m_StateIndex;
    private PlayerScore m_PlayerScore;
    [SerializeField]
    private Transform m_PicUpPoint;
    [SerializeField] 
    private PoweringData m_Powering;
    [SerializeField]
    private PoweringVisual m_PoweringVisual;

    private GameFoodObj m_FoodObj;
    public Transform PicUpPoint { get => m_PicUpPoint; }
    public bool IsPower { get; private set; }

    public int Index
    {
        get
        {
            return m_StateIndex;
        }
        set
        {
            CurrentState.OnSteteExit();
            m_StateIndex = value;
            CurrentState.OnSteteEnter();
        }
    }
    public IStatePlayerHand CurrentState => m_Stats[m_StateIndex];

    #region Unity

    private void Awake()
    {
        StateHoldsObj holdsing = new StateHoldsObj();
        StatePowering powering = new StatePowering(this, m_Powering);
        StateThrowing throwing = new StateThrowing();
        powering.OnStopPowering += powering_OnStopPowering;
        powering.OnStopPowering += throwing.powering_OnStopPowering;
        holdsing.OnStartCharging += holdsing_OnStartCharging;
        m_Stats = new IStatePlayerHand[]
        {
            new StateFree(this),
            holdsing,
            powering,
            throwing 
        };
    }

    private void Start()
    {

    }

    public override void OnNetworkSpawn()
    {
  
        base.OnNetworkSpawn();
        LisenToPlayr();
        AddLisenrToInput(GetComponent<GameInput>());

        PlayerAnimationChannel channel = GetComponentInChildren<PlayerAnimationChannel>();
        LaunchingAnimation += (eThrowAnimationType animationType) =>
        {
            switch (animationType)
            {
                case eThrowAnimationType.Throwing:
                    channel.SetPlayerAnimationToThrow(0f);
                    break;
                case eThrowAnimationType.Falling:
                    channel.SetPlayerAnimationDroping();
                    break;
                default:
                    channel.SetPlayerAnimationToThrow(0f);
                    break;
            }
        };
        
        StatePowering powering = m_Stats[StatePowering.k_Indx] as StatePowering;
        m_PoweringVisual = PoweringVisual.Instance.GetPoweringVisual();
        powering.OnPoweringNormalized += m_PoweringVisual.UpdateBarNormalized;
        channel.ThrowPoint += Animation_ThrowPoint;
        channel.ThrowPoint += () => { powering.OnThrowPoint(out float _); };
        channel.StartTrowing += channel_StartTrowing;
        m_StateIndex = StateFree.k_Indx;
        CurrentState.OnSteteEnter();
    }

    private void channel_StartTrowing()
    {
        //m_FoodObj.OnStartTrowing();
    }

    private void Update()
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
    private void powering_OnStopPowering(float obj)
    {
        if (Index == StatePowering.k_Indx)
        {
            eThrowAnimationType animationType = m_FoodObj.StopPowering();
            Index = StateThrowing.k_Indx;
            LaunchingAnimation(animationType);
        }
    }

    private void holdsing_OnStartCharging()
    {
        if (Index == StateHoldsObj.k_Indx)
        {
            Index = StatePowering.k_Indx;
        }
    }

    private void GameInput_OnPickUpAction(object sender, System.EventArgs e)
    {
        bool isPickItem = CurrentState.OnPickUpAction(out GameFoodObj o_Collcted);
        if (isPickItem)
        {
            m_FoodObj = o_Collcted;
            Index = StateHoldsObj.k_Indx;
        }
    }
    private void Animation_ThrowPoint()
    {
        if (CurrentState.OnThrowPoint(out float o_Force))
        {
            m_FoodObj.ThrowingAction(PicUpPoint.forward, o_Force);
            m_FoodObj = null;
            Index = StateFree.k_Indx;
        }
    }
    #endregion

    private void GameInput_OnStartChargingAction(object sender, System.EventArgs e)
    {
        IsPower = true;
        CurrentState.OnChargingAction = true;
    }
    private void GameInput_OnStopChargingAction(object sender, System.EventArgs e)
    {
        IsPower = false;
        CurrentState.OnChargingAction = false;
    }

    internal PlayerScore GetScore()
    {
        return m_PlayerScore;
    }

    private void AddLisenrToInput(GameInput input)
    {
        input.OnStartChargingAction += GameInput_OnStartChargingAction;
        input.OnStopChargingAction += GameInput_OnStopChargingAction;
        input.OnPickUpAction += GameInput_OnPickUpAction;
    }

    private void LisenToPlayr()
    {
        Player player = GetComponent<Player>();
        player.OnExitCollisionFoodObj += m_Stats[StateFree.k_Indx].ExitCollisionFoodObj;
        player.OnEnterCollisionFoodObj += m_Stats[StateFree.k_Indx].EnterCollisionFoodObj;
    }
}