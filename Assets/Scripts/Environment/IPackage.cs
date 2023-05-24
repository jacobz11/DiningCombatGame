using DiningCombat.Manger;
using DiningCombat.Util;
using System;
using System.Collections;
using UnityEngine;

namespace DiningCombat.Environment
{
    public abstract class IPackage : MonoBehaviour, IDictionaryObject
    {
        [SerializeField]
        private ParticleSystem m_ParticleSystemPreFap;
        [SerializeField]
        [Range(0f, 1000f)]
        private float m_Amont;
        [SerializeField]
        private GameObject m_Visale;
        protected float m_WitingAmont;
        private string m_NameKey;
        public float Amont => m_Amont;

        public string NameKey { get => m_NameKey; set => m_NameKey = value; }

        protected virtual void ReturnToPool()
        {
            m_Visale.SetActive(false);
            Instantiate(m_ParticleSystemPreFap, transform).Play();
            _ = StartCoroutine(ReturnToPoolInNS());
        }

        private IEnumerator ReturnToPoolInNS()
        {
            yield return new WaitForSeconds(m_WitingAmont);
            ManagerGamePackage.Instance.ReturnToPool(this);
        }
    }
}