using DiningCombat.Player;
using DiningCombat.Util;
using UnityEngine;
namespace DiningCombat.Environment
{
    public class Egg : MonoBehaviour, IDictionaryObject
    {
        private const float k_Damage = 11f;
        [SerializeField]
        private GameObject m_WholeEgg;
        [SerializeField]
        private GameObject m_BrokenEgg;
        [SerializeField]
        private float m_DisplayTimeAfterTriggerEnter;
        public string NameKey { get; set; }

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
            _ = PlayerLifePoint.TryToDamagePlayer(other.gameObject, k_Damage, out _);
            Invoke("ReturToPool", m_DisplayTimeAfterTriggerEnter);
        }

        private void ReturToPool()
        {
            EggPool.Instance.ReturnToPool(this);
        }
    }
}