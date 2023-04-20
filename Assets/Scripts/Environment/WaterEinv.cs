
using UnityEngine;

public class WaterEinv : MonoBehaviour
{
    private const float m_Damage = 0.01f;
    private void OnTriggerStay(Collider other)
    {
        PlayerLifePoint.TryToDamagePlayer(other.gameObject, m_Damage, out bool _);
    }
}
