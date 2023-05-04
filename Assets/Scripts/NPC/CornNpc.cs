using Assets.DataObject;
using Assets.Scripts;
using Assets.Scripts.NPC;
using DiningCombat;
using UnityEngine;
using UnityEngine.AI;

internal class CornNpc : GameFoodObj
{
    private NavMeshAgent m_Agent;

    [SerializeField]
    private Room m_RoomDimension;
    [SerializeField]
    private GameObject[] m_GameObj;

    private new void Awake()
    {
        Debug.Log("CornNpc Awake");

        m_Rigidbody = GetComponent<Rigidbody>();
        m_GameObj = GameObject.FindGameObjectsWithTag("wall");

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        UncollectStateCorn uncollect = new UncollectStateCorn(this, agent, m_RoomDimension, m_GameObj);
        IThrownState thrownState = m_TypeBuild.SetRigidbody(m_Rigidbody).SetTransform(transform);
        CollectState collectState = new CollectState(m_Rigidbody, transform, this);

        uncollect.OnCountdownEnding += uncollect_OnCountdownEnding;
        uncollect.Collect += Uncollect_Collect;
        m_AnimationType = m_TypeBuild.m_AnimationType;
        thrownState.OnReturnToPool += thrownState_OnReturnToPool;

        m_FoodStates = new IFoodState[]
        {
            uncollect,
            collectState,
            thrownState,
        };
    }

    private void uncollect_OnCountdownEnding()
    {
        Index = ThrownState.k_Indx;
        Index = CollectState.k_Indx;
        CollectInvoke();
    }
}
//using Assets.Scripts.FoodObject.Pools;
//using System;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.AI;
//using static Assets.DataObject.ThrownActionTypesBuilder;

//public class CornNpc : MonoBehaviour
//{
//    private NavMeshAgent m_Agent;
//    private bool IsActionHappen;
//    [SerializeField]
//    private GameObject m_Meshe;
//    [SerializeField]
//    [Range(0f, 10f)]
//    private float m_CountdownInitial;
//    [SerializeField]
//    [Range(0f, 3f)]
//    private float m_DillyrRturningToThePool;
//    [SerializeField]
//    [Range(0f, 3f)]
//    private float k_Damage;
//    [SerializeField]
//    [Range(0f, 3f)]
//    private float m_Radius;
//    [SerializeField]
//    [Range(0f, 3f)]
//    private float m_ForceHitExsplostin;
//    private float m_CountdownToEffect;
//    [SerializeField]
//    protected ParticleSystem m_Effect;

//    private void Activate()
//    {
//        Debug.Log("Activate");
//        m_CountdownToEffect = m_DillyrRturningToThePool;

//        DisplayEffect();
//        float damage = CalculatorDamage();
//        float ponits = 0;
//        int kills = 0;
//        foreach (Collider nearByObj in Physics.OverlapSphere(transform.position, m_Radius))
//        {
//            if (nearByObj.TryGetComponent<Rigidbody>(out Rigidbody o_Rb))
//            {
//                o_Rb.AddExplosionForce(m_ForceHitExsplostin, transform.position, m_Radius);
//            }
//            if (PlayerLifePoint.TryToDamagePlayer(nearByObj.gameObject, damage, out bool o_IsKill))
//            {
//                ponits += damage;
//                kills += o_IsKill ? 1 : 0;
//            }
//        }
//        m_Meshe.SetActive(false);
//    }

//    private float CalculatorDamage()
//    {
//        return k_Damage;
//    }

//    protected void DisplayEffect()
//    {
//        //m_Effect = FoodEffactPool.Instance[eElementSpecialByName.NpcCorn].Get();
//        m_Effect = Instantiate(m_Effect, transform.position, Quaternion.identity);
//        m_Effect.gameObject.transform.position = transform.position;
//        m_Effect.gameObject.SetActive(true);
//        m_Effect.Play();
//    }
//    private void Awake()
//    {
//        m_Agent = GetComponent<NavMeshAgent>();
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//    }

//    private void OnEnable()
//    {
//        m_Meshe.SetActive(true);
//        IsActionHappen = false;
//        m_CountdownToEffect = m_CountdownInitial;
//    }


//    private void Update()
//    {
//        //m_CountdownToEffect -= Time.deltaTime;
//        m_CountdownToEffect -= Time.deltaTime;
//        bool isCountdownOver = m_CountdownToEffect <= 0f;
//        if (isCountdownOver)
//        {
//            if (!IsActionHappen)
//            {
//                IsActionHappen = true;
//                Activate();
//            }
//            else
//            {
//                ReturnToPool();
//            }
//        }
//    }

//    private void ReturnToPool()
//    {
//        Debug.Log("ReturnToPool");
//        //if (m_Effect != null)
//        //{
//        //    m_Effect.gameObject.SetActive(false);
//        //    m_Effect.Stop();
//        //    FoodEffactPool.Instance[eElementSpecialByName.NpcCorn].ReturnToPool(m_Effect);
//        //    m_Effect = null;
//        //}
//        //base.ReturnToPool();
//    }
//}
