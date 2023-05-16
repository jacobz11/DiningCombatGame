using Assets.Scripts;
using UnityEngine;
// TODO : Add a namespace
public class GameStrting : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PreFab;
    [SerializeField]
    private Room m_RoomDimension;
    [SerializeField]
    private float m_Radius;
    [SerializeField]
    private float m_CenterOffset = 0f;
    public static GameStrting Instance { get; private set; }

    public int NumPlayers { get; private set; }
    public float AngleBetween { get; private set; }
    private int m_Cunter = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public Vector3 GatIntPosForPlayer()
    {
        float angle = m_Cunter * AngleBetween;
        ++m_Cunter;
        float x = m_CenterOffset + m_Radius * Mathf.Cos(Mathf.Deg2Rad * angle);
        float y = 0f;
        float z = m_CenterOffset + m_Radius * Mathf.Sin(Mathf.Deg2Rad * angle);

        return new Vector3(x, y, z);
    }

    public void AddNumOfPlyers(int i_Adding)
    {
        NumPlayers += i_Adding;
        AngleBetween = 360f / NumPlayers;
    }


}
