 using Assets.Scripts.Environment;
using System.Collections;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private const float k_Damage = 11f;
    [SerializeField]
    private GameObject m_WholeEgg;
    [SerializeField]
    private GameObject m_BrokenEgg;
    [SerializeField]
    private float m_DisplayTimeAfterTriggerEnter;

    public void OnExitPool(Vector3 i_Pos)
    {
        transform.position = i_Pos;
        m_WholeEgg.gameObject.SetActive(true);
        m_BrokenEgg.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_WholeEgg.gameObject.SetActive(false);
        m_BrokenEgg.gameObject.SetActive(true);
        PlayerLifePoint.TryToDamagePlayer(other.gameObject, k_Damage, out bool _);
        StartCoroutine(ReturToPool());
    }

    private IEnumerator ReturToPool()
    {
        yield return new WaitForSeconds(m_DisplayTimeAfterTriggerEnter);
        EggPool.Instance.ReturnToPool(this);
    }
}
