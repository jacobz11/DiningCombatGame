using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifePoint : MonoBehaviour
{
    public event Action OnPlayerDead;
    private const float k_StrtingLifePoint = 100f;

    private float m_LifePoint;
    [SerializeField]
    private List<LifePointsVisual> m_LifePointsVisual;

    public bool IsAi { get; internal set; }

    private void Awake()
    {
        m_LifePoint = k_StrtingLifePoint;
    }

    private void Start()
    {
        GameOverLogic.Instance.CharacterEntersTheGame(this);
        PlayerAnimationChannel animationChannel = gameObject.GetComponentInChildren<PlayerAnimationChannel>();

        if (animationChannel is null)
        {
            Debug.Log("animationChannel is null");
        }
        else
        {
            OnPlayerDead += animationChannel.OnPlayerDead;
        }
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
        if (o_IsKiil)
        {
            OnPlayerDead?.Invoke();
        }

        float normalizHp = hitPoint / k_StrtingLifePoint;
        m_LifePointsVisual.ForEach(visual => {visual.UpdateBarNormalized(normalizHp); });
    }

    public static bool TryToDamagePlayer(GameObject i_GameObject, float i_Damage, out bool o_IsKill)
    {
        Debug.Assert(i_Damage >= 0, "TryToDamagePlayer : i_Damage is nagtive");
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

    internal void AddLifePointsVisual(LifePointsVisual i_LifePointsVisual)
    {
        m_LifePointsVisual.Add(i_LifePointsVisual);
    }
}