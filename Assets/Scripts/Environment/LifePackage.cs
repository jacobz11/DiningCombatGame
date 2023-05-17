using DiningCombat.Environment;
using UnityEngine;

namespace DiningCombat.Player
{
    public class LifePackage : IPackage
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerLifePoint>(out PlayerLifePoint o_PlayerLife))
            {
                o_PlayerLife.Healed(this);
                ReturnToPool();
            }
        }
    }

}
