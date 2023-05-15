using System;
using System.Collections;
using UnityEngine;

public abstract class IPackage : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_ParticleSystemPreFap;
    [SerializeField]
    [Range(0f, 1000f)]
    private float m_Amont;
    [SerializeField]
    private GameObject m_Visale;
    private float m_WitingAmont;

    public float Amont => m_Amont;

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