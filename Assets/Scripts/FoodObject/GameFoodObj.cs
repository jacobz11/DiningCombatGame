using DiningCombat;
using System;
using System.Collections;
using UnityEngine;

internal class GameFoodObj : MonoBehaviour, IStateMachine<IFoodState, int>, IVisible
{
    private IFoodState[] foodStates;
    private int m_StatuIndex;
    public IFoodState CurrentStatu
    {
        get => foodStates[m_StatuIndex];
    }

    private Rigidbody m_Rigidbody;
    private AcitonStateMachine m_Collecter;
    public IFoodState UncollectState => foodStates[0];
    public IFoodState CollectState => foodStates[1];
    public IFoodState ThrownState => foodStates[2];

    public int Index
    {
        get => m_StatuIndex;
        private set
        {
            CurrentStatu.OnSteteExit();
            m_StatuIndex = value;
            CurrentStatu.OnSteteEnter();
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
        m_Rigidbody = GetComponent<Rigidbody>();
        UncollectState uncollect = new UncollectState(this);
        uncollect.Collect += Uncollect_Collect;
        foodStates = new IFoodState[]
        {
            uncollect,
            new CollectState(m_Rigidbody),
            new ThrownState(m_Rigidbody),
        };
    }

    private void Uncollect_Collect(AcitonStateMachine i_Collecter)
    {
        if (i_Collecter is not null)
        {
            m_Collecter = i_Collecter;
            this.transform.position = m_Collecter.PicUpPoint.position;
            this.transform.SetParent(m_Collecter.PicUpPoint, true);
            tag = GameGlobal.TagNames.k_Picked;
            Index = 1;
            //ManagerGameFoodObj.Instance.UickedFruit -= ViewElement;
        }
    }

    internal bool CanCollect()
    {
        return Index == 0;
    }

    public virtual void ThrowingAction(Vector3 i_Direction, float i_PowerAmount)
    {
        if (CurrentStatu.ThrowingAction())
        {
            transform.parent = null;
            StartCoroutine(ThrowingActionCoroutine(i_Direction, i_PowerAmount));
        }
    }

    private IEnumerator ThrowingActionCoroutine(Vector3 i_Direction, float i_PowerAmount)
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
        EnableRagdoll();
        m_Rigidbody.AddForce(i_PowerAmount * i_Direction);
        Debug.DrawRay(transform.position, i_Direction, Color.black, 3);
    }
    public void EnableRagdoll()
    {
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.detectCollisions = true;
    }
    internal void StopPowering()
    {
        if (Index == 1)
        {
            Index++;
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Index == 2)
        {
            if (collision.gameObject.TryGetComponent<PlayerLifePoint>(out PlayerLifePoint o_PlayerLifePoint))
            {
                if (!collision.gameObject.Equals(m_Collecter.gameObject))
                 {
                    float hitPoint = HitPintCalculator();
                    o_PlayerLifePoint.OnHitPlayer(hitPoint, out bool o_IsKiil);
                    int kill = o_IsKiil ? 1 : 0;
                    m_Collecter.GetScore().HitPlayer(collision, hitPoint, kill);
                }
            }
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        Destruction?.Invoke();
    }
    private float HitPintCalculator()
    {
        return m_Rigidbody.velocity.magnitude;
    }
}