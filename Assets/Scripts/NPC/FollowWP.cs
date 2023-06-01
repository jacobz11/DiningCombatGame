using DiningCombat.Environment;
using DiningCombat.NPC;
using DiningCombat.Util;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;
namespace DiningCombat.AI
{
    public class FollowWP : NetworkBehaviour, IDictionaryObject
    {
        public event Action OnEngRund;
        public Vector3[] m_Waypoints;
        private List<Vector3> m_WaypointsList;
        [SerializeField]
        private Vector3 m_MaxPos;
        [SerializeField]
        private Vector3 m_MinPos;
        [SerializeField]
        public float m_Speed;
        [SerializeField]
        private int m_NumOfWP;
        [SerializeField]
        public float m_RotationSpped;
        [SerializeField]
        private GameObject m_EggPrefab;
        [SerializeField]
        private Vector3 m_Buffer;
        private int m_CuurentWp = 0;
        private const float k_MinDistance = 3;

        public int CuurentWp
        {
            get => m_CuurentWp;
            set
            {
                if (value >= m_Waypoints.Length)
                {
                    OnEngRund?.Invoke();
                }

                m_CuurentWp = value % m_Waypoints.Length;
            }
        }
        public Vector3 CuurentWpPos
        {
            get => m_Waypoints[m_CuurentWp];
        }
        private string m_NameKey;
        public string NameKey { get => m_NameKey; set => m_NameKey = value; }

        private void Awake()
        {
            m_WaypointsList = new List<Vector3>();
            for (int i = 0; i < m_NumOfWP; i++)
            {
                m_WaypointsList.Add(GetRandoPos());
            }

            m_Waypoints = m_WaypointsList.ToArray();
        }

        private Vector3 GetRandoPos()
        {
            return new Vector3()
            {
                x = Random.Range(m_MinPos.x, m_MaxPos.x),
                y = Random.Range(m_MinPos.y, m_MaxPos.y),
                z = Random.Range(m_MinPos.z, m_MaxPos.z)
            };
        }

        void Start()
        {
            OnEngRund += FolowWP_OnEngRund;
        }

        void Update()
        {
            if (Vector3.Distance(transform.position, CuurentWpPos) < k_MinDistance)
            {
                ArrivedToWaypoint();
            }

            Quaternion lookAtWp = Quaternion.LookRotation(CuurentWpPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtWp, m_RotationSpped * Time.deltaTime);
            transform.Translate(0, 0, m_Speed * Time.deltaTime);
        }

        private void ArrivedToWaypoint()
        {
            CuurentWp++;
            Egg egg = EggPool.Instance.Get();
            egg.transform.position = transform.position - m_Buffer;
            egg.gameObject.SetActive(true);
        }

        private void FolowWP_OnEngRund()
        {
            ChickenPool.Instance.ReturnToPool(this);
        }
    }
}