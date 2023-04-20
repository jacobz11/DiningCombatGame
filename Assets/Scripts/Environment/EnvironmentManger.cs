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
    private GraphDC m_WaterGraph;

    public bool IsInit { get; private set; }

    private void Awake()
    {
       IsInit = true;
       m_WaterGraph = new GraphDC((int)m_RoomDimension.Higet,
            (int)m_RoomDimension.Width,
            m_RoomDimension.m_Center,
            new Action<Vector3>(SpawnWater));
        m_WaterGraph.OnEnding += WaterGraph_OnEnding;
    }

    private void WaterGraph_OnEnding()
    {
        Debug.Log("WaterGraph_OnEnding  ");
        gameObject.SetActive(false);
    }

    private void SpawnWater(Vector3 i_Pos)
    { 
        Instantiate(m_WaterPrefab, i_Pos, Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        if (m_Time.IsBufferOver())
        {
            m_WaterGraph.Activate();
            m_Time.Clear();
        }
    }
}
