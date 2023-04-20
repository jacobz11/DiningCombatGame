using System;
using UnityEngine;

internal class PlayerLifePoint : MonoBehaviour
{
    private const float k_StrtingLifePoint = 100f; 
    [SerializeField]
    private LifePointsVisual m_LifePointsVisual;
    private float m_LifePoint;

    private void Awake()
    {
        m_LifePoint = k_StrtingLifePoint;
    }
    internal void OnHitPlayer(float hitPoint, out bool o_IsKiil)
    {
        if (hitPoint < 0)
        {
            o_IsKiil = false;
            return;
        }
        m_LifePoint -= hitPoint;
        o_IsKiil = m_LifePoint <= 0;
        float normalizHp = hitPoint / k_StrtingLifePoint;
        //Array.ForEach(m_LifePointsVisual, (LifePointsVisual l) => { l.UpdateBarNormalized(normalizHp); });
        m_LifePointsVisual.UpdateBarNormalized(normalizHp);
    }

    public static bool TryToDamagePlayer(GameObject i_GameObject, float i_Damage, out bool o_IsKill)
    {
        bool isPlayer = i_GameObject.gameObject.TryGetComponent<PlayerLifePoint>(out PlayerLifePoint o_PlayerLife);
        if (isPlayer)
        {
            o_PlayerLife.OnHitPlayer(i_Damage, out o_IsKill);
        }
        else
        {
            o_IsKill = false;
        }

        return isPlayer;
    }
}