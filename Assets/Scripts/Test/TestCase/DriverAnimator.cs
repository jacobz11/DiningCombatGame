using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverAnimator : TestCase
{
    public string[] list = new string[]
    {
        "none"
    };
    public enum AnimationsNames
    {
        None = 0,
    }
    [SerializeField]
    [Range(0f, 5f)]
    private float m_MullBuffer;
    [SerializeField]
    [Range(0f, 5f)]
    private float m_TimeBuffer;

    [SerializeField]
    private AnimationsNames m_Start;
    [SerializeField]
    private AnimationsNames m_End;
    [SerializeField]
    private bool isChangeDoing;
    private bool isInCoroutine;

    private void Awake()
    {
        m_Object.active = false;
    }

    public override void OnTestCaseFinish()
    {
        m_Object.active = false;
    }

    public override void OnTestCaseStart()
    {
        m_Object.active = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isNotActive())
        {
            if (m_Start == AnimationsNames.None)
            {
                Debug.Log("Error Start Animations cant be none");
            }
            else
            {
                if (m_End == AnimationsNames.None)
                {
                    StartCoroutine(SingleCoroutine(m_Start));
                }
                else
                {
                    StartCoroutine(TransitionCoroutine(m_Start, m_End));
                }
            }
        }
    }

    private bool isNotActive()
    {
        return isRunning && isInCoroutine;
        throw new NotImplementedException();
    }

    private IEnumerator TransitionCoroutine(AnimationsNames m_Start, AnimationsNames m_End)
    {
        // set 
        if (isChangeDoing)
        {
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(m_TimeBuffer);
        }
        // chane 

        // buffer at the end 
        yield return new WaitForSeconds(m_TimeBuffer * m_MullBuffer);
    }

    private IEnumerator SingleCoroutine(AnimationsNames m_Start)
    {
        // set 
        //yield return new WaitForSeconds(m_TimeBuffer);
        // buffer at the end 
        yield return new WaitForSeconds(m_TimeBuffer * m_MullBuffer);

    }
}
