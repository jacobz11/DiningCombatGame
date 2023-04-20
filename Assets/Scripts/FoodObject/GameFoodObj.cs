using Assets.DataObject;
using Assets.Scripts.Util.Channels.Abstracts;
using DiningCombat;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using Unity.Netcode;
using UnityEngine;
using static ThrownState;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

internal class GameFoodObj : NetworkBehaviour, IStateMachine<IFoodState, int>, IVisible, IViewingElementsPosition
{
    public event Action OnCollect;
    private Rigidbody m_Rigidbody;
    private AcitonStateMachine m_Collector;
    [SerializeField]
    private ThrownActionTypesBuilder m_TypeBuild;
    [Range(0, 200)]
    [SerializeField] 
    private int m_Frames = 5;

    #region State
    private IFoodState[] m_FoodStates;
    private int m_StatuIndex;
    public IFoodState CurrentState
    {
        get => m_FoodStates[m_StatuIndex];
    }

    public int Index
    {
        get => m_StatuIndex;
        private set
        {
            CurrentState.OnSteteExit();
            m_StatuIndex = value;
            CurrentState.OnSteteEnter();
        }
    }
    #endregion
    public bool IsUesed => throw new NotImplementedException();

    public event Action Destruction;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        UncollectState uncollect = new UncollectState(this);
        uncollect.Collect += Uncollect_Collect;
        IThrownState thrownState = m_TypeBuild.SetRigidbody(m_Rigidbody).SetTransform(transform);
        thrownState.OnReturnToPool += thrownState_OnReturnToPool;
        m_FoodStates = new IFoodState[]
        {
            uncollect,
            new CollectState(m_Rigidbody, transform, this),
            thrownState,
        };
    }

    private void thrownState_OnReturnToPool()
    {
        ManagerGameFoodObj.Instance.ReturnToPool(this);
        CurrentState.OnSteteExit();
        m_StatuIndex = 0;
        tag = GameGlobal.TagNames.k_FoodObj;
    }

    #region Uncollect 
    private void Uncollect_Collect(AcitonStateMachine i_Collecter)
    {
        if (i_Collecter is not null)
        {
            m_Collector = i_Collecter;
            this.transform.position = m_Collector.PicUpPoint.position;
            tag = GameGlobal.TagNames.k_Picked;
            Index = CollectState.k_Indx;
            OnCollect?.Invoke();
        }
    }

    internal bool CanCollect()
    {
        return Index == UncollectState.k_Indx;
    }

    #endregion
    #region Collact
    internal void StopPowering()
    {

    }
    internal Vector3 GetCollctorPositin()
    {
        return m_Collector is null ? transform.position : m_Collector.PicUpPoint.position;
    }
    #endregion
    #region Throwing
    public virtual void ThrowingAction(Vector3 i_Direction, float i_PowerAmount)
    {
        if (CurrentState.IsThrowingAction())
        {
            Index = ThrownState.k_Indx;
            IThrownState thrownState = CurrentState as IThrownState;
            thrownState.SetCollcter(m_Collector);
            thrownState.SetThrowDirection(i_Direction, i_PowerAmount);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamaging damaging = CurrentState as IDamaging;
        if (damaging is not null)
        {
            damaging.Activation(collision);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        IDamaging damaging = CurrentState as IDamaging;
        if (damaging is not null)
        {
            damaging.Activation(other);
        }
    }

    #endregion
    #region Pool
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    #endregion

    public void ViewElement(List<Vector3> elements)
    {
        elements.Add(transform.position);
    }

    public bool Unsed() => false;

    public void OnEndUsing() { }

    private void Update()
    {
        CurrentState.Update();
    }
}

//    internal void OnStartTrowing()
//{
//    if (Index == CollectState.k_Indx)
//    {
//        StartCoroutine(WaitNFrameAndSetToNetxIndex(m_Frames));
//    }
//    IEnumerator WaitNFrameAndSetToNetxIndex(int n)
//    {
//        for (int i = 0; i < n; i++)
//        {
//            yield return new WaitForEndOfFrame();
//        }
//        EnableRagdoll();
//        Index = ThrownState.k_Indx;
//    }
//public void EnableRagdoll()
//{
//    Rigidbody.isKinematic = false;
//    Rigidbody.detectCollisions = true;
//}