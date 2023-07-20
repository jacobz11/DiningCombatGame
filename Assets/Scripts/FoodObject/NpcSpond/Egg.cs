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
        public bool IsTriggerEnter { get; private set; }
        public string NameKey { get; set; }

        private void OnEnable()
        {
            m_WholeEgg.gameObject.SetActive(true);
            m_BrokenEgg.gameObject.SetActive(false);
            IsTriggerEnter = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsTriggerEnter)
            {
                m_WholeEgg.gameObject.SetActive(false);
                m_BrokenEgg.gameObject.SetActive(true);
                PlayerLifePoint.TryToDamagePlayer(other.gameObject, k_Damage, out _);
                ReturToPool();
            }
        }

        private void ReturToPool()
        {
            Debug.Log("ReturToPool");
            EggPool.Instance.ReturnToPool(this);
        }
    }
}