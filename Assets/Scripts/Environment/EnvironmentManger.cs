using Assets.DataObject;
using Assets.Scripts.Util.Channels;
using Assets.Util;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnvironmentManger : MonoBehaviour
{
    [SerializeField]
    private SpawnData m_SpawnData;
    [SerializeField]
    private RoomDimension m_RoomDimension;
    [SerializeField]
    private GameObject m_WaterPrefab;
    [SerializeField]
    private TimeBuffer m_Time;
    [SerializeField]
    private GameObject m_WaterObject;
    private GraphDC m_WaterGraph;
    private bool m_IsEnding;
    public bool IsInit { get; private set; }

    private void Awake()
    {
       IsInit = true;
       m_WaterGraph = new GraphDC((int)m_RoomDimension.Higet,
            (int)m_RoomDimension.Width,
            m_RoomDimension.m_Center,
            new Action<Vector3>(SpawnWater));
        m_WaterGraph.OnEnding += WaterGraph_OnEnding;
        m_IsEnding = false;
    }

    private void WaterGraph_OnEnding()
    {
        Debug.Log("WaterGraph_OnEnding  ");
        m_IsEnding = true;
    }

    private void SpawnWater(Vector3 i_Pos)
    {
        GameObject water = Instantiate(m_WaterPrefab, i_Pos, Quaternion.identity);
        water.transform.parent = m_WaterObject.transform;
    }


    void Update()
    {
        if (m_IsEnding)
        {
            return;
        }
        if (m_Time.IsBufferOver())
        {
            m_WaterGraph.Activate();
            m_Time.Clear();
        }
    }
}
