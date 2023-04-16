using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

internal class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private PlayerScoreVisel m_PlayerScoreVisel;
    private float m_ScorePoint;
    private int m_Kills;
    public float ScorePoint
    {
        get 
        {
            return m_ScorePoint;
        }
        private set
        {
            m_ScorePoint = value;
            m_PlayerScoreVisel.UpdeteValueScore(m_ScorePoint);
        }
    }
    public int Kills
    {
        get
        {
            return m_Kills;
        }
        private set
        {
            m_Kills = value;
            m_PlayerScoreVisel.UpdeteValueKills(m_Kills);
        }
    }
    internal void HitPlayer(Collision collision, float hitPoint, int kill)
    {
        if (DidIHurtMyself(collision))
        {
            Debug.Log("you stupid son of a bitch? You hurt yourself");
        }
        else
        {
            ScorePoint += hitPoint;
            Kills += kill;
        }
    }

    private bool DidIHurtMyself(Collision collision)
    {
        return gameObject.Equals(collision.gameObject);
    }
}