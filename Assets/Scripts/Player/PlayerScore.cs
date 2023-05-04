using UnityEngine;

internal class PlayerScore : MonoBehaviour
{
    private float m_ScorePoint;
    private int m_Kills;

    [SerializeField]
    private PlayerScoreVisel m_PlayerScoreVisel;
    public float ScorePoint
    {
        get => m_ScorePoint;
        private set
        {
            m_ScorePoint = value;
            m_PlayerScoreVisel.UpdeteValueScore(m_ScorePoint);
        }
    }
    public int Kills
    {
        get => m_Kills;
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