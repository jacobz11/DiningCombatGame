using System;
using System.Collections;
using UnityEngine;

public abstract class IPackage : MonoBehaviour
{
    private ParticleSystem m_Effect;
    [SerializeField]
    private ParticleSystem m_ParticleSystemPreFap;
    [SerializeField]
    [Range(0f, 1000f)]
    private float m_Amont;
    [SerializeField]
    private GameObject m_Visale;
    private float m_WitingAmont;

    private void Awake()
    {
        m_Effect = Instantiate(m_ParticleSystemPreFap, transform);
    }
    protected virtual void ReturnToPool()
    {
        m_Visale.SetActive(false);
        m_Effect.Play();
        StartCoroutine(ReturnToPoolInNS());
    }

    private IEnumerator ReturnToPoolInNS()
    {
        yield return new WaitForSeconds(m_WitingAmont);
        m_Effect.Pause();
        ManagerGamePackage.Instance.ReturnToPool(this);
    }
}