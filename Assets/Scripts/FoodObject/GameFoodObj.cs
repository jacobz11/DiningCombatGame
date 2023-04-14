using System;
using UnityEngine;

internal class GameFoodObj : MonoBehaviour, IStateMachine<IFoodState, int>, IVisible
{
    private IFoodState[] foodStates;
    private int m_StatuIndex;
    public IFoodState CurrentStatus
    {
        get => foodStates[m_StatuIndex];
    }

    public IFoodState UncollectState => foodStates[0];
    public IFoodState CollectState => foodStates[1];
    public IFoodState ThrownState => foodStates[2];

    public int Index
    {
        get => m_StatuIndex;
        private set
        {
            CurrentStatus.OnSteteExit();
            m_StatuIndex = value;
            CurrentStatus.OnSteteEnter();
        }
    }

    public event Action Destruction;

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    private void Awake()
    {
        foodStates = new IFoodState[]
        {
            new UncollectState(),
            new CollectState(),
            new ThrownState(),
        };
    }

    internal bool CanCollect()
    {
        return Index == 0;
    }

    public virtual void ThrowingAction(Vector3 i_Direction, float i_PowerAmount)
    {

    }
}