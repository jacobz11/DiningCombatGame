//using System.Collections.Generic;
//using UnityEngine;
//using Random = UnityEngine.Random;
//public class FollowWP : MonoBehaviour
//{
//    public GameObject[] m_Waypoints;
//    private List<GameObject> m_WaypointsList;
//    [SerializeField]
//    private Vector3 m_MaxPos;
//    [SerializeField]
//    private Vector3 m_MinPos;
//    [SerializeField]
//    public float m_Speed;
//    [SerializeField]
//    private int m_NumOfWP;
//    [SerializeField]
//    public float m_RotationSpped;
//    [SerializeField]
//    private GameObject m_EggPrefab;
//    [SerializeField]
//    private Vector3 m_Buffer;
//    private int m_CuurentWp = 0;
//    private const float k_MinDistance = 3;

//    public int CuurentWp
//    {
//        get => m_CuurentWp;
//        set 
//        {
//            m_CuurentWp = value % m_Waypoints.Length;
//        }
//    }
//    public Transform CuurentWpPos
//    {
//        get
//        {
//            return m_Waypoints[m_CuurentWp].transform;
//        }
//    }
//    private void Awake()
//    {
//        m_WaypointsList = new List<GameObject>();
//        for (int i =0; i < m_NumOfWP; i++)
//        {
//            GameObject wp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//            wp.transform.position = GetRandoPos();
//            m_WaypointsList.Add(wp);
//        }

//        m_Waypoints = m_WaypointsList.ToArray();
//    }

//    private Vector3 GetRandoPos()
//    {
//        return new Vector3(
//            Random.Range(m_MinPos.x, m_MaxPos.x),
//            Random.Range(m_MinPos.y, m_MaxPos.y),
//            Random.Range(m_MinPos.z, m_MaxPos.z)
//            );
//    }

//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Vector3.Distance(transform.position, CuurentWpPos.position) < k_MinDistance)
//        {
//            ArrivedToWaypoint();
//        }

//        Quaternion lookAtWp = Quaternion.LookRotation(CuurentWpPos.position - transform.position);
//        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtWp, m_RotationSpped *Time.deltaTime);
//        transform.Translate(0, 0, m_Speed * Time.deltaTime);
        
//    }

//    private void ArrivedToWaypoint()
//    {
//        CuurentWp++;
//        GameObject egg = Instantiate(m_EggPrefab, transform.position - m_Buffer, Quaternion.identity);
//    }
//}
