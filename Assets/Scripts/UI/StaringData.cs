using UnityEngine;
// TODO : Add a namespace
internal class StaringData : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    public int m_NumOfAi;

    public bool IsOnline => false;
}