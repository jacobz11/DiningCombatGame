using UnityEngine;

internal class StaringData : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    public int m_NumOfAi;

    public bool IsOnline { get; internal set; }
}