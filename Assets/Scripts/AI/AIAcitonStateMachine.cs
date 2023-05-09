using Assets.scrips.Player.States;
using Assets.scrips.UI;
using Assets.Scripts.AI.States;
using UnityEngine.AI;

internal class AIAcitonStateMachine : AcitonStateMachine
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
        powering.OnStopPowering += powering_OnStopPowering;
        powering.OnStopPowering += throwing.powering_OnStopPowering;
        holdsing.OnStartCharging += holdsing_OnStartCharging;

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

        AddLisenerToPlayer();
        SetLaunchingAnimation(channel);

        powering.OnPoweringNormalized += m_PoweringVisual.UpdateBarNormalized;
        channel.ThrowPoint += Animation_ThrowPoint;
        channel.ThrowPoint += () => { powering.OnThrowPoint(out float _); };
        //channel.StartTrowing += channel_StartTrowing;

        m_StateIndex = StateFree.k_Indx;
        CurrentState.OnSteteEnter();
    }
    #endregion

    protected override void Update()
    {
        CurrentState.Update();
    }
    private void AddLisenerToPlayer()
    {
        Player player = GetComponent<Player>();
        player.OnExitCollisionFoodObj += m_Stats[StateFree.k_Indx].ExitCollisionFoodObj;
        player.OnEnterCollisionFoodObj += m_Stats[StateFree.k_Indx].EnterCollisionFoodObj;
    }
}
