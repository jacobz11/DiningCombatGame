using DiningCombat.AI;
using DiningCombat.Environment;
using DiningCombat.FoodObject;
using UnityEngine;
using UnityEngine.AI;
namespace DiningCombat.NPC
{
    public class CornNpc : GameFoodObj
    {
        private NavMeshAgent m_Agent;

        [SerializeField]
        private Room m_RoomDimension;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            FollowWP follow = GetComponent<FollowWP>();
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            UncollectStateCorn uncollect = new UncollectStateCorn(this, agent, m_RoomDimension, follow);
            IThrownState thrownState = m_TypeBuild.SetRigidbody(m_Rigidbody).SetTransform(transform);
            CollectState collectState = new CollectState(m_Rigidbody, transform, this);

            uncollect.OnCountdownEnding += Uncollect_OnCountdownEnding;
            uncollect.Collect += Uncollect_Collect;
            m_AnimationType = m_TypeBuild.m_AnimationType;
            thrownState.OnReturnToPool += ThrownState_OnReturnToPool;

            m_FoodStates = new IFoodState[]
            {
                uncollect,
                collectState,
                thrownState,
            };
        }

        private void Uncollect_OnCountdownEnding()
        {
            Index = ThrownState.k_Indx;
            Index = CollectState.k_Indx;
            CollectInvoke();
        }
    }
}