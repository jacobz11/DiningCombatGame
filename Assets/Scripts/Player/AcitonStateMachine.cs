using Assets.scrips;
using Assets.scrips.Player.Data;
using Assets.scrips.Player.States;
using Assets.scrips.UI;
using System;
using UnityEngine;

internal class AcitonStateMachine : MonoBehaviour, IStateMachine<IStatePlayerHand, int>
{
    private IStatePlayerHand[] m_Stats;
    private int m_StateIndex;
    private GameInput m_GameInput;
    [SerializeField]
    private Transform m_PicUpPoint;
    [SerializeField] 
    private PoweringData m_Powering;
    [SerializeField]
    private PoweringVisual m_PoweringVisual;
    [SerializeField]
    private PlayerScore m_PlayerScore;

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
            CurrentStatu.OnSteteExit();
            m_StateIndex = value;
            CurrentStatu.OnSteteEnter();
        }
    }
    public IStatePlayerHand CurrentStatu => m_Stats[m_StateIndex];

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
        Player player = GetComponent<Player>();
        player.OnExitCollisionFoodObj += m_Stats[0].ExitCollisionFoodObj;
        player.OnEnterCollisionFoodObj += m_Stats[0].EnterCollisionFoodObj;

        m_GameInput = GetComponent<GameInput>();
        m_GameInput.OnStartChargingAction += GameInput_OnStartChargingAction;
        m_GameInput.OnStopChargingAction += GameInput_OnStopChargingAction;
        m_GameInput.OnPickUpAction += GameInput_OnPickUpAction;
        m_StateIndex = StateFree.k_Indx;
        CurrentStatu.OnSteteEnter();
        PlayerAnimationChannel channel = GetComponentInChildren<PlayerAnimationChannel>();
        StatePowering powering = m_Stats[2] as StatePowering;
        powering.OnPoweringNormalized += m_PoweringVisual.UpdateBarNormalized;
        powering.OnStopPowering += channel.SetPlayerAnimationToThrow;
        channel.ThrowPoint += Animation_ThrowPoint;
        channel.ThrowPoint += () => { powering.OnThrowPoint(out float _); };
    }

    private void OnTriggerEnter(Collider other)
    {
        CurrentStatu.EnterCollisionFoodObj(other);
    }
    private void OnTriggerExit(Collider other)
    {
        CurrentStatu.ExitCollisionFoodObj(other);
    }

    private void powering_OnStopPowering(float obj)
    {
        if (Index == StatePowering.k_Indx)
        {
            m_FoodObj.StopPowering();
            Index = StateThrowing.k_Indx;
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
        bool isPickItem = CurrentStatu.OnPickUpAction(out GameFoodObj o_Collcted);
        if (isPickItem)
        {
            m_FoodObj = o_Collcted;
            Index = StateHoldsObj.k_Indx;
        }
    }
    private void GameInput_OnStartChargingAction(object sender, System.EventArgs e)
    {
        IsPower = true;
        CurrentStatu.OnChargingAction = true;
    }
    private void GameInput_OnStopChargingAction(object sender, System.EventArgs e)
    {
        IsPower = false;
        CurrentStatu.OnChargingAction = false;
    }

    private void Update()
    {
        CurrentStatu.Update();
    }

    private void Animation_ThrowPoint()
    {
        if (CurrentStatu.OnThrowPoint(out float o_Force))
        {
            m_FoodObj.ThrowingAction(PicUpPoint.forward, o_Force);
            m_FoodObj = null;
            Index = 0;
        }
    }

    internal PlayerScore GetScore()
    {
        return m_PlayerScore;
    }
}