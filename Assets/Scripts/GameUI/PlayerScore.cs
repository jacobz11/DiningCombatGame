using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private string m_PlayerFormtToShow;
    private int m_ScoreValue = 0;
    private int m_Kills = 0;
    [SerializeField]
    private Text m_Score;
    
    public int ScoreValue
    {
        get => m_ScoreValue;
        set
        {
            m_ScoreValue = value;
            m_Score.text = string.Format(m_PlayerFormtToShow, m_ScoreValue);
        }
    }

    public void OnHitPlayer(object sender, EventArgs args)
    {
        EventHitPlayer hit = args as EventHitPlayer;

        if (hit != null)
        {
            this.m_Kills += hit.Kill;
            this.ScoreValue += hit.Score;
        }
    }

    protected void Start()
    {
        if (m_Score == null) 
        {
            Debug.LogError("no find the Text elment ");
            this.enabled = false;
        }

        m_PlayerFormtToShow = gameObject.name + ": {0}";
        ScoreValue = 0;
    }
}
