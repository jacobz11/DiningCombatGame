using Assets.scrips.Player.Data;
using Assets.scrips.Player.States;
using Assets.scrips.UI;
using Assets.scrips;
using System;
using UnityEngine;
using static GameFoodObj;

internal class AIAcitonStateMachine : AcitonStateMachine
{
    private Action<eThrowAnimationType> LaunchingAnimation;
    private IStatePlayerHand[] m_Stats;
    private int m_StateIndex;

    private GameFoodObj m_FoodObj;

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

    public override void OnNetworkSpawn()
    {

        base.OnNetworkSpawn();
        LisenToPlayr();
        //AddLisenrToInput(GetComponent<GameInput>());

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

    //private void channel_StartTrowing()
    //{
    //    //m_FoodObj.OnStartTrowing();
    //}
    #endregion


    //private void GameInput_OnPickUpAction(object sender, System.EventArgs e)
    //{
    //    bool isPickItem = CurrentState.OnPickUpAction(out GameFoodObj o_Collcted);
    //    if (isPickItem)
    //    {
    //        m_FoodObj = o_Collcted;
    //        Index = StateHoldsObj.k_Indx;
    //    }
    //}

    private void LisenToPlayr()
    {
        Player player = GetComponent<Player>();
        player.OnExitCollisionFoodObj += m_Stats[StateFree.k_Indx].ExitCollisionFoodObj;
        player.OnEnterCollisionFoodObj += m_Stats[StateFree.k_Indx].EnterCollisionFoodObj;
    }
}
