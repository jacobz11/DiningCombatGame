using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestCase : MonoBehaviour
{
    [SerializeField]
    public TestsChannelCanvas Canvas;
    [SerializeField]
    public GameObject m_Object;

    protected bool isRunning;
    public abstract void OnTestCaseStart();
    public abstract void OnTestCaseFinish();
}
