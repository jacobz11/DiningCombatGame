using Assets.Util.DesignPatterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popcorn : MonoBehaviour
{
    private const float k_Damage = 0.1f;
    private const float k_LifeTime = 15;

    private void Start()
    {
        StartCoroutine(DestroyAfter(k_LifeTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerLifePoint.TryToDamagePlayer(other.gameObject, k_Damage, out bool _);
    }

    private IEnumerator DestroyAfter(float i_LifeTime)
    {
        yield return new WaitForSeconds(i_LifeTime);
        SelfDestroy();
    }
    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
